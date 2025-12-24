using System;
using System.Collections.Generic;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaLanguages))]
    public partial class Languages
    {

    }
}

public class z_metaLanguages
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "語言編號")]
    [Required(ErrorMessage = "{0}不可空白!!")]
    public string? LangNo { get; set; }
    [Display(Name = "語言名稱")]
    [Required(ErrorMessage = "{0}不可空白!!")]
    public string? LangName { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}
