using System;
using System.Collections.Generic;

namespace powererp.Models;

public partial class vi_SysTableColumn
{
    public string? SchemaName { get; set; }

    public int TableId { get; set; }

    public string TableType { get; set; } = null!;

    public string TableName { get; set; } = null!;

    public short? ColumnId { get; set; }

    public string? ColumnName { get; set; }

    public string? DataTypeName { get; set; }

    public string DataType { get; set; } = null!;

    public short? DataTypePrec { get; set; }

    public int? DataTypeScale { get; set; }

    public short DataTypeLength { get; set; }

    public string? DefaultValue { get; set; }

    public string IsNullable { get; set; } = null!;

    public string IsIdentity { get; set; } = null!;

    public object? Comments { get; set; }
}
