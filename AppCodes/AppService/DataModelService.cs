using System.Reflection;

/// <summary>
/// 資料模型服務
/// </summary>
public class DataModelService : BaseClass
{
    /// <summary>
    /// 取得指定類別的指定類別屬性及指定類型值
    /// </summary>
    /// <param name="className">類別名稱</param>
    /// <param name="propertyName">屬性名稱</param>
    /// <param name="propertyType">屬性類型</param>
    /// <param name="nameSpaceName">類別命名空間名稱</param>
    /// <param name="metaClassName">MetaData 名稱</param>
    /// <returns></returns>
    public string GetPropertyTypeValue(string className, string propertyName, enDataModelProperty propertyType, string nameSpaceName = "", string metaClassName = "")
    {
        string str_value = "";
        var model = GetPropertyModelList(className, nameSpaceName, metaClassName);
        if (model != null)
        {
            var prop = model.Where(m => m.PropertyName == propertyName).FirstOrDefault();
            if (prop != null)
            {
                if (propertyType == enDataModelProperty.DisplayName) str_value = prop.DisplayName;
                if (propertyType == enDataModelProperty.PropertyType) str_value = prop.PropertyType;
                if (propertyType == enDataModelProperty.FullType) str_value = prop.FullType;
                if (propertyType == enDataModelProperty.AllowNull) str_value = prop.AllowNull;
                if (propertyType == enDataModelProperty.DataFormat) str_value = prop.DataFormat;
                if (propertyType == enDataModelProperty.IsKeyColumn) str_value = prop.IsKeyColumn.ToString();
                if (propertyType == enDataModelProperty.IsHidden) str_value = prop.IsHidden.ToString();
                if (propertyType == enDataModelProperty.IsCheckBox) str_value = prop.IsCheckBox.ToString();
                if (propertyType == enDataModelProperty.IsRequired) str_value = prop.IsRequired.ToString();
                if (propertyType == enDataModelProperty.DefaultValue) str_value = prop.DefaultValue;
                if (propertyType == enDataModelProperty.DropdownClass) str_value = prop.DropdownClass;
                if (propertyType == enDataModelProperty.ClassName) str_value = prop.ClassName;
                if (propertyType == enDataModelProperty.PropertyName) str_value = prop.PropertyName;
            }
        }
        return str_value;
    }

    /// <summary>
    /// 取得 DataModel 屬性列表
    /// </summary>
    /// <param name="className">類別名稱</param>
    /// <param name="nameSpaceName">類別命名空間名稱</param>
    /// <param name="metaClassName">MetaData 名稱</param>
    /// <returns></returns>
    public List<DataModelPropertyModel> GetPropertyModelList(string className, string nameSpaceName = "", string metaClassName = "")
    {
        List<DataModelPropertyModel> values = new List<DataModelPropertyModel>();
        bool bln_hidden = false;
        bool bln_key = false;
        bool bln_required = false;
        bool bln_checkbox = false;
        string str_display = "";
        string str_dropdown = "";
        string str_format = "";
        string str_default = "";
        string str_namespace = (string.IsNullOrEmpty(nameSpaceName)) ? "" : $"{nameSpaceName}.";
        string str_class_name = $"{str_namespace}{className}";
        string str_metadata_name = (string.IsNullOrEmpty(metaClassName)) ? str_class_name : metaClassName;
        PropertyInfo[] myPropertyInfo = null;
        PropertyInfo[] myMetadataInfo = null;
        if (Type.GetType(str_class_name) != null) myPropertyInfo = Type.GetType(str_class_name).GetProperties();
        if (Type.GetType(str_metadata_name) != null) myMetadataInfo = Type.GetType(str_metadata_name).GetProperties();
        if (myPropertyInfo != null)
        {
            foreach (var item in myPropertyInfo)
            {
                bln_hidden = false;
                bln_key = false;
                bln_required = false;
                bln_checkbox = false;
                str_display = "";
                str_format = "";
                str_dropdown = "";
                str_default = "";
                var displays = (DisplayAttribute)Attribute.GetCustomAttribute(item, typeof(DisplayAttribute));
                if (displays != null)
                {
                    str_display = displays.Name;
                }
                else
                {
                    if (myMetadataInfo != null)
                    {
                        PropertyInfo metaName = myMetadataInfo.Where(m => m.Name == item.Name).FirstOrDefault();
                        if (metaName != null)
                        {
                            DisplayAttribute display = (DisplayAttribute)Attribute.GetCustomAttribute(metaName, typeof(DisplayAttribute));
                            KeyAttribute key = (KeyAttribute)Attribute.GetCustomAttribute(metaName, typeof(KeyAttribute));
                            RequiredAttribute required = (RequiredAttribute)Attribute.GetCustomAttribute(metaName, typeof(RequiredAttribute));
                            DisplayFormatAttribute format = (DisplayFormatAttribute)Attribute.GetCustomAttribute(metaName, typeof(DisplayFormatAttribute));
                            ColumnAttribute column = (ColumnAttribute)Attribute.GetCustomAttribute(metaName, typeof(ColumnAttribute));

                            bln_key = (key == null) ? false : true;
                            bln_required = (required == null) ? false : true;
                            str_display = (display == null) ? item.Name : display.Name;
                            str_format = (format == null) ? "" : format.DataFormatString;
                        }
                    }
                }
                if (item.Name == "Id" && bln_key == false) bln_key = true;
                var prop = GetDataModelProperty(item.Name, item.PropertyType.Name, item.PropertyType.FullName);
                prop.IsHidden = bln_hidden;
                prop.IsKeyColumn = bln_key;
                prop.IsRequired = bln_required;
                prop.DisplayName = str_display;
                prop.DataFormat = str_format;
                prop.IsCheckBox = bln_checkbox;
                prop.DropdownClass = str_dropdown;
                prop.DefaultValue = str_default;
                if (prop.PropertyType == "DateOnly") prop.PropertyType = "DateTime";
                values.Add(prop);
            }
        }
        return values;
    }

    /// <summary>
    /// 取得指定類別的屬性列表
    /// </summary>
    /// <param name="propertyName">屬性名稱</param>
    /// <param name="typeName">屬性類型名稱</param>
    /// <param name="typeClassName">屬性類型類別完整名稱</param>
    /// <returns></returns>
    private DataModelPropertyModel GetDataModelProperty(string propertyName, string typeName, string typeClassName)
    {
        DataModelPropertyModel values = new DataModelPropertyModel();
        values.PropertyName = propertyName;
        if (typeName == "Nullable`1")
        {
            //System.Nullable`1[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
            values.AllowNull = "是";
            int int_start = typeClassName.IndexOf("[[System.") + 9;
            int int_end = typeClassName.IndexOf(",");
            int int_leng = int_end - int_start;
            values.PropertyType = typeClassName.Substring(int_start, int_leng);
            values.FullType = "";
            string str_typeName = typeClassName.Substring(int_start, int_leng);
            if (str_typeName == "Int32") values.FullType = "Nullable<int>";
            if (str_typeName == "Boolean") values.FullType = "Nullable<bool>";
            if (str_typeName == "DateTime") values.FullType = "Nullable<System.DateTime>";
            if (str_typeName == "Decimal") values.FullType = "Nullable<decimal>";
            if (str_typeName == "DateOnly") values.FullType = "Nullable<System.DateTime>";
            if (string.IsNullOrEmpty(values.FullType)) values.FullType = $"Nullable<{str_typeName.ToLower()}>";
        }
        else
        {
            values.AllowNull = "否";
            string str_column_type = "";
            if (typeName == "Int32") str_column_type = "int";
            if (typeName == "Boolean") str_column_type = "bool";
            if (typeName == "DateTime") str_column_type = "DateTime";
            if (typeName == "DateOnly") str_column_type = "DateTime";
            if (string.IsNullOrEmpty(str_column_type)) str_column_type = typeName.ToLower();
            values.PropertyType = str_column_type;
            values.FullType = str_column_type;
        }
        return values;
    }
}

/// <summary>
/// 類別屬性模型
/// </summary>
public class DataModelPropertyModel
{
    [Display(Name = "類別名稱")]
    [StringLength(50)]
    public string ClassName { get; set; }
    [Display(Name = "欄位名稱")]
    [StringLength(50)]
    public string PropertyName { get; set; }
    [Display(Name = "欄位型別")]
    public string PropertyType { get; set; }
    [Display(Name = "完整型別")]
    public string FullType { get; set; }
    [Display(Name = "允許Null")]
    public string AllowNull { get; set; }
    [Display(Name = "顯示文字")]
    public string DisplayName { get; set; }
    [Display(Name = "欄位格式")]
    public string DataFormat { get; set; }
    [Display(Name = "主索引鍵")]
    public bool IsKeyColumn { get; set; }
    [Display(Name = "欄位隱藏")]
    public bool IsHidden { get; set; }
    [Display(Name = "CheckBox")]
    public bool IsCheckBox { get; set; }
    [Display(Name = "必輸欄位")]
    public bool IsRequired { get; set; }
    [Display(Name = "預設值")]
    public string DefaultValue { get; set; }
    [Display(Name = "下拉選單來源類別")]
    public string DropdownClass { get; set; }
}