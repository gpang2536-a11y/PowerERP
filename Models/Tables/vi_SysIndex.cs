using System;
using System.Collections.Generic;

namespace powererp.Models;

public partial class vi_SysIndex
{
    public int TableId { get; set; }

    public string TableName { get; set; } = null!;

    public string? IndexName { get; set; }

    public string? IndexColumn { get; set; }

    public string IsClustered { get; set; } = null!;

    public string IsPrimaryKey { get; set; } = null!;

    public string IsUnique { get; set; } = null!;
}
