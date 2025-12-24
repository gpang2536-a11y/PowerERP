using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaPhotos))]
    public partial class Photos
    {
        [NotMapped]
        [Display(Name = "圖片")]
        public string? ImageUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(FolderName))
                {
                    // 將 wwwroot/images/portfolios 與 FolderName 組合成跨平台路徑
                    string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "portfolios");
                    string filePath = Path.Combine(folderPath, $"{FolderName}.jpg");

                    // 檔案存在
                    if (File.Exists(filePath))
                    {
                        // 返回給前端的 URL 使用 / 作為分隔符號
                        return $"/images/portfolios/{FolderName}.jpg";
                    }
                }
                return "/images/nopic.jpg";
            }
        }

        [NotMapped]
        [Display(Name = "詳細說明(部份內容)")]
        public string? ShortDetailText
        {
            get
            {
                if (DetailText != null && DetailText.Length > 20)
                {
                    return DetailText.Substring(0, 20) + "...";
                }
                else
                {
                    return DetailText;
                }
            }
        }
    }
}

public class z_metaPhotos
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "分類代號")]
    public string? CodeNo { get; set; }
    [Display(Name = "檔案名稱")]
    public string? FolderName { get; set; }
    [Display(Name = "圖片名稱")]
    public string? PhotoName { get; set; }
    [Display(Name = "銷售價格")]
    public string? PriceName { get; set; }
    [Display(Name = "銷售日期")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    public DateOnly SaleDate { get; set; }
    [Display(Name = "詳細說明")]
    public string? DetailText { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}