
namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaProducts))]
    public partial class Products
    {
        [NotMapped]
        [Display(Name = "商品狀態")]
        public string? StatusName { get; set; }
        [NotMapped]
        [Display(Name = "供應商")]
        public string? VendorName { get; set; }
        [NotMapped]
        [Display(Name = "商品分類")]
        public string? CategoryName { get; set; }
        [NotMapped]
        [Display(Name = "照片")]
        public string ImageUrl
        {
            get
            {
                string appPath = Directory.GetCurrentDirectory();
                string imgFile = Path.Combine(appPath, "wwwroot", "images", "products", $"{this.ProdNo}.jpg");
                string fileName = "";
                if (File.Exists(imgFile))
                    fileName = $"~/images/products/{this.ProdNo}.jpg";
                else
                    fileName = "~/images/nopic.jpg";
                fileName = ImageService.GetUniqueFileName(fileName);
                return fileName;
            }
        }
    }
}

public class z_metaProducts
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "啟用")]
    public bool IsEnabled { get; set; }
    [Display(Name = "管理庫存")]
    public bool IsInventory { get; set; }
    [Display(Name = "顯示圖片")]
    public bool IsShowPhoto { get; set; }
    [Display(Name = "商品編號")]
    public string? ProdNo { get; set; }
    [Display(Name = "商品名稱")]
    public string? ProdName { get; set; }
    [Display(Name = "廠牌名稱")]
    public string? BrandName { get; set; }
    [Display(Name = "系列名稱")]
    public string? BrandSeriesName { get; set; }
    [Display(Name = "條碼編號")]
    public string? BarcodeNo { get; set; }
    [Display(Name = "狀態編號")]
    public string? StatusNo { get; set; }
    [Display(Name = "供應商編號")]
    public string? VendorNo { get; set; }
    [Display(Name = "分類編號")]
    public string? CategoryNo { get; set; }
    [Display(Name = "成本單價")]
    public int CostPrice { get; set; }
    [Display(Name = "市場價格")]
    public int MarketPrice { get; set; }
    [Display(Name = "銷售價格")]
    public int SalePrice { get; set; }
    [Display(Name = "折扣價格")]
    public int DiscountPrice { get; set; }
    [Display(Name = "庫存數量")]
    public int InventoryQty { get; set; }
    [Display(Name = "商品說明")]
    public string? ContentText { get; set; }
    [Display(Name = "規格說明")]
    public string? SpecificationText { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}
