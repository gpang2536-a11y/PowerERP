using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaOvertimeTypes))]
    public partial class OvertimeTypes
    {

    }
}

public class z_metaOvertimeTypes
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "唯一鍵值")]
    public string? BaseNo { get; set; }
    [Display(Name = "類別代號")]
    [Required(ErrorMessage = "{0}不可空白!!")]
    public string? TypeNo { get; set; }
    [Display(Name = "類別名稱")]
    public string? TypeName { get; set; }
    [Display(Name = "開始小時")]
    [Required(ErrorMessage = "{0}不可空白!!")]
    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public int StartHour { get; set; }
    [Display(Name = "結束小時")]
    [Required(ErrorMessage = "{0}不可空白!!")]
    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public int EndHour { get; set; }
    [Display(Name = "加班倍率")]
    [Required(ErrorMessage = "{0}不可空白!!")]
    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal Rates { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}