using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaFormLocks))]
    public partial class FormLocks
    {
        [NotMapped]
        [Display(Name = "鎖定名稱")]
        public string? LockName { get; set; }
    }
}

public class z_metaFormLocks
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "鎖定代號")]
    public string? LockNo { get; set; }
    [Display(Name = "鎖定日期")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    public DateTime? LockDate { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}
