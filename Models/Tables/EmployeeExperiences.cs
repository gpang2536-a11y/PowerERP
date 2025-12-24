using System;
using System.Collections.Generic;

namespace powererp.Models;

public partial class EmployeeExperiences
{
    public int Id { get; set; }

    public string? EmpNo { get; set; }

    public string? CompName { get; set; }

    public string? DeptName { get; set; }

    public string? TitleName { get; set; }

    public string? BossName { get; set; }

    public int? Salary { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? QuitReason { get; set; }

    public string? Remark { get; set; }
}
