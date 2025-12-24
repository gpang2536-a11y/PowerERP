using System;
using System.Collections.Generic;

namespace powererp.Models;

public partial class InvMasters
{
    public int Id { get; set; }

    public string? BaseNo { get; set; }

    public string? ProductNo { get; set; }

    public int Qty { get; set; }

    public string? Remark { get; set; }
}
