using System;
using System.Collections.Generic;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaLogCodes))]
    public partial class LogCodes
    {

    }
}

public class z_metaLogCodes
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "類別代號")]
    public string? CodeNo { get; set; }
    [Display(Name = "類別名稱")]
    public string? CodeName { get; set; }
    [Display(Name = "日誌等級")]
    public string? LogLevel { get; set; }
    [Display(Name = "日誌頻率")]
    public string? LogInterval { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}