using System;
using System.Collections.Generic;

namespace powererp.Models;

public partial class EmployeeSchools
{
    public int Id { get; set; }

    public string? EmpNo { get; set; }

    public string? EducationName { get; set; }

    public string? SchoolName { get; set; }

    public string? SubjectName { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool IsGraduate { get; set; }

    public string? Remark { get; set; }
}
