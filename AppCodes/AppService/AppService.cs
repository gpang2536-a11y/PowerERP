using System.Reflection;
using System.Net;

/// <summary>
/// 應用程式參數類別
/// </summary>
public static class AppService
{
    // ✅ 只保留一組靜態欄位
    private static IConfiguration? _cachedConfiguration = null;
    private static readonly object _configLock = new object();
    private static bool _isInitialized = false;

    // ✅ 靜態建構函式
    static AppService()
    {
        InitializeConfiguration();
    }

    // ✅ 初始化方法
    private static void InitializeConfiguration()
    {
        if (_isInitialized) return;

        lock (_configLock)
        {
            if (_isInitialized) return;

            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);

                _cachedConfiguration = builder.Build();
                _isInitialized = true;

                Console.WriteLine("✅ AppService: Configuration 已初始化");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ AppService: 初始化失敗: {ex.Message}");
                _cachedConfiguration = new ConfigurationBuilder().Build();
                _isInitialized = true;
            }
        }
    }

    // ✅ 只保留一個 GetApplicationString 方法
    public static string GetApplicationString(string KeyName)
    {
        try
        {
            string str_section = $"Applications:{KeyName}";
            return _cachedConfiguration?[str_section] ?? GetDefaultValue(KeyName);
        }
        catch
        {
            return GetDefaultValue(KeyName);
        }
    }

    // ✅ 預設值方法
    private static string GetDefaultValue(string keyName)
    {
        return keyName switch
        {
            "AppName" => "PowerERP",
            "AppVersion" => "1.0.0",
            "AppDescription" => "",
            "AppKeywords" => "",
            "Designer" => "",
            "AdminName" => "",
            "AdminEmail" => "",
            "DebugMode" => "0",
            "LoginMode" => "0",
            _ => ""
        };
    }

    public static string ProjectName { get { return Assembly.GetCallingAssembly().GetName().Name; } }

    /// <summary>
    /// 應用程式名稱
    /// </summary>
    public static string AppName
    {
        get { return GetApplicationString("AppName"); }
    }

    /// <summary>
    /// 應用程式版本
    /// </summary>
    public static string AppVersion
    {
        get { return GetApplicationString("AppVersion"); }
    }

    /// <summary>
    /// 應用程式描述
    /// </summary>
    public static string AppDescription
    {
        get { return GetApplicationString("AppDescription"); }
    }

    /// <summary>
    /// 應用程式關鍵字
    /// </summary>
    public static string AppKeywords
    {
        get { return GetApplicationString("AppKeywords"); }
    }

    /// <summary>
    /// 網站設計者
    /// </summary>
    public static string Designer
    {
        get { return GetApplicationString("Designer"); }
    }

    /// <summary>
    /// 系統管理者名稱
    /// </summary>
    public static string AdminName
    {
        get { return GetApplicationString("AdminName"); }
    }

    /// <summary>
    /// 系統管理者電子信箱
    /// </summary>
    public static string AdminEmail
    {
        get { return GetApplicationString("AdminEmail"); }
    }

    /// <summary>
    /// 除錯模式(開發階段不管權限模式)
    /// </summary>
    public static bool DebugMode
    {
        get
        {
            string str_value = GetApplicationString("DebugMode");
            return (str_value == "1");
        }
    }

    /// <summary>
    /// 登入模式(一進入系統即登入)
    /// </summary>
    public static bool LoginMode
    {
        get
        {
            string str_value = GetApplicationString("LoginMode");
            return (str_value == "1");
        }
    }

    /// <summary>
    /// 後台選單區域名稱
    /// </summary>
    public static string MenuArea { get { return "Menu"; } }

    /// <summary>
    /// 後台選單控制器名稱
    /// </summary>
    public static string MenuController { get { return "Home"; } }

    /// <summary>
    /// 後台選單動作名稱
    /// </summary>
    public static string MenuAction { get { return "Init"; } }

    /// <summary>
    /// 取得本機名稱
    /// </summary>
    public static string GetHostName()
    {
        var strHostName = Dns.GetHostName();
        return strHostName ?? "";
    }

    /// <summary>
    /// 取得本機 IP 位址
    /// </summary>
    public static string GetIpAddress()
    {
        string ipAddress = "";
        var strHostName = Dns.GetHostName();
        var ipAddresses = Dns.GetHostAddresses(strHostName);

        if (ipAddresses.Length > 0)
        {
            foreach (var ip in ipAddresses)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipAddress = ip.ToString();
                    break;
                }
            }
        }
        return ipAddress;
    }
}