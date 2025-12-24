using System;
using System.Collections.Generic;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaRoles))]
    public partial class Roles
    {

    }
}

public class z_metaRoles
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "啟用")]
    public bool IsEnabled { get; set; }
    [Display(Name = "代號")]
    [Required(ErrorMessage = "{0}不可空白!!")]
    public string? RoleNo { get; set; }
    [Display(Name = "名稱")]
    [Required(ErrorMessage = "{0}不可空白!!")]
    public string? RoleName { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}
