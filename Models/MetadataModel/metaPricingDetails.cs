namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaPricingDetails))]
    public partial class PricingDetails
    {

    }
}

public class z_metaPricingDetails
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "方案代碼")]
    public string? PricingNo { get; set; }
    [Display(Name = "支援")]
    public bool IsSupport { get; set; }
    [Display(Name = "排序")]
    public string? SortNo { get; set; }
    [Display(Name = "項目名稱")]
    public string? ItemName { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}