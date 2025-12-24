using System;
using System.Collections.Generic;

namespace powererp.Models;

public partial class Questions
{
    public int Id { get; set; }

    public string? SortNo { get; set; }

    public string? QuestionText { get; set; }

    public string? AnswerText { get; set; }

    public string? Remark { get; set; }
}
