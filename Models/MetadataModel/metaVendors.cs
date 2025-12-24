using System;
using System.Collections.Generic;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaVendors))]
    public partial class Vendors
    {
        [NotMapped]
        [Display(Name = "公司類別")]
        public string? CodeName { get; set; }
    }
}

public class z_metaVendors
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "預設")]
    public bool IsDefault { get; set; }
    [Display(Name = "啟用")]
    public bool IsEnabled { get; set; }
    [Display(Name = "類別代號")]
    public string? CodeNo { get; set; }
    [Display(Name = "廠商編號")]
    [Required(ErrorMessage = "{0}不可空白!!")]
    public string? CompNo { get; set; }
    [Display(Name = "廠商名稱")]
    [Required(ErrorMessage = "{0}不可空白!!")]
    public string? CompName { get; set; }
    [Display(Name = "廠商簡稱")]
    [Required(ErrorMessage = "{0}不可空白!!")]
    public string? ShortName { get; set; }
    [Display(Name = "英文名稱")]
    public string? EngName { get; set; }
    [Display(Name = "英文簡稱")]
    public string? EngShortName { get; set; }
    [Display(Name = "註冊日期")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
    public DateTime RegisterDate { get; set; }
    [Display(Name = "負責人")]
    public string? BossName { get; set; }
    [Display(Name = "連絡人")]
    public string? ContactName { get; set; }
    [Display(Name = "公司電話")]
    public string? CompTel { get; set; }
    [Display(Name = "連絡電話")]
    public string? ContactTel { get; set; }
    [Display(Name = "傳真電話")]
    public string? CompFax { get; set; }
    [Display(Name = "統一編號")]
    public string? CompID { get; set; }
    [Display(Name = "電子信箱")]
    [EmailAddress(ErrorMessage = "電子信箱格式錯誤!!")]
    public string? ContactEmail { get; set; }
    [Display(Name = "公司地址")]
    public string? CompAddress { get; set; }
    [Display(Name = "公司網址")]
    [Url(ErrorMessage = "網址格式錯誤!!")]
    public string? CompUrl { get; set; }
    [Display(Name = "Twitter")]
    [Url(ErrorMessage = "網址格式錯誤!!")]
    public string? TwitterUrl { get; set; }
    [Display(Name = "Facebook")]
    [Url(ErrorMessage = "網址格式錯誤!!")]
    public string? FacebookUrl { get; set; }
    [Display(Name = "Instagram")]
    [Url(ErrorMessage = "網址格式錯誤!!")]
    public string? InstagramUrl { get; set; }
    [Display(Name = "Skype")]
    [Url(ErrorMessage = "網址格式錯誤!!")]
    public string? SkypeUrl { get; set; }
    [Display(Name = "Linkedin")]
    [Url(ErrorMessage = "網址格式錯誤!!")]
    public string? LinkedinUrl { get; set; }
    [Display(Name = "緯度")]
    [DisplayFormat(DataFormatString = "{0:N15}", ApplyFormatInEditMode = true)]
    public decimal Latitude { get; set; }
    [Display(Name = "經度")]
    [DisplayFormat(DataFormatString = "{0:N15}", ApplyFormatInEditMode = true)]
    public decimal Longitude { get; set; }
    [Display(Name = "公司簡介")]
    public string? AboutusText { get; set; }
    [Display(Name = "支援說明")]
    public string? SupportText { get; set; }
    [Display(Name = "退貨說明")]
    public string? ReturnText { get; set; }
    [Display(Name = "出貨說明")]
    public string? ShippingText { get; set; }
    [Display(Name = "付款說明")]
    public string? PaymentText { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}
