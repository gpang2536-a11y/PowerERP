using System;
using System.Collections.Generic;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaTeams))]
    public partial class Teams
    {
        [NotMapped]
        [Display(Name = "性別")]
        public string? GenderName { get; set; }

        [NotMapped]
        [Display(Name = "大頭照")]
        public string? ImageUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(TeamNo))
                {
                    // 將 wwwroot/images/teams 與 TeamNo 組合成跨平台路徑
                    string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "teams");
                    string filePath = Path.Combine(folderPath, $"{TeamNo}.jpg");

                    // 檔案存在
                    if (File.Exists(filePath))
                    {
                        // 返回給前端的 URL 使用 / 作為分隔符號
                        return $"/images/teams/{TeamNo}.jpg";
                    }
                }
                return "/images/nouser.jpg";
            }
        }

        [NotMapped]
        [Display(Name = "內容明細(部份內容)")]
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

public class z_metaTeams
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "排序")]
    public string? SortNo { get; set; }
    [Display(Name = "團隊編號")]
    public string? TeamNo { get; set; }
    [Display(Name = "團隊名稱")]
    public string? TeamName { get; set; }
    [Display(Name = "英文姓名")]
    public string? EngName { get; set; }
    [Display(Name = "性別")]
    public string? GenderCode { get; set; }
    [Display(Name = "部門")]
    public string? DeptName { get; set; }
    [Display(Name = "職稱")]
    public string? TitleName { get; set; }
    [Display(Name = "Twitter")]
    public string? TwitterUrl { get; set; }
    [Display(Name = "Facebook")]
    public string? FacebookUrl { get; set; }
    [Display(Name = "Linkedin")]
    public string? LinkedinUrl { get; set; }
    [Display(Name = "Instagram")]
    public string? InstagramUrl { get; set; }
    [Display(Name = "Skype")]
    public string? SkypeUrl { get; set; }
    [Display(Name = "電子信箱")]
    public string? ContactEmail { get; set; }
    [Display(Name = "內容明細")]
    public string? DetailText { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}