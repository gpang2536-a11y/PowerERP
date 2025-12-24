using System;
using System.Collections.Generic;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaDepartments))]
    public partial class Departments
    {
        [NotMapped]
        [Display(Name = "部門主管")]
        public string? BossName { get; set; }
    }
}

public class z_metaDepartments
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "部門代號")]
    [Required(ErrorMessage = "{0}不可空白!!")]
    public string? DeptNo { get; set; }
    [Display(Name = "部門名稱")]
    [Required(ErrorMessage = "{0}不可空白!!")]
    public string? DeptName { get; set; }
    [Display(Name = "主管代號")]
    [Required(ErrorMessage = "{0}不可空白!!")]
    public string? BossNo { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}