using System;
using System.Collections.Generic;

namespace powererp.Models;

public partial class EmployeeAgents
{
    public int Id { get; set; }

    public bool IsEnabled { get; set; }

    public string? EmpNo { get; set; }

    public string? AgentNo { get; set; }

    public string? Remark { get; set; }
}
