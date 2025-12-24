/// <summary>
/// 類別屬性列舉
/// </summary>
public enum enDataModelProperty
{
    [Display(Name = "類別名稱")]
    ClassName,
    [Display(Name = "欄位名稱")]
    PropertyName,
    [Display(Name = "欄位型別")]
    PropertyType,
    [Display(Name = "完整型別")]
    FullType,
    [Display(Name = "允許Null")]
    AllowNull,
    [Display(Name = "顯示文字")]
    DisplayName,
    [Display(Name = "欄位格式")]
    DataFormat,
    [Display(Name = "主索引鍵")]
    IsKeyColumn,
    [Display(Name = "欄位隱藏")]
    IsHidden,
    [Display(Name = "CheckBox")]
    IsCheckBox,
    [Display(Name = "必輸欄位")]
    IsRequired,
    [Display(Name = "預設值")]
    DefaultValue,
    [Display(Name = "下拉選單來源類別")]
    DropdownClass
}