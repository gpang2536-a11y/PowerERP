using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.Json;

/// <summary>
/// Session 類別
/// </summary>
public static class SessionService
{
    #region 基本 Session 設定 
    /// <summary>
    /// HttpContextAccessor 物件
    /// </summary>
    /// <returns></returns>
    public static IHttpContextAccessor _contextAccessor { get; set; } = new HttpContextAccessor();
    /// <summary>
    /// HttpContext 物件
    /// </summary>
    public static HttpContext? _context { get { return _contextAccessor.HttpContext; } }
    /// <summary>
    /// 讀取 Session 字串值，不使用 Json 反序列化
    /// </summary>
    /// <param name="keyName">Session 名稱</param>
    /// <param name="defaultValue">預設值</param>
    /// <returns></returns>
    public static string GetSessionValue(string keyName, string defaultValue = "")
    {
        string str_value = _context.Session.Get<string>(keyName);
        if (str_value == null) str_value = defaultValue;
        return str_value;
    }

    /// <summary>
    /// 設定 Session 字串值，不使用 Json 序列化
    /// </summary>
    /// <param name="keyName">Session 名稱</param>
    /// <param name="value">Session 字串值</param>
    public static void SetSessionValue(string keyName, string value)
    {
        _context?.Session.Set<string>(keyName, value);
    }

    /// <summary>
    /// 讀取 Session 字串值，使用 Json 反序列化
    /// </summary>
    /// <param name="keyName">Session 名稱</param>
    /// <returns></returns>
    public static T GetSessionObjectValue<T>(string keyName)
    {
        var obj = (T)Activator.CreateInstance(typeof(T));
        string str_value = GetSessionValue(keyName);
        if (!string.IsNullOrEmpty(str_value))
            obj = JsonSerializer.Deserialize<T>(str_value);
        return obj;
    }

    /// <summary>
    /// 設定 Session 字串值，使用 Json 序列化
    /// </summary>
    /// <param name="keyName">Session 名稱</param>
    /// <param name="value">Session 字串值</param>
    public static void SetSessionObjectValue<T>(string keyName, T value)
    {
        string str_value = JsonSerializer.Serialize(value);
        SetSessionValue(keyName, str_value);
    }
    /// <summary>
    /// 讀取 Session 值(支援泛型)
    /// </summary>
    /// <typeparam name="T">泛型型別</typeparam>
    /// <param name="sessionKey">Session 索引鍵</param>
    /// <param name="defaultValue">查無 Session 值時的預設值</param>
    /// <returns></returns>
    public static object GetSessionValue<T>(string sessionKey, object? defaultValue = null)
    {
        string str_session_value = "";
        if (_context != null) str_session_value = _context.Session.Get<string>(sessionKey);
        bool bln_session_exist = (str_session_value == null) ? false : true;
        if (typeof(T) == typeof(int))
        {
            if (!bln_session_exist)
            {
                if (defaultValue == null) return 0;
                return defaultValue;
            }
            int int_value = 0;
            int.TryParse(str_session_value, out int_value);
            return int_value;
        }
        if (typeof(T) == typeof(string))
        {
            if (!bln_session_exist)
            {
                if (defaultValue == null) return "";
                return defaultValue;
            }
            return str_session_value;
        }
        if (typeof(T) == typeof(bool))
        {
            if (!bln_session_exist)
            {
                if (defaultValue == null) return false;
                return defaultValue;
            }
            return (str_session_value == "yes");
        }
        if (typeof(T) == typeof(DateTime))
        {
            if (!bln_session_exist)
            {
                if (defaultValue == null) return DateTime.MinValue;
                return defaultValue;
            }
            DateTime dtm_value = DateTime.MinValue;
            DateTime.TryParse(str_session_value, out dtm_value);
            return dtm_value;
        }
        return defaultValue;
    }
    /// <summary>
    /// 儲存 Session 值(支援泛型)
    /// </summary>
    /// <typeparam name="T">泛型型別</typeparam>
    /// <param name="sessionKey">Session 索引鍵</param>
    /// <param name="value">Session 值</param>
    public static void SetSessionValue<T>(string sessionKey, T value)
    {
        string str_value = "";
        if (typeof(T) == typeof(bool))
            str_value = (bool.Parse(value.ToString())) ? "yes" : "no";
        else
            str_value = (value == null) ? "" : value.ToString();
        _context?.Session.Set<string>(sessionKey, str_value);
    }
    #endregion
    #region 登入帳號 Session 相關
    /// <summary>
    /// 登入使用者代號
    /// </summary>
    /// <value></value>
    public static string UserNo
    {
        get { return GetSessionValue<string>("UserNo", "").ToString(); }
        set { SetSessionValue<string>("UserNo", value); }
    }
    /// <summary>
    /// 登入使用者姓名
    /// </summary>
    /// <value></value>
    public static string UserName
    {
        get { return GetSessionValue<string>("UserName", "").ToString(); }
        set { SetSessionValue<string>("UserName", value); }
    }
    /// <summary>
    /// 登入使用者組織
    /// </summary>
    /// <value></value>
    public static string OrgNo
    {
        get { return GetSessionValue<string>("OrgNo", "").ToString(); }
        set { SetSessionValue<string>("OrgNo", value); }
    }
    /// <summary>
    /// 登入使用者公司
    /// </summary>
    /// <value></value>
    public static string CompanyNo
    {
        get { return GetSessionValue<string>("CompanyNo", "").ToString(); }
        set { SetSessionValue<string>("CompanyNo", value); }
    }
    /// <summary>
    /// 登入使用者角色
    /// </summary>
    /// <value></value>
    public static string RoleNo
    {
        get { return GetSessionValue<string>("RoleNo", "").ToString(); }
        set { SetSessionValue<string>("RoleNo", value); }
    }
    /// <summary>
    /// 是否已經登入
    /// </summary>
    /// <value></value>
    public static bool IsLogin
    {
        get { return (bool)GetSessionValue<bool>("IsLogin", false); }
        set { SetSessionValue<bool>("IsLogin", value); }
    }
    /// <summary>
    /// 登入使用者部門代號
    /// </summary>
    /// <value></value>
    public static string DeptNo
    {
        get { return GetSessionValue<string>("DeptNo", "").ToString(); }
        set { SetSessionValue<string>("DeptNo", value); }
    }
    /// <summary>
    /// 登入使用者部門名稱
    /// </summary>
    /// <value></value>
    public static string DeptName
    {
        get { return GetSessionValue<string>("DeptName", "").ToString(); }
        set { SetSessionValue<string>("DeptName", value); }
    }
    /// <summary>
    /// 登入使用者職稱代號
    /// </summary>
    /// <value></value>
    public static string TitleNo
    {
        get { return GetSessionValue<string>("TitleNo", "").ToString(); }
        set { SetSessionValue<string>("TitleNo", value); }
    }
    /// <summary>
    /// 登入使用者職稱
    /// </summary>
    /// <value></value>
    public static string TitleName
    {
        get { return GetSessionValue<string>("TitleName", "").ToString(); }
        set { SetSessionValue<string>("TitleName", value); }
    }
    /// <summary>
    /// Row ID
    /// </summary>
    /// <value></value>
    public static int RowId
    {
        get
        {
            int int_value = 0;
            string str_value = "0";
            if (_context != null) str_value = _context.Session.Get<string>("RowId");
            if (str_value == null) str_value = "0";
            if (!int.TryParse(str_value, out int_value)) int_value = 0;
            return int_value;
        }
        set
        { _context?.Session.Set<string>("RowId", value.ToString()); }
    }
    /// <summary>
    /// Row Data
    /// </summary>
    /// <value></value>
    public static string RowData
    {
        get { return GetSessionValue<string>("RowData", "").ToString(); }
        set { SetSessionValue<string>("RowData", value); }
    }
    /// <summary>
    /// ColumnName
    /// </summary>
    /// <value></value>
    public static string ColumnName
    {
        get { return GetSessionValue<string>("ColumnName", "").ToString(); }
        set { SetSessionValue<string>("ColumnName", value); }
    }
    /// <summary>
    /// ImageFolder
    /// </summary>
    /// <value></value>
    public static string ImageFolder
    {
        get { return GetSessionValue<string>("ImageFolder", "").ToString(); }
        set { SetSessionValue<string>("ImageFolder", value); }
    }
    /// <summary>
    /// 使用者圖片
    /// </summary>
    /// <value></value>
    public static string UserImage
    {
        get
        {
            // 給瀏覽器使用的 URL（永遠用 /）
            string webPath = "~/images/users/";

            // 實體檔案完整路徑（跨平台）
            string physicalPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "images",
                "users",
                $"{UserNo}.jpg"
            );

            // 檔案存在就用使用者圖片，否則用預設圖
            if (File.Exists(physicalPath))
                webPath += $"{UserNo}.jpg";
            else
                webPath += "User.jpg";

            // 避免瀏覽器快取
            webPath += $"?t={DateTime.Now:yyyyMMddHHmmssff}";

            return webPath;
        }
    }
    #endregion
    #region 表單程式 Session 相關
    /// <summary>
    /// 主檔編號
    /// </summary>
    /// <value></value>
    public static string BaseNo
    {
        get { return GetSessionValue<string>("BaseNo", "").ToString(); }
        set { SetSessionValue<string>("BaseNo", value); }
    }
    /// <summary>
    /// 選取值
    /// </summary>
    /// <value></value>
    public static string SelectValue
    {
        get { return GetSessionValue<string>("SelectValue", "").ToString(); }
        set { SetSessionValue<string>("SelectValue", value); }
    }
    /// <summary>
    /// 模組代號
    /// </summary>
    /// <value></value>
    public static string ModuleNo
    {
        get { return GetSessionValue<string>("ModuleNo", "").ToString(); }
        set { SetSessionValue<string>("ModuleNo", value); }
    }
    /// <summary>
    /// 模組名稱
    /// </summary>
    /// <value></value>
    public static string ModuleName
    {
        get { return GetSessionValue<string>("ModuleName", "").ToString(); }
        set { SetSessionValue<string>("ModuleName", value); }
    }
    /// <summary>
    /// 鎖定代號
    /// </summary>
    /// <value></value>
    public static string LockNo
    {
        get { return GetSessionValue<string>("LockNo", "").ToString(); }
        set { SetSessionValue<string>("LockNo", value); }
    }
    /// <summary>
    /// 鎖定日期
    /// </summary>
    public static DateTime LockDate
    {
        get { return DateTime.Parse(GetSessionValue<string>("LockDate", DateTime.MinValue.ToString()).ToString()); }
        set { SetSessionValue<string>("LockDate", value.ToString()); }
    }
    /// <summary>
    /// 程式代號
    /// </summary>
    /// <value></value>
    public static string PrgNo
    {
        get { return GetSessionValue<string>("PrgNo", "").ToString(); }
        set { SetSessionValue<string>("PrgNo", value); }
    }
    /// <summary>
    /// 程式名稱
    /// </summary>
    /// <value></value>
    public static string PrgName
    {
        get { return GetSessionValue<string>("PrgName", "").ToString(); }
        set { SetSessionValue<string>("PrgName", value); }
    }
    /// <summary>
    /// 程式控制器名稱
    /// </summary>
    /// <value></value>
    public static string PrgControllerName
    {
        get { return GetSessionValue<string>("PrgControllerName", "").ToString(); }
        set { SetSessionValue<string>("PrgControllerName", value); }
    }
    /// <summary>
    /// 程式區域名稱
    /// </summary>
    /// <value></value>
    public static string PrgAreaName
    {
        get { return GetSessionValue<string>("PrgAreaName", "").ToString(); }
        set { SetSessionValue<string>("PrgAreaName", value); }
    }
    /// <summary>
    /// 目前頁數
    /// </summary>
    /// <value></value>
    public static int Page
    {
        get
        {
            int int_value = 1;
            string str_value = GetSessionValue<int>("Page", 1).ToString();
            if (!int.TryParse(str_value, out int_value)) int_value = 1;
            return int_value;
        }
        set { SetSessionValue<int>("Page", value); }
    }
    /// <summary>
    /// 表頭目前頁數
    /// </summary>
    /// <value></value>
    public static int PageMaster
    {
        get
        {
            int int_value = 1;
            string str_value = GetSessionValue<int>("PageMaster", 1).ToString();
            if (!int.TryParse(str_value, out int_value)) int_value = 1;
            return int_value;
        }
        set { SetSessionValue<int>("PageMaster", value); }
    }
    /// <summary>
    /// 表頭目前頁數
    /// </summary>
    /// <value></value>
    public static int PageDetail
    {
        get
        {
            int int_value = 1;
            string str_value = GetSessionValue<int>("PageDetail", 1).ToString();
            if (!int.TryParse(str_value, out int_value)) int_value = 1;
            return int_value;
        }
        set { SetSessionValue<int>("PageDetail", value); }
    }
    /// <summary>
    /// 明細所在頁籤
    /// </summary>
    /// <value></value>
    public static int DetailPageTabIndex
    {
        get
        {
            int int_value = 1;
            string str_value = GetSessionValue<int>("DetailPageTabIndex", 1).ToString();
            if (!int.TryParse(str_value, out int_value)) int_value = 1;
            return int_value;
        }
        set { SetSessionValue<int>("DetailPageTabIndex", value); }
    }
    /// <summary>
    /// 表頭每頁筆數
    /// </summary>
    /// <value></value>
    public static int PageMasterSize
    {
        get
        {
            int int_value = 1;
            string str_value = GetSessionValue<int>("PageMasterSize", 1).ToString();
            if (!int.TryParse(str_value, out int_value)) int_value = 1;
            return int_value;
        }
        set { SetSessionValue<int>("PageMasterSize", value); }
    }
    /// <summary>
    /// 明細每頁筆數
    /// </summary>
    /// <value></value>
    public static int PageDetailSize
    {
        get
        {
            int int_value = 10;
            string str_value = GetSessionValue<int>("PageDetailSize", 10).ToString();
            if (!int.TryParse(str_value, out int_value)) int_value = 10;
            return int_value;
        }
        set { SetSessionValue<int>("PageDetailSize", value); }
    }
    /// <summary>
    /// 總頁數
    /// </summary>
    /// <value></value>
    public static int PageCount
    {
        get
        {
            int int_value = 0;
            string str_value = GetSessionValue<int>("PageCount", 0).ToString();
            if (!int.TryParse(str_value, out int_value)) int_value = 0;
            return int_value;
        }
        set { SetSessionValue<int>("PageCount", value); }
    }
    /// <summary>
    /// 表頭總頁數
    /// </summary>
    /// <value></value>
    public static int PageCountMaster
    {
        get
        {
            int int_value = 0;
            string str_value = GetSessionValue<int>("PageCountMaster", 0).ToString();
            if (!int.TryParse(str_value, out int_value)) int_value = 0;
            return int_value;
        }
        set { SetSessionValue<int>("PageCountMaster", value); }
    }
    /// <summary>
    /// 明細總頁數
    /// </summary>
    /// <value></value>
    public static int PageCountDetail
    {
        get
        {
            int int_value = 0;
            string str_value = GetSessionValue<int>("PageCountDetail", 0).ToString();
            if (!int.TryParse(str_value, out int_value)) int_value = 0;
            return int_value;
        }
        set { SetSessionValue<int>("PageCountDetail", value); }
    }
    /// <summary>
    /// 表頭 Id
    /// </summary>
    /// <value></value>
    public static int MasterId
    {
        get
        {
            int int_value = 0;
            string str_value = GetSessionValue<int>("MasterId", 0).ToString();
            if (!int.TryParse(str_value, out int_value)) int_value = 0;
            return int_value;
        }
        set { SetSessionValue<int>("MasterId", value); }
    }
    /// <summary>
    /// 表頭 No
    /// </summary>
    /// <value></value>
    public static string MasterNo
    {
        get
        {
            string str_value = GetSessionValue<string>("MasterNo", "").ToString();
            if (string.IsNullOrEmpty(str_value)) str_value = "";
            return str_value;
        }
        set { SetSessionValue<string>("MasterNo", value); }
    }
    /// <summary>
    /// 表頭 BaseNo
    /// </summary>
    /// <value></value>
    public static string MasterBaseNo
    {
        get
        {
            string str_value = GetSessionValue<string>("MasterBaseNo", "").ToString();
            if (string.IsNullOrEmpty(str_value)) str_value = "";
            return str_value;
        }
        set { SetSessionValue<string>("MasterBaseNo", value); }
    }
    /// <summary>
    /// 表頭選取的 No
    /// </summary>
    /// <value></value>
    public static string SelectNo
    {
        get
        {
            string str_value = GetSessionValue<string>("SelectNo", "").ToString();
            if (string.IsNullOrEmpty(str_value)) str_value = "";
            return str_value;
        }
        set { SetSessionValue<string>("SelectNo", value); }
    }
    /// <summary>
    /// 表頭選取的 Id
    /// </summary>
    /// <value></value>
    public static int SelectId
    {
        get
        {
            int int_value = 0;
            string str_value = GetSessionValue<int>("SelectId", 0).ToString();
            if (!int.TryParse(str_value, out int_value)) int_value = 0;
            return int_value;
        }
        set { SetSessionValue<int>("SelectId", value); }
    }
    /// <summary>
    /// 權限物件類別
    /// </summary>
    /// <value></value>
    public static string SecurityCodeNo
    {
        get
        {
            string str_value = GetSessionValue<string>("SecurityCodeNo", "").ToString();
            if (string.IsNullOrEmpty(str_value)) str_value = "";
            return str_value;
        }
        set { SetSessionValue<string>("SecurityCodeNo", value); }
    }
    /// <summary>
    /// 權限對象代號
    /// </summary>
    /// <value></value>
    public static string SecurityTargetNo
    {
        get
        {
            string str_value = GetSessionValue<string>("SecurityTargetNo", UserNo).ToString();
            if (string.IsNullOrEmpty(str_value)) str_value = UserNo;
            return str_value;
        }
        set { SetSessionValue<string>("SecurityTargetNo", value); }
    }
    /// <summary>
    /// 要表頭執行的功能
    /// </summary>
    /// <value></value>
    public static enFormActioEnum FormAction
    {
        get
        {
            enFormActioEnum en_value = enFormActioEnum.None;
            string str_value = GetSessionValue<int>("FormAction", 0).ToString();
            if (!Enum.TryParse(str_value, out en_value)) en_value = enFormActioEnum.None;
            return en_value;
        }
        set { SetSessionValue<enFormActioEnum>("FormAction", value); }
    }
    /// <summary>
    /// 要表頭執行的功能
    /// </summary>
    /// <value></value>
    public static FormSecurity FormSecurity
    {
        get
        {

            return GetSessionObjectValue<FormSecurity>("FormSecurity");
        }
        set
        {
            SetSessionObjectValue<FormSecurity>("FormSecurity", value);
        }
    }
    /// <summary>
    /// 程式資訊
    /// </summary>
    /// <value></value>
    public static string PrgInfo
    {
        get
        {
            string str_value = (string.IsNullOrEmpty(PrgNo)) ? "" : $"{PrgNo} ";
            str_value += PrgName;
            return str_value;
        }
    }
    /// <summary>
    /// 每頁筆數
    /// </summary>
    /// <value></value>
    public static int PageSize
    {
        get
        {
            int int_value = 10;
            string str_value = GetSessionValue<int>("PageSize", 10).ToString();
            if (!int.TryParse(str_value, out int_value)) int_value = 10;
            return int_value;
        }
        set { SetSessionValue<int>("PageSize", value); }
    }
    /// <summary>
    /// 每頁筆數
    /// </summary>
    /// <value></value>
    public static int PageSizeMaster
    {
        get
        {
            int int_value = 1;
            string str_value = GetSessionValue<int>("PageSizeMaster", 1).ToString();
            if (!int.TryParse(str_value, out int_value)) int_value = 1;
            return int_value;
        }
        set { SetSessionValue<int>("PageSizeMaster", value); }
    }
    /// <summary>
    /// 每頁筆數
    /// </summary>
    /// <value></value>
    public static int PageSizeDetail
    {
        get
        {
            int int_value = 1;
            string str_value = GetSessionValue<int>("PageSizeDetail", 1).ToString();
            if (!int.TryParse(str_value, out int_value)) int_value = 1;
            return int_value;
        }
        set { SetSessionValue<int>("PageSizeDetail", value); }
    }
    /// <summary>
    /// 是否為鎖定模式
    /// </summary>
    /// <value></value>
    public static bool IsLockMode
    {
        get
        {
            bool bln_value = false;
            string str_value = GetSessionValue<bool>("IsLockMode", bln_value).ToString();
            if (!bool.TryParse(str_value, out bln_value)) bln_value = false;
            return bln_value;
        }
        set { SetSessionValue<bool>("IsLockMode", value); }
    }
    /// <summary>
    /// 是否為為確認模式
    /// </summary>
    /// <value></value>
    public static bool IsConfirmMode
    {
        get
        {
            bool bln_value = false;
            string str_value = GetSessionValue<bool>("IsConfirmMode", bln_value).ToString();
            if (!bool.TryParse(str_value, out bln_value)) bln_value = false;
            return bln_value;
        }
        set { SetSessionValue<bool>("IsConfirmMode", value); }
    }
    /// <summary>
    /// 是否為為作廢模式
    /// </summary>
    /// <value></value>
    public static bool IsCancelMode
    {
        get
        {
            bool bln_value = false;
            string str_value = GetSessionValue<bool>("IsCancelMode", bln_value).ToString();
            if (!bool.TryParse(str_value, out bln_value)) bln_value = false;
            return bln_value;
        }
        set { SetSessionValue<bool>("IsCancelMode", value); }
    }
    /// <summary>
    /// 是否為唯讀模式
    /// </summary>
    /// <value></value>
    public static bool IsReadonlyMode
    {
        get
        {
            bool bln_value = false;
            string str_value = GetSessionValue<bool>("IsReadonlyMode", bln_value).ToString();
            if (!bool.TryParse(str_value, out bln_value)) bln_value = false;
            return bln_value;
        }
        set { SetSessionValue<bool>("IsReadonlyMode", value); }
    }
    /// <summary>
    /// 是否為鎖定表單模式
    /// </summary>
    /// <value></value>
    public static bool IsFormLocked
    {
        get
        {
            bool bln_value = false;
            string str_value = GetSessionValue<bool>("IsFormLocked", bln_value).ToString();
            if (!bool.TryParse(str_value, out bln_value)) bln_value = false;
            return bln_value;
        }
        set { SetSessionValue<bool>("IsFormLocked", value); }
    }
    /// <summary>
    /// 是否為鎖定表單模式
    /// </summary>
    /// <value></value>
    public static bool IsFormConfirmMode
    {
        get
        {
            bool bln_value = false;
            string str_value = GetSessionValue<bool>("IsFormConfirmMode", bln_value).ToString();
            if (!bool.TryParse(str_value, out bln_value)) bln_value = false;
            return bln_value;
        }
        set { SetSessionValue<bool>("IsFormConfirmMode", value); }
    }
    /// <summary>
    /// 是否為鎖定表單模式
    /// </summary>
    /// <value></value>
    public static bool IsFormCloseMode
    {
        get
        {
            bool bln_value = false;
            string str_value = GetSessionValue<bool>("IsFormCloseMode", bln_value).ToString();
            if (!bool.TryParse(str_value, out bln_value)) bln_value = false;
            return bln_value;
        }
        set { SetSessionValue<bool>("IsFormCloseMode", value); }
    }
    /// <summary>
    /// 是否為表頭明細式表單
    /// </summary>
    /// <value></value>
    public static bool IsMultiMode
    {
        get
        {
            bool bln_value = false;
            string str_value = GetSessionValue<bool>("IsMultiMode", bln_value).ToString();
            if (!bool.TryParse(str_value, out bln_value)) bln_value = false;
            return bln_value;
        }
        set { SetSessionValue<bool>("IsMultiMode", value); }
    }
    /// <summary>
    ///表頭表單是否確認
    /// </summary>
    /// <value></value>
    public static bool IsConfirm
    {
        get
        {
            bool bln_value = false;
            string str_value = GetSessionValue<bool>("IsConfirm", bln_value).ToString();
            if (!bool.TryParse(str_value, out bln_value)) bln_value = false;
            return bln_value;
        }
        set { SetSessionValue<bool>("IsConfirm", value); }
    }
    /// <summary>
    ///表頭表單是否作廢
    /// </summary>
    /// <value></value>
    public static bool IsCancel
    {
        get
        {
            bool bln_value = false;
            string str_value = GetSessionValue<bool>("IsCancel", bln_value).ToString();
            if (!bool.TryParse(str_value, out bln_value)) bln_value = false;
            return bln_value;
        }
        set { SetSessionValue<bool>("IsCancel", value); }
    }
    /// <summary>
    /// 分頁資訊
    /// </summary>
    /// <value></value>
    public static string PageInfo { get { return $"第 ({Page} 頁，共 {PageCount} 頁)"; } }

    /// <summary>
    /// 動作代號
    /// </summary>
    /// <value></value>
    public static string ActionNo
    {
        get { return GetSessionValue<string>("ActionNo", "").ToString(); }
        set { SetSessionValue<string>("ActionNo", value); }
    }

    /// <summary>
    /// 動作名稱
    /// </summary>
    /// <value></value>
    public static string ActionName
    {
        get { return GetSessionValue<string>("ActionName", "").ToString(); }
        set { SetSessionValue<string>("ActionName", value); }
    }

    /// <summary>
    /// 子標題名稱
    /// </summary>
    /// <value></value>
    public static string SubActionName
    {
        get { return GetSessionValue<string>("SubActionName", "").ToString(); }
        set { SetSessionValue<string>("SubActionName", value); }
    }

    /// <summary>
    /// 父階頁數
    /// </summary>
    /// <value></value>
    public static int ParentPage
    {
        get
        {
            int int_value = 1;
            string str_value = GetSessionValue<int>("ParentPage", 1).ToString();
            if (!int.TryParse(str_value, out int_value)) int_value = 1;
            return int_value;
        }
        set { SetSessionValue<int>("ParentPage", value); }
    }

    /// <summary>
    /// 父階 Id
    /// </summary>
    /// <value></value>
    public static int ParentId
    {
        get
        {
            int int_value = 0;
            string str_value = GetSessionValue<int>("ParentPage", 0).ToString();
            if (!int.TryParse(str_value, out int_value)) int_value = 0;
            return int_value;
        }
        set { SetSessionValue<int>("ParentPage", value); }
    }

    /// <summary>
    /// 父階編號
    /// </summary>
    /// <value></value>
    public static string ParentNo
    {
        get { return GetSessionValue<string>("ParentNo", "").ToString(); }
        set { SetSessionValue<string>("ParentNo", value); }
    }

    /// <summary>
    /// 父階名稱
    /// </summary>
    /// <value></value>
    public static string ParentName
    {
        get { return GetSessionValue<string>("ParentName", "").ToString(); }
        set { SetSessionValue<string>("ParentName", value); }
    }

    /// <summary>
    /// 明細資料 Id
    /// </summary>
    /// <value></value>
    public static int DetailId
    {
        get
        {
            int int_value = 0;
            string str_value = GetSessionValue<int>("DetailId", 0).ToString();
            if (!int.TryParse(str_value, out int_value)) int_value = 0;
            return int_value;
        }
        set { SetSessionValue<int>("DetailId", value); }
    }

    /// <summary>
    /// 卡片寛度
    /// </summary>
    /// <value></value>
    public static string CardSize
    {
        get { return GetSessionValue<string>("CardSize", "").ToString(); }
        set { SetSessionValue<string>("CardSize", value); }
    }

    /// <summary>
    /// 搜尋文字
    /// </summary>
    /// <value></value>
    public static string SearchText
    {
        get { return GetSessionValue<string>("SearchText", "").ToString(); }
        set { SetSessionValue<string>("SearchText", value); }
    }
    /// <summary>
    /// 排序欄位
    /// </summary>
    /// <value></value>
    public static string SortColumn
    {
        get { return GetSessionValue<string>("SortColumn", "").ToString(); }
        set { SetSessionValue<string>("SortColumn", value); }
    }
    /// <summary>
    /// 排序方式 (asc / desc) 小寫
    /// </summary>
    /// <value></value>
    public static string SortDirection
    {
        get { return GetSessionValue<string>("SortDirection", "asc").ToString(); }
        set { SetSessionValue<string>("SortDirection", value); }
    }
    /// <summary>
    /// 是否有分頁功能
    /// </summary>
    /// <value></value>
    public static bool IsPageSize
    {
        get
        {
            bool bln_value = false;
            string str_value = GetSessionValue<bool>("IsPageSize", bln_value).ToString();
            if (!bool.TryParse(str_value, out bln_value)) bln_value = false;
            return bln_value;
        }
        set { SetSessionValue<bool>("IsPageSize", value); }
    }
    /// <summary>
    /// 是否有搜尋功能
    /// </summary>
    /// <value></value>
    public static bool IsSearch
    {
        get
        {
            bool bln_value = false;
            string str_value = GetSessionValue<bool>("IsSearch", bln_value).ToString();
            if (!bool.TryParse(str_value, out bln_value)) bln_value = false;
            return bln_value;
        }
        set { SetSessionValue<bool>("IsSearch", value); }
    }
    /// <summary>
    /// 排序編號
    /// </summary>
    /// <value></value>
    public static string SortNo
    {
        get { return GetSessionValue<string>("SortNo", "Product").ToString(); }
        set { SetSessionValue<string>("SortNo", value); }
    }

    /// <summary>
    /// 設定程式預設事件
    /// </summary>
    /// <param name="subActionName">副標題</param>
    public static void SetPrgInit(string subActionName = "")
    {
        SortColumn = "";
        SortDirection = "";
        SearchText = "";
        if (ActionService.Controller == "Home")
        {
            PrgNo = "Home";
            PrgName = "首頁";
            SubActionName = "";
            return;
        }
        if (!string.IsNullOrEmpty(subActionName))
        {
            SubActionName = subActionName;
            return;
        }
        SubActionName = PrgInfo;
    }

    /// <summary>
    /// 設定程式資訊
    /// </summary>
    /// <param name="prgNo">程式編號</param>
    /// <param name="prgName">程式名稱</param>
    public static void SetProgramInfo(string prgNo, string prgName)
    {
        PrgNo = prgNo;
        PrgName = prgName;
    }

    /// <summary>
    /// 設定程式資訊
    /// </summary>
    /// <param name="prgNo">程式編號</param>
    /// <param name="prgName">程式名稱</param>
    /// <param name="isPageSize">是否有分頁</param>
    /// <param name="isSearch">是否有搜尋</param>
    /// <param name="pageSize">每頁筆數</param>
    public static void SetProgramInfo(string prgNo, string prgName, bool isPageSize, bool isSearch, int pageSize)
    {
        PrgNo = prgNo;
        PrgName = prgName;
        IsPageSize = isPageSize;
        IsSearch = isSearch;
        PageSize = pageSize;
    }

    /// <summary>
    /// 取得分頁資訊
    /// </summary>
    /// <param name="page">目前頁數</param>
    /// <returns></returns>
    public static void SetPageInfo(int page)
    {
        Page = page;
        PageSize = 0;
        PageCount = 0;
    }

    /// <summary>
    /// 取得分頁資訊
    /// </summary>
    /// <param name="page">目前頁數</param>
    /// <param name="pageSize">每頁筆數</param>
    /// <param name="pageCount">總頁數</param>
    /// <returns></returns>
    public static void SetPageInfo(int page, int pageSize, int pageCount)
    {
        Page = page;
        PageSize = pageSize;
        PageCount = pageCount;
    }

    /// <summary>
    /// 取得表頭分頁資訊
    /// </summary>
    /// <param name="page">目前頁數</param>
    /// <param name="pageSize">每頁筆數</param>
    /// <param name="pageCount">總頁數</param>
    /// <returns></returns>
    public static void SetPageMasterInfo(int page, int pageSize, int pageCount)
    {
        PageMaster = page;
        PageSizeMaster = pageSize;
        PageCountMaster = pageCount;
    }

    /// <summary>
    /// 取得明細分頁資訊
    /// </summary>
    /// <param name="page">目前頁數</param>
    /// <param name="pageSize">每頁筆數</param>
    /// <param name="pageCount">總頁數</param>
    /// <returns></returns>
    public static void SetPageDetailInfo(int page, int pageSize, int pageCount)
    {
        PageDetail = page;
        PageSizeDetail = pageSize;
        PageCountDetail = pageCount;
    }

    /// <summary>
    /// 設定動作資訊
    /// </summary>
    /// <param name="action">表單動作</param>
    /// <param name="cardSize">卡片寛度大小</param>
    /// <param name="id">Id</param>
    /// <param name="subActionName">子標題名稱</param>
    public static void SetActionInfo(enAction action, enCardSize cardSize, int id = 0, string subActionName = "")
    {
        if (action == enAction.CreateEdit && id == 0) action = enAction.Create;
        if (action == enAction.CreateEdit && id > 0) action = enAction.Edit;
        List<SelectListItem> actionList = new List<SelectListItem>();
        var actinList = Enum.GetValues(typeof(enAction)).Cast<enAction>().ToList();
        foreach (var item in actinList)
        {
            string str_text = GetActionName(item);
            string str_value = Enum.GetName(typeof(enAction), item);
            actionList.Add(new SelectListItem() { Text = str_text, Value = str_value });
        }
        ActionNo = Enum.GetName(typeof(enAction), action);
        var data = actionList.Where(m => m.Value == ActionNo).FirstOrDefault();
        ActionName = (data == null) ? ActionNo : data.Text;
        string str_size = Enum.GetName(typeof(enCardSize), cardSize).ToLower();
        CardSize = $"card-size-{str_size}";
        SubActionName = subActionName;
    }
    /// <summary>
    /// 取得動作名稱
    /// </summary>
    /// <param name="action">動作</param>
    /// <returns></returns>
    public static string GetActionName(enAction action)
    {
        if (action == enAction.None) return "";
        if (action == enAction.Home) return "首頁";
        if (action == enAction.Init) return "初始化";
        if (action == enAction.Dashboard) return "儀表板";
        if (action == enAction.Index) return "列表";
        if (action == enAction.Detail) return "明細";
        if (action == enAction.Create) return "新增";
        if (action == enAction.Current) return "目前功能";
        if (action == enAction.Edit) return "修改";
        if (action == enAction.Export) return "匯出";
        if (action == enAction.Delete) return "刪除";
        if (action == enAction.Search) return "查詢";
        if (action == enAction.Select) return "選取";
        if (action == enAction.Sort) return "排序";
        if (action == enAction.First) return "首筆";
        if (action == enAction.Last) return "末筆";
        if (action == enAction.Next) return "下筆";
        if (action == enAction.Prior) return "上筆";
        if (action == enAction.List) return "瀏覽";
        if (action == enAction.Preview) return "預覽";
        if (action == enAction.print) return "列印";
        if (action == enAction.Upload) return "上傳";
        if (action == enAction.UploadImage) return "上傳圖片";
        if (action == enAction.UploadFile) return "上傳檔案";
        if (action == enAction.Download) return "下載";
        if (action == enAction.Confirm) return "確認";
        if (action == enAction.Undo) return "取消確認";
        if (action == enAction.Cancel) return "作廢";
        if (action == enAction.Approve) return "核准";
        if (action == enAction.Reject) return "駁回";
        if (action == enAction.Login) return "登入";
        if (action == enAction.Register) return "註冊";
        if (action == enAction.Forget) return "忘記密碼";
        if (action == enAction.Reset) return "重設";
        if (action == enAction.ResetPassword) return "重設密碼";
        return "";
    }
    #endregion
    #region 程式備用 Session 相關
    /// <summary>
    /// 記錄 Id
    /// </summary>
    /// <value></value>
    public static int Id
    {
        get
        {
            int int_value = 0;
            string str_value = GetSessionValue<int>("Id", 0).ToString();
            if (!int.TryParse(str_value, out int_value)) int_value = 0;
            return int_value;
        }
        set { SetSessionValue<int>("Id", value); }
    }
    /// <summary>
    /// 字串變數1
    /// </summary>
    /// <value></value>
    public static string StringValue1
    {
        get { return GetSessionValue<string>("StringValue1", "").ToString(); }
        set { SetSessionValue<string>("StringValue1", value); }
    }
    /// <summary>
    /// 字串變數2
    /// </summary>
    /// <value></value>
    public static string StringValue2
    {
        get { return GetSessionValue<string>("StringValue2", "").ToString(); }
        set { SetSessionValue<string>("StringValue2", value); }
    }
    /// <summary>
    /// 字串變數3
    /// </summary>
    /// <value></value>
    public static string StringValue3
    {
        get { return GetSessionValue<string>("StringValue3", "").ToString(); }
        set { SetSessionValue<string>("StringValue3", value); }
    }
    /// <summary>
    /// 數字變數1
    /// </summary>
    /// <value></value>
    public static int IntValue1
    {
        get
        {
            int int_value = 0;
            string str_value = GetSessionValue<int>("IntValue1", 0).ToString();
            if (!int.TryParse(str_value, out int_value)) int_value = 0;
            return int_value;
        }
        set { SetSessionValue<int>("IntValue1", value); }
    }
    /// <summary>
    /// 數字變數2
    /// </summary>
    /// <value></value>
    public static int IntValue2
    {
        get
        {
            int int_value = 0;
            string str_value = GetSessionValue<int>("IntValue2", 0).ToString();
            if (!int.TryParse(str_value, out int_value)) int_value = 0;
            return int_value;
        }
        set { SetSessionValue<int>("IntValue2", value); }
    }
    /// <summary>
    /// 數字變數3
    /// </summary>
    /// <value></value>
    public static int IntValue3
    {
        get
        {
            int int_value = 0;
            string str_value = GetSessionValue<int>("IntValue3", 0).ToString();
            if (!int.TryParse(str_value, out int_value)) int_value = 0;
            return int_value;
        }
        set { SetSessionValue<int>("IntValue3", value); }
    }
    #endregion
    #region 購物商城 Session 相關
    /// <summary>
    /// Category No
    /// </summary>
    /// <value></value>
    public static string CategoryNo
    {
        get { return GetSessionValue<string>("CategoryNo", "").ToString(); }
        set { SetSessionValue<string>("CategoryNo", value); }
    }

    /// <summary>
    /// Category Name
    /// </summary>
    /// <value></value>
    public static string CategoryName
    {
        get { return GetSessionValue<string>("CategoryName", "").ToString(); }
        set { SetSessionValue<string>("CategoryName", value); }
    }

    /// <summary>
    /// Category Image
    /// </summary>
    /// <value></value>
    public static string CategoryImage
    {
        get { return GetSessionValue<string>("CategoryImage", "").ToString(); }
        set { SetSessionValue<string>("CategoryImage", value); }
    }
    #endregion
    #region 公用 Session 相關
    /// <summary>
    /// Message Text
    /// </summary>
    /// <value></value>
    public static string MessageText
    {
        get { return GetSessionValue<string>("MessageText", "").ToString(); }
        set { SetSessionValue<string>("MessageText", value); }
    }
    /// <summary>
    /// ErrorMessage Text
    /// </summary>
    /// <value></value>
    public static string ErrorMessage
    {
        get { return GetSessionValue<string>("ErrorMessage", "").ToString(); }
        set { SetSessionValue<string>("ErrorMessage", value); }
    }
    #endregion
}
#region 枚舉類型
/// <summary>
/// 表單動作枚舉類型
/// </summary>
public enum enAction
{
    None,
    Home,
    Init,
    Dashboard,
    Index,
    List,
    Detail,
    Create,
    Edit,
    Export,
    CreateEdit,
    Current,
    Delete,
    Search,
    Sort,
    First,
    Last,
    Next,
    Preview,
    Prior,
    print,
    Select,
    Upload,
    UploadImage,
    UploadFile,
    Download,
    Confirm,
    Undo,
    Cancel,
    Approve,
    Reject,
    Login,
    Register,
    Forget,
    Reset,
    ResetPassword
}

/// <summary>
/// 卡片寛度大小枚舉類型
/// </summary>
public enum enCardSize
{
    Small,
    Medium,
    Larget,
    Max
}
#endregion