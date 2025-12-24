using System;
using System.Collections.Generic;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaCodeBases))]
    public partial class CodeBases
    {

    }
}

public class z_metaCodeBases
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "後台管理")]
    public bool IsAdmin { get; set; }
    [Display(Name = "類別代號")]
    public string? BaseNo { get; set; }
    [Display(Name = "類別名稱")]
    public string? BaseName { get; set; }
    [Display(Name = "預設值")]
    public string? DefaultValue { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}