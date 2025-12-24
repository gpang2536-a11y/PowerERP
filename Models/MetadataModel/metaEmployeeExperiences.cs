using System;
using System.Collections.Generic;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaEmployeeExperiences))]
    public partial class EmployeeExperiences
    {
        [NotMapped]
        [Display(Name = "員工姓名")]
        public string? EmpName { get; set; }
    }
}

public class z_metaEmployeeExperiences
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "員工編號")]
    public string? EmpNo { get; set; }
    [Display(Name = "公司名稱")]
    public string? CompName { get; set; }
    [Display(Name = "部門名稱")]
    public string? DeptName { get; set; }
    [Display(Name = "職稱")]
    public string? TitleName { get; set; }
    [Display(Name = "主管姓名")]
    public string? BossName { get; set; }
    [Display(Name = "薪資")]
    public int? Salary { get; set; }
    [Display(Name = "到職日期")]
    public DateTime? StartDate { get; set; }
    [Display(Name = "離職日期")]
    public DateTime? EndDate { get; set; }
    [Display(Name = "離職原因")]
    public string? QuitReason { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}
