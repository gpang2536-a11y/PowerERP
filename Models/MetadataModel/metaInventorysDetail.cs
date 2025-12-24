namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaInventorysDetail))]
    public partial class InventorysDetail
    {
        [NotMapped]
        [Display(Name = "商品名稱")]
        public string? ProdName { get; set; }
    }
}

public class z_metaInventorysDetail
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "父階鍵值")]
    public string? ParentNo { get; set; }
    [Display(Name = "商品編號")]
    public string? ProductNo { get; set; }
    [Display(Name = "數量")]
    public int Qty { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}