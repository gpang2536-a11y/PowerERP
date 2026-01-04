using System.Reflection;
using System.Net;

/// <summary>
/// 應用程式參數類別
/// </summary>
public static class AppService
{
    public static string ProjectName { get { return Assembly.GetCallingAssembly().GetName().Name; } }
    /// <summary>
    /// 應用程式名稱
    /// </summary>
    /// <value></value>
    public static string AppName
    {
        get
        {
            return GetApplicationString("AppName");
        }
    }
    /// <summary>
    /// 應用程式版本
    /// </summary>
    /// <value></value>
    public static string AppVersion
    {
        get
        {
            return GetApplicationString("AppVersion");
        }
    }
    /// <summary>
    /// 應用程式版本
    /// </summary>
    /// <value></value>
    public static string AppDescription
    {
        get
        {
            return GetApplicationString("AppDescription");
        }
    }
    /// <summary>
    /// 應用程式版本
    /// </summary>
    /// <value></value>
    public static string AppKeywords
    {
        get
        {
            return GetApplicationString("AppKeywords");
        }
    }
    /// <summary>
    /// 網站設計者
    /// </summary>
    /// <value></value>
    public static string Designer
    {
        get
        {
            return GetApplicationString("Designer");
        }
    }
    /// <summary>
    /// 系統管理者名稱
    /// </summary>
    /// <value></value>
    public static string AdminName
    {
        get
        {
            return GetApplicationString("AdminName");
        }
    }
    /// <summary>
    /// 系統管理者電子信箱
    /// </summary>
    /// <value></value>
    public static string AdminEmail
    {
        get
        {
            return GetApplicationString("AdminEmail");
        }
    }
    /// <summary>
    /// 除錯模式(開發階段不管權限模式)
    /// </summary>
    /// <value></value>
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
    /// <value></value>
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
    /// <value></value>
    public static string MenuController { get { return "Home"; } }
    /// <summary>
    /// 後台選單動作名稱
    /// </summary>
    /// <value></value>
    public static string MenuAction { get { return "Init"; } }

    // public static string GetApplicationString(string KeyName)
    // {
    //     string str_section = $"Applications:{KeyName}";
    //     var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
    //     var config = builder.Build();
    //     return config.GetValue<string>(str_section) ?? "";
    // }





    private static IConfiguration? _cachedConfiguration = null;
    private static readonly object _configLock = new object();

    public static string GetApplicationString(string KeyName)
    {
        if (_cachedConfiguration == null)
        {
            lock (_configLock)
            {
                if (_cachedConfiguration == null)
                {
                    try
                    {
                        var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);

                        _cachedConfiguration = builder.Build();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"⚠️ 讀取設定檔錯誤: {ex.Message}");
                        // 建立空的 Configuration
                        _cachedConfiguration = new ConfigurationBuilder().Build();
                    }
                }
            }
        }

        try
        {
            string str_section = $"Applications:{KeyName}";
            return _cachedConfiguration[str_section] ?? "";
        }
        catch
        {
            // 返回預設值
            if (KeyName == "ApplicationSettings:AppName") return "PowerERP";
            if (KeyName == "ApplicationSettings:Version") return "1.0.0";
            return "";
        }
    }






    /// <summary>
    /// 取得本機名稱
    /// </summary>
    public static string GetHostName()
    {
        // 取得本機名稱
        var strHostName = Dns.GetHostName();
        return strHostName ?? "";
    }

    /// <summary>
    /// 取得本機 IP 位址
    /// </summary>
    public static string GetIpAddress()
    {
        string ipAddress = "";
        // 取得本機名稱
        var strHostName = Dns.GetHostName();
        // 取得本機名稱所有的 IP 位址
        var ipAddresses = Dns.GetHostAddresses(strHostName);
        // 只取第一個 IPV4 IP 位址
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