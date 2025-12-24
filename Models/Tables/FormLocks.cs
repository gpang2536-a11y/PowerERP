using System;
using System.Collections.Generic;

namespace powererp.Models;

public partial class FormLocks
{
    public int Id { get; set; }

    public string? LockNo { get; set; }

    public DateTime? LockDate { get; set; }

    public string? Remark { get; set; }
}
