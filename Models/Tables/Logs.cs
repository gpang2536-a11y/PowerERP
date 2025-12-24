using System;
using System.Collections.Generic;

namespace powererp.Models;

public partial class Logs
{
    public int Id { get; set; }
    public string? KeyNo { get; set; }

    public DateTime? LogDate { get; set; }

    public DateTime? LogTime { get; set; }

    public DateTime? LastDate { get; set; }

    public DateTime? LastTime { get; set; }

    public string? LogCode { get; set; }

    public string? LogLevel { get; set; }

    public string? LogInterval { get; set; }

    public string? LogMessage { get; set; }

    public string? UserNo { get; set; }

    public string? UserName { get; set; }

    public string? TargetNo { get; set; }

    public string? IpAddress { get; set; }

    public string? LogNo { get; set; }

    public int LogQty { get; set; }

    public string? Remark { get; set; }
}
