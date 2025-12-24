using System;
using System.Collections.Generic;

namespace powererp.Models;

public partial class Roles
{
    public int Id { get; set; }

    public bool IsEnabled { get; set; }

    public string? RoleNo { get; set; }

    public string? RoleName { get; set; }

    public string? Remark { get; set; }
}
