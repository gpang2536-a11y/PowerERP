using System;
using System.Collections.Generic;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaCarousels))]
    public partial class Carousels
    {
        [NotMapped]
        [Display(Name = "圖片")]
        public string? PhotoUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(ImageUrl))
                {
                    string filePath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "images",
                        "carousels",
                        ImageUrl
                    );

                    // 檔案存在
                    if (File.Exists(filePath))
                    {
                        // URL 路徑「一定」用 /
                        return $"~/images/carousels/{ImageUrl}";
                    }
                }
                return "~/images/nopic.jpg";
            }
        }

        [NotMapped]
        [Display(Name = "內容描述(部份內容)")]
        public string? ShortDescription
        {
            get
            {
                if (Description != null && Description.Length > 20)
                {
                    return Description.Substring(0, 20) + "...";
                }
                else
                {
                    return Description;
                }
            }
        }
    }
}

public class z_metaCarousels
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "啟用")]
    public bool IsActive { get; set; }
    [Display(Name = "排序")]
    public string? SortNo { get; set; }
    [Display(Name = "標題文字")]
    public string? HeaderName { get; set; }
    [Display(Name = "內容描述")]
    public string? Description { get; set; }
    [Display(Name = "圖片檔名")]
    public string? ImageUrl { get; set; }
    [Display(Name = "更多網址")]
    public string? MoreUrl { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}