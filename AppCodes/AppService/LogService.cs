using NuGet.Common;

/// <summary>
/// 日誌服務類別
/// </summary>
public static class LogService
{
    /// <summary>
    /// 新增日誌記錄
    /// </summary>
    public static void AddLog(LogModel logModel)
    {
        var model = new Logs();
        string logCode = logModel.LogCode;
        using var log = new z_sqlLogCodes();
        var data = log.GetData(logCode);
        if (data == null)
        {
            model.LogLevel = Enum.GetName(typeof(enLogLevel), logModel.LogLevel);
            model.LogInterval = Enum.GetName(typeof(enLogInterval), logModel.LogInterval);
        }
        else
        {
            model.LogLevel = data.LogLevel;
            model.LogInterval = data.LogInterval;
        }
        model.Id = 0;
        model.KeyNo = Guid.NewGuid().ToString().ToLower();
        model.LogDate = DateTime.Today;
        model.LogTime = DateTime.Now;
        model.LastDate = DateTime.Today;
        model.LastTime = DateTime.Now;
        model.LogCode = logModel.LogCode;
        model.LogMessage = logModel.LogMessage;
        model.UserNo = SessionService.UserNo;
        model.UserName = SessionService.UserName;
        model.TargetNo = logModel.TargetNo;
        model.IpAddress = AppService.GetIpAddress();
        model.LogNo = logModel.LogNo;
        model.LogQty = logModel.LogQty;
        model.Remark = "";

        using var sqlData = new z_sqlLogs();
        try
        {
            sqlData.AddLog(model);
        }
        catch (Exception ex)
        {
            string errMsg = $"新增日誌記錄失敗，原因：{ex.Message}";
        }
    }
}

/// <summary>
/// 日誌資料模型
/// </summary>
public class LogModel
{
    /// <summary>
    /// 日誌分類
    /// </summary>
    public string? LogCode { get; set; } = "None";
    /// <summary>
    /// 日誌等級
    /// </summary>
    public enLogLevel LogLevel { get; set; } = enLogLevel.Information;
    /// <summary>
    /// 日誌間隔
    /// </summary>
    public enLogInterval LogInterval { get; set; } = enLogInterval.Day;
    /// <summary>
    /// 對象代號
    /// </summary>
    public string? TargetNo { get; set; } = "";
    /// <summary>
    /// 訊息內容
    /// </summary>
    public string? LogMessage { get; set; } = "";
    /// <summary>
    /// 記錄編號
    /// </summary>
    public string? LogNo { get; set; } = "";
    /// <summary>
    /// 記錄數量
    /// </summary>
    public int LogQty { get; set; } = 1;
}

/// <summary>
/// 日誌頻率
/// </summary>
public enum enLogInterval
{
    /// <summary>
    /// 未設定
    /// </summary>
    None,
    /// <summary>
    /// 每分鐘
    /// </summary>
    Minute,
    /// <summary>
    /// 每小時
    /// </summary>
    Hour,
    /// <summary>
    /// 每日
    /// </summary>
    Day,
    /// <summary>
    /// 每週
    /// </summary>
    Week,
    /// <summary>
    /// 每月
    /// </summary>
    Month,
    /// <summary>
    /// 每年
    /// </summary>
    Year
}

/// <summary>
/// 日誌等級
/// </summary>
public enum enLogLevel
{
    /// <summary>
    /// 追蹤
    /// </summary>
    Trace,
    /// <summary>
    /// 除錯
    /// </summary>
    Debug,
    /// <summary>
    /// 資訊
    /// </summary>
    Information,
    /// <summary>
    /// 警告
    /// </summary>
    Warning,
    /// <summary>
    /// 錯誤
    /// </summary>
    Error,
    /// <summary>
    /// 致命錯誤
    /// </summary>
    Fatal
}