/// <summary>
/// 頁面大小模型
/// </summary>
public class pageSizeModel : BaseClass
{
    /// <summary>
    /// 頁面大小
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// 頁面大小選單
    /// </summary>
    public List<SelectListItem> PageSizeList { get; set; } = new List<SelectListItem>()
    {
        new SelectListItem() { Text = "10", Value = "10" },
        new SelectListItem() { Text = "20", Value = "20" },
        new SelectListItem() { Text = "50", Value = "50" },
        new SelectListItem() { Text = "100", Value = "100" },
        new SelectListItem() { Text = "200", Value = "200" },
        new SelectListItem() { Text = "500", Value = "500" },
        new SelectListItem() { Text = "1000", Value = "1000" },
    };
}