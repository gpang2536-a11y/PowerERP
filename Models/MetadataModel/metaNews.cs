using System;
using System.Collections.Generic;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaNews))]
    public partial class News
    {
        [NotMapped]
        [Display(Name = "類別")]
        public string? CodeName { get; set; }
        [NotMapped]
        [Display(Name = "圖片")]
        public string ImageUrl { get { return $"~/images/news/{Id}.jpg"; } }
        [NotMapped]
        [Display(Name = "部份訊息內容")]
        public string? ShortDetail
        {
            get
            {
                if (string.IsNullOrEmpty(DetailText)) return "";
                if (DetailText.Length <= 20) return DetailText;
                return DetailText.Substring(0, 20) + "...";
            }
        }
    }
}
public class z_metaNews
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "類別代號")]
    public string? CodeNo { get; set; }
    [Display(Name = "發布日期")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    public DateTime PublishDate { get; set; }
    [Display(Name = "標題")]
    [Required(ErrorMessage = "{0}不可空白!!")]
    public string? HeaderName { get; set; }
    [Display(Name = "內容")]
    public string? DetailText { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}