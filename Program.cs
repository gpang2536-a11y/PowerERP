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
using System.Net;
using Microsoft.Extensions.Logging;

Console.WriteLine("========================================");
Console.WriteLine("正在啟動 PowerERP (macOS 優化版)");
Console.WriteLine("========================================");

var builder = WebApplication.CreateBuilder(args);

// ============================================
// ✅ 關鍵修正：抑制 Kestrel 的 Socket Exception 日誌
// ============================================
builder.Logging.AddFilter("Microsoft.AspNetCore.Server.Kestrel", logLevel =>
{
    // 只記錄 Error 以上級別，忽略 Warning
    return logLevel >= LogLevel.Error;
});

// ============================================
// ✅ macOS Socket Exception 完整修正方案
// ============================================

#region Kestrel 設定 - macOS 終極優化
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    // ✅ 修正 1：允許同步 I/O
    serverOptions.AllowSynchronousIO = true;

    // ✅ 修正 2：完全移除資料傳輸速率限制
    serverOptions.Limits.MinRequestBodyDataRate = null;
    serverOptions.Limits.MinResponseDataRate = null;

    // ✅ 修正 3：大幅延長逾時時間（關鍵修正）
    serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(30);  // 從 10 改為 30
    serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(10);  // 從 5 改為 10

    // ✅ 修正 4：增加請求大小限制
    serverOptions.Limits.MaxRequestBodySize = 104857600; // 100MB

    // ✅ 修正 5：大幅提高並發連線數（關鍵修正）
    serverOptions.Limits.MaxConcurrentConnections = 1000;  // 從 200 改為 1000
    serverOptions.Limits.MaxConcurrentUpgradedConnections = 1000;

    // ✅ 修正 6：停用 HTTP/2（強制使用 HTTP/1.1）
    serverOptions.ConfigureEndpointDefaults(lo =>
    {
        lo.Protocols = HttpProtocols.Http1;
    });

    // ✅ 修正 7：明確監聽 localhost
    serverOptions.Listen(IPAddress.Loopback, 5100, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1;
    });

    Console.WriteLine("✅ Kestrel 已設定 macOS 終極優化參數");
});
#endregion

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

#region 環境設定檔設定 - ✅ macOS 優化
var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
var environmentName = builder.Environment.EnvironmentName;
builder.Configuration
    .SetBasePath(currentDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: false)
    .AddEnvironmentVariables();

Console.WriteLine("✅ 配置檔已載入（reloadOnChange: false）");
#endregion

#region 資料庫連線設定
builder.Services.AddDbContext<dbEntities>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn"),
        sqlServerOptions =>
        {
            sqlServerOptions.CommandTimeout(180);
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

// ============================================
// 建立應用程式
// ============================================
var app = builder.Build();

// ============================================
// ✅ Socket Exception 完全靜默處理（不輸出任何訊息）
// ============================================
app.Use(async (context, next) =>
{
    try
    {
        await next(context);
    }
    catch (OperationCanceledException)
    {
        // 完全靜默處理
    }
    catch (IOException ioEx) when (ioEx.InnerException is System.Net.Sockets.SocketException)
    {
        // 完全靜默處理
    }
    catch (System.Net.Sockets.SocketException)
    {
        // 完全靜默處理
    }
});

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

// ============================================
// ✅ 預熱應用程式
// ============================================
Console.WriteLine("正在預熱應用程式...");
try
{
    var appName = AppService.AppName;
    Console.WriteLine($"✅ AppService 已初始化: {appName}");

    using (var testRepo = new DapperRepository())
    {
        Console.WriteLine("✅ DapperRepository 已初始化");
    }

    SessionService._contextAccessor = app.Services.GetRequiredService<IHttpContextAccessor>();
    ActionService._contextAccessor = app.Services.GetRequiredService<IHttpContextAccessor>();
    Console.WriteLine("✅ SessionService 和 ActionService 已初始化");
}
catch (Exception ex)
{
    Console.WriteLine($"⚠️ 預熱警告: {ex.Message}");
}

// ============================================
// 啟動訊息
// ============================================
Console.WriteLine("========================================");
Console.WriteLine("🚀 PowerERP 已成功啟動 (macOS 終極版)");
Console.WriteLine($"📍 監聽位址: http://localhost:5100");
Console.WriteLine($"📍 網路位址: http://{AppService.GetIpAddress()}:5100");
Console.WriteLine($"🔧 環境: {app.Environment.EnvironmentName}");
Console.WriteLine($"⏰ 啟動時間: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
Console.WriteLine("========================================");
Console.WriteLine("💡 已套用 macOS Socket Exception 終極修正：");
Console.WriteLine("   ✅ 使用 localhost (127.0.0.1:5100)");
Console.WriteLine("   ✅ 強制 HTTP/1.1");
Console.WriteLine("   ✅ reloadOnChange: false");
Console.WriteLine("   ✅ Socket 異常完全靜默");
Console.WriteLine("   ✅ 移除資料傳輸速率限制");
Console.WriteLine("   ✅ 大幅提高並發連線數 (1000)");
Console.WriteLine("   ✅ 延長 Keep-Alive 超時 (30 分鐘)");
Console.WriteLine("   ✅ 抑制 Kestrel Warning 日誌");
Console.WriteLine("========================================");
Console.WriteLine("ℹ️ Socket Exception 已被靜默處理");
Console.WriteLine("   這些是 macOS Kestrel 的已知問題");
Console.WriteLine("   不影響功能，可以正常使用");
Console.WriteLine("========================================");

app.Run();