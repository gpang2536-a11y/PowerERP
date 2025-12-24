using System;
using System.Collections.Generic;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaOvertimes))]
    public partial class Overtimes
    {
        [NotMapped]
        [Display(Name = "加班類別")]
        public string? TypeName { get; set; }
        [NotMapped]
        [Display(Name = "員工姓名")]
        public string? EmpName { get; set; }
    }
}

public class z_metaOvertimes
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "唯一鍵值")]
    public string? BaseNo { get; set; }
    [Display(Name = "加班單號")]
    public string? SheetNo { get; set; }
    [Display(Name = "加班日期")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "{0}不可空白!!")]
    public DateTime? SheetDate { get; set; }
    [Display(Name = "員工編號")]
    [Required(ErrorMessage = "{0}不可空白!!")]
    public string? EmpNo { get; set; }
    [Display(Name = "部門編號")]
    public string? DeptNo { get; set; }
    [Display(Name = "部門名稱")]
    public string? DeptName { get; set; }
    [Display(Name = "加班事由")]
    [Required(ErrorMessage = "{0}不可空白!!")]
    public string? ReasonText { get; set; }
    [Display(Name = "類別代號")]
    [Required(ErrorMessage = "{0}不可空白!!")]
    public string? TypeNo { get; set; }
    [Display(Name = "開始時間")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "{0}不可空白!!")]
    public DateTime? StartTime { get; set; }
    [Display(Name = "結束時間")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "{0}不可空白!!")]
    public DateTime? EndTime { get; set; }
    [Display(Name = "加班時數")]
    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public int Hours { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}