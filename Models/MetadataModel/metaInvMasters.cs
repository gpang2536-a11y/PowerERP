namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaInvMasters))]
    public partial class InvMasters
    {
        [NotMapped]
        [Display(Name = "列號")]
        public int RowNo { get; set; }
        [NotMapped]
        [Display(Name = "商品名稱")]
        public string? ProductName { get; set; }
    }
}

public class z_metaInvMasters
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "唯一鍵值")]
    public string? BaseNo { get; set; }
    [Display(Name = "商品編號")]
    public string? ProductNo { get; set; }
    [Display(Name = "總庫存量")]
    public int Qty { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}
