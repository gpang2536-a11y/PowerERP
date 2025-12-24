using System;
using System.Collections.Generic;

namespace powererp.Models;

public partial class TableRelations
{
    public int Id { get; set; }

    public string? RelationName { get; set; }

    public string? LeftTableName { get; set; }

    public string? RightTableName { get; set; }

    public string? LeftColumnName { get; set; }

    public string? LeftDataType { get; set; }

    public string? RightColumnName { get; set; }

    public string? RightDataType { get; set; }

    public string? RelationType { get; set; }

    public string? Remark { get; set; }
}
