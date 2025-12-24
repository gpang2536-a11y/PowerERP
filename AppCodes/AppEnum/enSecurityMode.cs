/// <summary>
/// 權限模式
/// </summary>
public enum enSecurityMode
{
    /// <summary>
    /// 允許權限
    /// </summary>
    Allow = 0,
    /// <summary>
    /// 顯示權限
    /// </summary>
    Display = 1,
    /// <summary>
    /// 新增權限
    /// </summary>
    Add = 2,
    /// <summary>
    /// 修改權限
    /// </summary>
    Edit = 3,
    /// <summary>
    /// 新增或修改權限
    /// </summary>
    AddEdit = 4,
    /// <summary>
    /// 刪除權限
    /// </summary>
    Delete = 5,
    /// <summary>
    /// 審核權限
    /// </summary>
    Confirm = 6,
    /// <summary>
    /// 取消權限
    /// </summary>
    Undo = 7,
    /// <summary>
    /// 作廢權限
    /// </summary>
    Cancel = 8,
    /// <summary>
    /// 上傳檔案權限
    /// </summary>
    Upload = 9,
    /// <summary>
    /// 下載檔案權限
    /// </summary>
    Download = 10,
    /// <summary>
    /// 列印權限
    /// </summary>
    Print = 11
}
