namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaProductStatus))]
    public partial class ProductStatus
    {

    }
}

public partial class z_metaProductStatus
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "狀態編號")]
    public string? StatusNo { get; set; }
    [Display(Name = "狀態名稱")]
    public string? StatusName { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}
