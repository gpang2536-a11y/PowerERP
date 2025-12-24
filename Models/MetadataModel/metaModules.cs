using System;
using System.Collections.Generic;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaModules))]
    public partial class Modules
    {
        [NotMapped]
        [Display(Name = "角色名稱")]
        public string? RoleName { get; set; }

    }
}
public class z_metaModules
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "啟用")]
    public bool IsEnabled { get; set; }
    [Display(Name = "簽核")]
    public bool IsWorkflow { get; set; }
    [Display(Name = "角色編號")]
    public string? RoleNo { get; set; }
    [Display(Name = "排序")]
    public string? SortNo { get; set; }
    [Display(Name = "模組編號")]
    public string? ModuleNo { get; set; }
    [Display(Name = "模組名稱")]
    public string? ModuleName { get; set; }
    [Display(Name = "圖示名稱")]
    public string? IconName { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}
