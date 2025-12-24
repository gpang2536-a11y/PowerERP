using System;
using System.Collections.Generic;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaSecuritys))]
    public partial class Securitys
    {
        [NotMapped]
        [Display(Name = "角色名稱")]
        public string? RoleName { get; set; }
        [NotMapped]
        [Display(Name = "目標名稱")]
        public string? TargetName { get; set; }
        [NotMapped]
        [Display(Name = "模組名稱")]
        public string? ModuleName { get; set; }
        [NotMapped]
        [Display(Name = "程式名稱")]
        public string? PrgName { get; set; }
    }
}

public class z_metaSecuritys
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "角色編號")]
    public string? RoleNo { get; set; }
    [Display(Name = "目標編號")]
    public string? TargetNo { get; set; }
    [Display(Name = "模組編號")]
    public string? ModuleNo { get; set; }
    [Display(Name = "程式編號")]
    public string? PrgNo { get; set; }
    [Display(Name = "新增")]
    public bool IsAdd { get; set; }
    [Display(Name = "修改")]
    public bool IsEdit { get; set; }
    [Display(Name = "刪除")]
    public bool IsDelete { get; set; }
    [Display(Name = "確認")]
    public bool IsConfirm { get; set; }
    [Display(Name = "取消")]
    public bool IsUndo { get; set; }
    [Display(Name = "作廢")]
    public bool IsCancel { get; set; }
    [Display(Name = "上傳")]
    public bool IsUpload { get; set; }
    [Display(Name = "下載")]
    public bool IsDownload { get; set; }
    [Display(Name = "列印")]
    public bool IsPrint { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}
