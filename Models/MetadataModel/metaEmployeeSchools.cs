using System;
using System.Collections.Generic;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaEmployeeSchools))]
    public partial class EmployeeSchools
    {
        [NotMapped]
        [Display(Name = "員工姓名")]
        public string? EmpName { get; set; }
    }
}

public class z_metaEmployeeSchools
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "員工編號")]
    public string? EmpNo { get; set; }
    [Display(Name = "學歷")]
    public string? EducationName { get; set; }
    [Display(Name = "學校名稱")]
    public string? SchoolName { get; set; }
    [Display(Name = "科系名稱")]
    public string? SubjectName { get; set; }
    [Display(Name = "入學日期")]
    public DateTime? StartDate { get; set; }
    [Display(Name = "畢業日期")]
    public DateTime? EndDate { get; set; }
    [Display(Name = "畢業")]
    public bool IsGraduate { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}
