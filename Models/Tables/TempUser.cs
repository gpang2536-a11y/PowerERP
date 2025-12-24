using System;
using System.Collections.Generic;

namespace powererp.Models;

public partial class TempUser
{
    public int Id { get; set; }

    public string? UserNo { get; set; }

    public string? UserName { get; set; }

    public string? Remark { get; set; }
}
