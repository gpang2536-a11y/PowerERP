using System;
using System.Collections.Generic;

namespace powererp.Models;

public partial class Departments
{
    public int Id { get; set; }

    public string? DeptNo { get; set; }

    public string? DeptName { get; set; }

    public string? BossNo { get; set; }

    public string? Remark { get; set; }
}
