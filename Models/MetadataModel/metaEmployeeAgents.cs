using System;
using System.Collections.Generic;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaEmployeeAgents))]
    public partial class EmployeeAgents
    {
        [NotMapped]
        [Display(Name = "員工姓名")]
        public string? EmpName { get; set; }
        [NotMapped]
        [Display(Name = "代理姓名")]
        public string? AgentName { get; set; }
        [NotMapped]
        [Display(Name = "部門名稱")]
        public string? DeptName { get; set; }
        [NotMapped]
        [Display(Name = "職稱")]
        public string? TitleName { get; set; }
    }
}

public class z_metaEmployeeAgents
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "啟用")]
    public bool IsEnabled { get; set; }
    [Display(Name = "員工編號")]
    public string? EmpNo { get; set; }
    [Display(Name = "代理工號")]
    public string? AgentNo { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}
