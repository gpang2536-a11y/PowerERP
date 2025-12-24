using System;
using System.Collections.Generic;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaPrograms))]
    public partial class Programs
    {
        [NotMapped]
        [Display(Name = "角色名稱")]
        public string? RoleName { get; set; }
        [NotMapped]
        [Display(Name = "模組名稱")]
        public string? ModuleName { get; set; }
        [NotMapped]
        [Display(Name = "類別名稱")]
        public string? CodeName { get; set; }
        [NotMapped]
        [Display(Name = "圖示名稱")]
        public string? IconName { get; set; }
        [NotMapped]
        [Display(Name = "鎖定名稱")]
        public string? LockName { get; set; }
    }
}

public class z_metaPrograms
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "啟用")]
    public bool IsEnabled { get; set; }
    [Display(Name = "分頁")]
    public bool IsPageSize { get; set; }
    [Display(Name = "搜尋")]
    public bool IsSearch { get; set; }
    [Display(Name = "角色編號")]
    public string? RoleNo { get; set; }
    [Display(Name = "模組編號")]
    public string? ModuleNo { get; set; }
    [Display(Name = "排序")]
    public string? SortNo { get; set; }
    [Display(Name = "程式編號")]
    public string? PrgNo { get; set; }
    [Display(Name = "程式名稱")]
    public string? PrgName { get; set; }
    [Display(Name = "類別代號")]
    public string? CodeNo { get; set; }
    [Display(Name = "鎖定代號")]
    public string? LockNo { get; set; }
    [Display(Name = "區域名稱")]
    public string? AreaName { get; set; }
    [Display(Name = "控制器名稱")]
    public string? ControllerName { get; set; }
    [Display(Name = "動作名稱")]
    public string? ActionName { get; set; }
    [Display(Name = "參數值")]
    public string? ParmValue { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}
