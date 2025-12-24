/// <summary>
/// 錯誤訊息模式
/// </summary>
public enum enErrorMessageMode
{
    /// <summary>
    /// 無此功能
    /// </summary>
    None = 0,
    /// <summary>
    /// 資料找不到
    /// </summary>
    NotFound = 1,
    /// <summary>
    /// 此功能未經授權
    /// </summary>
    Unauthorized = 2,
    /// <summary>
    /// 此功能禁止使用
    /// </summary>
    Forbidden = 3,
    /// <summary>
    /// 錯誤操作
    /// </summary>
    ErrorOperation = 4,
    /// <summary>
    /// 其它問題
    /// </summary>
    Other = 5
}