namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaInvDetails))]
    public partial class InvDetails
    {
        [NotMapped]
        [Display(Name = "倉庫名稱")]
        public string? WareHouseName { get; set; }
        [NotMapped]
        [Display(Name = "商品名稱")]
        public string? ProductName { get; set; }
    }
}

public partial class z_metaInvDetails
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "倉庫編號")]
    public string? WareHouseNo { get; set; }
    [Display(Name = "商品編號")]
    public string? ProductNo { get; set; }
    [Display(Name = "數量")]
    public int Qty { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}