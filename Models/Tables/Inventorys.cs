using System;
using System.Collections.Generic;

namespace powererp.Models;

public partial class Inventorys
{
    public int Id { get; set; }

    public string? BaseNo { get; set; }

    public bool IsConfirm { get; set; }

    public bool IsCancel { get; set; }

    public string? TypeNo { get; set; }

    public string? SheetCode { get; set; }

    public string? SheetNo { get; set; }

    public DateTime? SheetDate { get; set; }

    public string? WarehouseNo { get; set; }

    public string? TargetNo { get; set; }

    public string? TargetName { get; set; }

    public string? HandleNo { get; set; }

    public string? Remark { get; set; }
}
