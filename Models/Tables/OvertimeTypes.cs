using System;
using System.Collections.Generic;

namespace powererp.Models;

public partial class OvertimeTypes
{
    public int Id { get; set; }

    public string? BaseNo { get; set; }

    public string? TypeNo { get; set; }

    public string? TypeName { get; set; }

    public int StartHour { get; set; }

    public int EndHour { get; set; }

    public decimal Rates { get; set; }

    public string? Remark { get; set; }
}
