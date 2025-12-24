using System;
using System.Collections.Generic;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaServices))]
    public partial class Services
    {
        [NotMapped]
        [Display(Name = "內容描述(部份內容)")]
        public string? ShortDescription
        {
            get
            {
                if (DetailName != null && DetailName.Length > 20)
                {
                    return DetailName.Substring(0, 20) + "...";
                }
                else
                {
                    return DetailName;
                }
            }
        }
    }
}

public class z_metaServices
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "啟用")]
    public bool IsEnabled { get; set; }
    [Display(Name = "排序")]
    public string? SortNo { get; set; }
    [Display(Name = "標題文字")]
    public string? HeaderName { get; set; }
    [Display(Name = "內容描述")]
    public string? DetailName { get; set; }
    [Display(Name = "圖示名稱")]
    public string? ImageUrl { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}