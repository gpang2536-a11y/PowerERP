using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaTestimonials))]
    public partial class Testimonials
    {
        [NotMapped]
        [Display(Name = "大頭照")]
        public string? ImageUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(UserNo))
                {
                    // 跨平台實體檔案路徑
                    string filePath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "images",
                        "testimonials",
                        $"{UserNo}.jpg"
                    );

                    // 檔案存在
                    if (File.Exists(filePath))
                    {
                        // URL 路徑一定用 /
                        return $"~/images/testimonials/{UserNo}.jpg";
                    }
                }

                // 預設圖片
                return "~/images/testimonials/user.jpg";
            }
        }

        [NotMapped]
        [Display(Name = "訊息內容(部份內容)")]
        public string? ShortMessageText
        {
            get
            {
                if (!string.IsNullOrEmpty(MessageText) && MessageText.Length > 20)
                {
                    return MessageText.Substring(0, 20) + "...";
                }
                else
                {
                    return MessageText;
                }
            }
        }
    }
}

public class z_metaTestimonials
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "提交日期")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    public DateTime? SendDate { get; set; }
    [Display(Name = "使用者編號")]
    public string? UserNo { get; set; }
    [Display(Name = "使用者姓名")]
    public string? UserName { get; set; }
    [Display(Name = "職稱")]
    public string? TitleName { get; set; }
    [Display(Name = "星數")]
    public int StarCount { get; set; }
    [Display(Name = "訊息內容")]
    public string? MessageText { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}
