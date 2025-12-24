using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaInventorysType))]
    public partial class InventorysType
    {

    }
}


public class z_metaInventorysType
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "類別編號")]
    public string? TypeNo { get; set; }
    [Display(Name = "類別名稱")]
    public string? TypeName { get; set; }
    [Display(Name = "數量值")]
    public int QtyCode { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}
