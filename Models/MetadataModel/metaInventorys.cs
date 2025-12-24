using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaInventorys))]
    public partial class Inventorys
    {
        [NotMapped]
        [Display(Name = "列號")]
        public int RowNo { get; set; }
        [NotMapped]
        [Display(Name = "出入庫別")]
        public string? CodeName { get; set; }
        [NotMapped]
        [Display(Name = "類別名稱")]
        public string? TypeName { get; set; }
        [NotMapped]
        [Display(Name = "倉庫編號")]
        public string? WarehouseName { get; set; }
        [NotMapped]
        [Display(Name = "承辦姓名")]
        public string? HandleName { get; set; }
        [NotMapped]
        [Display(Name = "表單狀態")]
        public string StatusName
        {
            get
            {
                if (IsCancel) return "已作廢";
                else if (IsConfirm) return "已確認";
                else return "待確認";
            }
        }
        [NotMapped]
        [Display(Name = "狀態圖示")]
        public string StatusIcon
        {
            get
            {
                if (IsCancel) return "fa-solid fa-circle-xmark text-danger";
                else if (IsConfirm) return "fa-solid fa-circle-check text-success";
                else return "fa-solid fa-clock text-secondary";
            }
        }
    }
}

public partial class z_metaInventorys
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "唯一鍵值")]
    public string? BaseNo { get; set; }
    [Display(Name = "確認")]
    public bool IsConfirm { get; set; }
    [Display(Name = "作廢")]
    public bool IsCancel { get; set; }
    [Display(Name = "庫存類別")]
    public string? TypeNo { get; set; }
    [Display(Name = "單據類別")]
    public string? SheetCode { get; set; }
    [Display(Name = "單據編號")]
    public string? SheetNo { get; set; }
    [Display(Name = "單據日期")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    public DateTime? SheetDate { get; set; }
    [Display(Name = "倉庫編號")]
    public string? WarehouseNo { get; set; }
    [Display(Name = "對象代號")]
    public string? TargetNo { get; set; }
    [Display(Name = "對象名稱")]
    public string? TargetName { get; set; }
    [Display(Name = "承辦代號")]
    public string? HandleNo { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}
