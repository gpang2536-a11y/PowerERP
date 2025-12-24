using System;
using System.Collections.Generic;

namespace powererp.Models;

public partial class Testimonials
{
    public int Id { get; set; }

    public DateTime? SendDate { get; set; }

    public string? UserNo { get; set; }

    public string? UserName { get; set; }

    public string? TitleName { get; set; }

    public int StarCount { get; set; }

    public string? MessageText { get; set; }

    public string? Remark { get; set; }
}
