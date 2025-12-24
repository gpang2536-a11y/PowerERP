using System;
using System.Collections.Generic;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaLogs))]
    public partial class Logs
    {
        [NotMapped]
        [Display(Name = "分類名稱")]
        public string? CodeName { get; set; }
        [NotMapped]
        [Display(Name = "日誌等級")]
        public string? LevelName { get; set; }
        [NotMapped]
        [Display(Name = "日誌頻率")]
        public string? IntervalName { get; set; }
    }
}

public class z_metaLogs
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "主鍵編號")]
    public string? KeyNo { get; set; }
    [Display(Name = "日誌日期")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    public DateTime LogDate { get; set; }
    [Display(Name = "日誌時間")]
    [DisplayFormat(DataFormatString = "{0:HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTime LogTime { get; set; }
    [Display(Name = "最近日期")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    public DateTime LastDate { get; set; }
    [Display(Name = "最近時間")]
    [DisplayFormat(DataFormatString = "{0:HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTime LastTime { get; set; }
    [Display(Name = "日誌類別")]
    public string? LogCode { get; set; }
    [Display(Name = "日誌等級")]
    public string? LogLevel { get; set; }
    [Display(Name = "日誌頻率")]
    public string? LogInterval { get; set; }
    [Display(Name = "日誌訊息")]
    public string? LogMessage { get; set; }
    [Display(Name = "用戶編號")]
    public string? UserNo { get; set; }
    [Display(Name = "用戶姓名")]
    public string? UserName { get; set; }
    [Display(Name = "對象代號")]
    public string? TargetNo { get; set; }
    [Display(Name = "IP位址")]
    public string? IpAddress { get; set; }
    [Display(Name = "記錄編號")]
    public string? LogNo { get; set; }
    [Display(Name = "記錄數量")]
    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public int LogQty { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}
