global using powererp.Models;
global using Dapper;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Rendering;
global using Microsoft.Data.SqlClient;
global using Microsoft.EntityFrameworkCore;
global using System.Data;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using X.PagedList;
global using X.PagedList.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// ===== macOS 關鍵修正：允許同步 I/O 和移除速率限制 =====
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    // 關鍵修正 1：允許同步 I/O
    serverOptions.AllowSynchronousIO = true;

    // 關鍵修正 2：完全移除資料傳輸速率限制
    serverOptions.Limits.MinRequestBodyDataRate = null;
    serverOptions.Limits.MinResponseDataRate = null;

    // 關鍵修正 3：延長所有逾時時間
    serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10);
    serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(5);

    // 關鍵修正 4：增加請求大小限制
    serverOptions.Limits.MaxRequestBodySize = 104857600; // 100MB

    // 關鍵修正 5：停用 HTTP/2（強制使用 HTTP/1.1）
    serverOptions.ConfigureEndpointDefaults(lo =>
    {
        lo.Protocols = HttpProtocols.Http1;
    });
});

// Add services to the container.
builder.Services.AddControllersWithViews();

#region DI 注入設定
builder.Services.AddSingleton<CssService>();
#endregion

#region Controller設定
builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true;
});

builder.Services.AddRazorPages()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.Encoder =
            JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs);
    });
#endregion

#region 環境設定檔設定
var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
var environmentName = builder.Environment.EnvironmentName;
builder.Configuration
    .SetBasePath(currentDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();
#endregion

#region 資料庫連線設定
builder.Services.AddDbContext<dbEntities>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn"),
        sqlServerOptions =>
        {
            // 設定命令執行逾時時間（秒）
            sqlServerOptions.CommandTimeout(180);
            // 啟用連線重試機制
            sqlServerOptions.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorNumbersToAdd: null);
        });
});
#endregion

#region WebAPI 設定
builder.Services.AddSingleton<JWTBase, JWTServices>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "待辦事項 WebAPI",
        Description = "一個 ASP.NET Core 管理待辦事項的 Web API",
        TermsOfService = new Uri("https://localhost:5050/Home/Terms"),
        Contact = new OpenApiContact
        {
            Name = "連絡我們",
            Url = new Uri("https://localhost:5050/Home/Contact")
        },
        License = new OpenApiLicense
        {
            Name = "版權宣告",
            Url = new Uri("https://localhost:5050/Home/License")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
#endregion

#region WebAPI JWT 設定
var str_issuer = builder.Configuration.GetSection("JwtSettings")
    .GetValue<string>("Issuer") ?? "mvcfull9";
var str_audience = builder.Configuration.GetSection("JwtSettings")
    .GetValue<string>("Audience") ?? "mvcfull9";
var str_signing_key = builder.Configuration.GetSection("JwtSettings")
    .GetValue<string>("SignKey") ?? "123730a1-1e99-428b-9f6d-9f3ed4021234";

builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.IncludeErrorDetails = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = str_issuer,
            ValidateAudience = false,
            ValidAudience = str_audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(str_signing_key)
            ),
            RequireExpirationTime = true,
            ClockSkew = TimeSpan.FromSeconds(30)
        };
    });
#endregion

#region Session設定
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.Name = "mvcfull8";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.None;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.IsEssential = true;
});

builder.Services.AddRazorPages().AddSessionStateTempDataProvider();
builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "forms",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}/{initPage?}");

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();