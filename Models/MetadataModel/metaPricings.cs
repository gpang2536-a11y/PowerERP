namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaPricings))]
    public partial class Pricings
    {

    }
}

public class z_metaPricings
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "是否啟用")]
    public bool IsEnabled { get; set; }
    [Display(Name = "是否進階")]
    public bool IsAdvanced { get; set; }
    [Display(Name = "是否推薦")]
    public bool IsRecommend { get; set; }
    [Display(Name = "排序")]
    public string? SortNo { get; set; }
    [Display(Name = "方案編號")]
    public string? PricingNo { get; set; }
    [Display(Name = "方案名稱")]
    public string? PricingName { get; set; }
    [Display(Name = "鎖售價格")]
    public int ProdPrice { get; set; }
    [Display(Name = "繳費週期")]
    public string? CycleName { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}