using NuGet.Protocol.Plugins;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaMessages))]
    public partial class Messages
    {
        [NotMapped]
        [Display(Name = "類別")]
        public string? CodeName { get; set; }
        [NotMapped]
        [Display(Name = "發送者")]
        public string? SenderName { get; set; }
        [NotMapped]
        [Display(Name = "接收者")]
        public string? ReceiverName { get; set; }
        [NotMapped]
        [Display(Name = "發送者圖片")]
        public string SenderImageUrl
        {
            get
            {
                string imageUrl = "";
                if (string.IsNullOrEmpty(SenderNo))
                    imageUrl = "~/images/nouser.jpg";
                else
                    imageUrl = $"/images/users/{SenderNo}.jpg";
                imageUrl += "?t=" + DateTime.Now.ToString("yyyyMMddHHmmss");
                return imageUrl;
            }
        }
    }
}

public class z_metaMessages
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "已讀")]
    public bool IsRead { get; set; }
    [Display(Name = "類別")]
    public string? CodeNo { get; set; }
    [Display(Name = "發送者")]
    public string? SenderNo { get; set; }
    [Display(Name = "接收者")]
    public string? ReceiverNo { get; set; }
    [Display(Name = "發送時間")]
    public DateTime SendTime { get; set; }
    [Display(Name = "標題")]
    public string? HeaderText { get; set; }
    [Display(Name = "內容")]
    public string? MessageText { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}