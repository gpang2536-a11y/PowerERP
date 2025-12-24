using System;
using System.Collections.Generic;

namespace powererp.Models;

public partial class PropertyNames
{
    public int Id { get; set; }

    public string? PropName { get; set; }

    public string? DisplayName { get; set; }

    public string? Remark { get; set; }
}
