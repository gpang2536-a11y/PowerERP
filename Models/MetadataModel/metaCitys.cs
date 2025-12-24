using System;
using System.Collections.Generic;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaCitys))]
    public partial class Citys
    {

    }
}

public class z_metaCitys
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "排序")]
    public string? SortNo { get; set; }
    [Display(Name = "縣市名稱")]
    public string? CityName { get; set; }
    [Display(Name = "緯度")]
    [DisplayFormat(DataFormatString = "{0:N15}", ApplyFormatInEditMode = true)]
    public string? Latitude { get; set; }
    [Display(Name = "經度")]
    [DisplayFormat(DataFormatString = "{0:N15}", ApplyFormatInEditMode = true)]
    public string? Longitude { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}