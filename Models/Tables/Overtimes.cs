using System;
using System.Collections.Generic;

namespace powererp.Models;

public partial class Overtimes
{
    public int Id { get; set; }

    public string? BaseNo { get; set; }

    public string? SheetNo { get; set; }

    public DateTime? SheetDate { get; set; }

    public string? EmpNo { get; set; }

    public string? DeptNo { get; set; }

    public string? DeptName { get; set; }

    public string? ReasonText { get; set; }

    public string? TypeNo { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int Hours { get; set; }

    public string? Remark { get; set; }
}
