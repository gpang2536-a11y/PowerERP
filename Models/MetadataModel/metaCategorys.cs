
namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaCategorys))]
    public partial class Categorys
    {

    }
}

public class z_metaCategorys
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "是否啟用")]
    public bool IsEnabled { get; set; }
    [Display(Name = "是否類別")]
    public bool IsCategory { get; set; }
    [Display(Name = "上層編號")]
    public string? ParentNo { get; set; }
    [Display(Name = "類別編號")]
    public string? CategoryNo { get; set; }
    [Display(Name = "排序編號")]
    public string? SortNo { get; set; }
    [Display(Name = "類別名稱")]
    public string? CategoryName { get; set; }
    [Display(Name = "路由名稱")]
    public string? RouteName { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}