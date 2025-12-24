using System.Linq.Expressions;
using System.Reflection;
using System.Text.Encodings.Web;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Html;

public static class HtmlHelperExtensions
{
    /// <summary>
    /// HttpContextAccessor 物件
    /// </summary>
    /// <returns></returns>
    public static IHttpContextAccessor _contextAccessor { get; set; } = new HttpContextAccessor();
    /// <summary>
    /// HttpContext 物件
    /// </summary>
    public static HttpContext? _context { get { return _contextAccessor.HttpContext; } }
    /// <summary>
    /// 圖片 Html Helper
    /// </summary>
    /// <param name="htmlHelper">htmlHelper 物件</param>
    /// <param name="src">圖片網址</param>
    /// <param name="alt">替代文字</param>
    /// <param name="klass">Class 樣式表名稱</param>
    /// <returns></returns>
    public static TagBuilder Image(this IHtmlHelper htmlHelper, string src, string alt, string klass)
    {
        TagBuilder tb = new("img");
        tb.Attributes.Add("src", src);
        if (!string.IsNullOrEmpty(alt)) tb.Attributes.Add("alt", alt);
        if (!string.IsNullOrEmpty(klass)) tb.Attributes.Add("class", klass);
        return tb;
    }

    public static TagBuilder DisplayNameSortFor<TModelItem, TResult>(this IHtmlHelper<IEnumerable<TModelItem>> htmlHelper, Expression<Func<TModelItem, TResult>> expression, string columnName = "")
    {
        //呼叫原本的EditorFor
        string fullName = typeof(TModelItem).FullName;
        var colName = string.IsNullOrEmpty(columnName) ? expression.Body.GetMemberExpressions().FirstOrDefault().Member.Name : columnName;
        var colCaption = htmlHelper.DisplayNameFor(expression);
        string str_area = string.IsNullOrEmpty(ActionService.Area) ? "" : $"/{ActionService.Area}";
        string str_controller = string.IsNullOrEmpty(ActionService.Controller) ? "" : $"/{ActionService.Controller}";
        string str_action = $"/Sort/{colName}";
        string str_url = $"{ActionService.HttpHost}{str_area}{str_controller}{str_action}";
        string str_text = colCaption;
        string str_direction = (SessionService.SortDirection.ToLower() == "asc") ? " ▲" : " ▼";
        if (SessionService.SortColumn == colName)
        {
            str_text += str_direction;
        }
        var location = new Uri(str_url);
        TagBuilder tb = new("a");
        tb.Attributes.Add("href", location.ToString());
        tb.InnerHtml.AppendHtml(str_text);
        return tb;
    }
    public static TagBuilder DisplayNameSortFor<TModelItem, TResult>(this IHtmlHelper<TModelItem> htmlHelper, Expression<Func<TModelItem, TResult>> expression, string columnName = "")
    {
        //呼叫原本的EditorFor
        string fullName = typeof(TModelItem).FullName;
        string colName = string.IsNullOrEmpty(columnName) ? expression.Body.GetMemberExpressions().FirstOrDefault().Member.Name : columnName;
        string colCaption = htmlHelper.DisplayNameFor(expression);
        string str_area = string.IsNullOrEmpty(ActionService.Area) ? "" : $"/{ActionService.Area}";
        string str_controller = string.IsNullOrEmpty(ActionService.Controller) ? "" : $"/{ActionService.Controller}";
        string str_action = $"/Sort/{colName}";
        string str_url = $"{ActionService.HttpHost}{str_area}{str_controller}{str_action}";
        string str_text = colCaption;
        string str_direction = (SessionService.SortDirection.ToLower() == "asc") ? " ▲" : " ▼";
        if (SessionService.SortColumn == colName)
        {
            str_text += str_direction;
        }
        var location = new Uri(str_url);
        TagBuilder tb = new("a");
        tb.Attributes.Add("href", location.ToString());
        tb.InnerHtml.AppendHtml(str_text);
        return tb;
    }
    public static TagBuilder DisplayNameSortFor(this IHtmlHelper htmlHelper, string columnName, string captionName)
    {
        //呼叫原本的EditorFor
        string str_area = string.IsNullOrEmpty(ActionService.Area) ? "" : $"/{ActionService.Area}";
        string str_controller = string.IsNullOrEmpty(ActionService.Controller) ? "" : $"/{ActionService.Controller}";
        string str_action = $"/Sort/{columnName}";
        string str_url = $"{ActionService.HttpHost}{str_area}{str_controller}{str_action}";
        string str_text = captionName;
        string str_direction = (SessionService.SortDirection.ToLower() == "asc") ? " ▲" : " ▼";
        if (SessionService.SortColumn == columnName)
        {
            str_text += str_direction;
        }
        var location = new Uri(str_url);
        TagBuilder tb = new("a");
        tb.Attributes.Add("href", location.ToString());
        tb.InnerHtml.AppendHtml(str_text);
        return tb;
    }

    public static IHtmlContent DetailDataFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>>? expression1, bool firstRow = false, bool lastRow = false)
    {
        string str_html = "<div class=\"";
        str_html += "col-md-12 ";
        if (!firstRow && !lastRow) str_html += "border-bottom ";
        if (expression1 != null)
        {
            str_html += "<label class=\"control-label\">";
            str_html += htmlHelper.DisplayNameFor(expression1);
            str_html += "：";
            str_html += "</label>";
            str_html += "<label class=\"control-data\">";
            str_html += GetDisplayForString(htmlHelper.DisplayFor(expression1));
            str_html += "</label>";
        }
        str_html += "</div>";
        return new HtmlString(str_html);
    }

    public static IHtmlContent DetailDataFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>>? expression1, Expression<Func<TModel, TResult>>? expression2, bool firstRow = false, bool lastRow = false)
    {
        string str_html = "<div class=\"";
        if (expression2 != null)
            str_html += "col-md-6 ";
        else
            str_html += "col-md-12 ";
        if (!lastRow) str_html += "border-bottom ";
        str_html += "border-end\">";
        if (expression1 != null)
        {
            str_html += "<label class=\"control-label\">";
            str_html += htmlHelper.DisplayNameFor(expression1);
            str_html += "：";
            str_html += "</label>";
            str_html += "<label class=\"control-data\">";
            str_html += GetDisplayForString(htmlHelper.DisplayFor(expression1));
            str_html += "</label>";
        }
        str_html += "</div>";
        if (expression2 != null)
        {
            str_html += "<div class=\"col-md-6 ";
            if (!lastRow) str_html += "border-bottom ";
            str_html += "border-start\">";

            str_html += "<label class=\"control-label\">";
            str_html += htmlHelper.DisplayNameFor(expression2);
            str_html += "：";
            str_html += "</label>";
            str_html += "<label class=\"control-data\">";
            str_html += GetDisplayForString(htmlHelper.DisplayFor(expression2));
            str_html += "</label>";
            str_html += "</div>";
        }
        return new HtmlString(str_html);
    }

    public static IHtmlContent DetailDataFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>>? expression1, Expression<Func<TModel, TResult>>? expression2, Expression<Func<TModel, TResult>>? expression3, bool firstRow = false)
    {
        string str_html = "<div class=\"";
        if (expression2 != null && expression3 != null)
            str_html += "col-md-4 ";
        else if (expression2 == null && expression3 == null)
            str_html += "col-md-12 ";
        else
            str_html += "col-md-6 ";
        if (firstRow) str_html += "border-top ";
        str_html += "border-bottom border-start border-end\">";
        if (expression1 != null)
        {
            str_html += "<label class=\"control-label\">";
            str_html += htmlHelper.DisplayNameFor(expression1);
            str_html += "：";
            str_html += "</label>";
            str_html += "<label class=\"control-data\">";
            str_html += GetDisplayForString(htmlHelper.DisplayFor(expression1));
            str_html += "</label>";
        }
        str_html += "</div>";
        if (expression2 != null)
        {
            str_html += "<div class=\"";
            if (expression3 != null)
                str_html += "col-md-4 ";
            else
                str_html += "col-md-6 ";
            if (firstRow) str_html += "border-top ";
            str_html += "border-bottom border-start border-end\">";

            str_html += "<label class=\"control-label\">";
            str_html += htmlHelper.DisplayNameFor(expression2);
            str_html += "：";
            str_html += "</label>";
            str_html += "<label class=\"control-data\">";
            str_html += GetDisplayForString(htmlHelper.DisplayFor(expression2));
            str_html += "</label>";
            str_html += "</div>";
        }
        if (expression3 != null)
        {
            str_html += "<div class=\"";
            if (expression2 != null)
                str_html += "col-md-4 ";
            else
                str_html += "col-md-6 ";
            if (firstRow) str_html += "border-top ";
            str_html += "border-bottom border-start border-end\">";

            str_html += "<label class=\"control-label\">";
            str_html += htmlHelper.DisplayNameFor(expression3);
            str_html += "：";
            str_html += "</label>";
            str_html += "<label class=\"control-data\">";
            str_html += GetDisplayForString(htmlHelper.DisplayFor(expression3));
            str_html += "</label>";
            str_html += "</div>";
        }
        return new HtmlString(str_html);
    }

    public static string GetDisplayForString(this IHtmlContent content)
    {
        if (content == null)
            return null;

        using var writer = new StringWriter();
        content.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
    }

    public static IHtmlContent LabelRequiredFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression, string className = "control-label")
    {
        var colCaption = htmlHelper.DisplayNameFor(expression);
        var member = expression.Body.GetMemberExpressions().FirstOrDefault().Member;
        var colName = member.Name;
        string? nameSpace = member.DeclaringType?.Namespace;
        string? tableName = member.DeclaringType?.Name;
        string? fullName = member.DeclaringType?.FullName;
        string meteName = $"z_meta{tableName}";
        string str_caption = colCaption;
        if (IsRequired(colName, fullName, meteName)) str_caption += " <span class=\"text-danger\">*</span>";
        TagBuilder tb = new("label");
        tb.Attributes.Add("class", className);
        tb.Attributes.Add("title", "必填欄位");
        tb.InnerHtml.AppendHtml(str_caption);
        return tb;
    }

    public static IHtmlContent DisplayLabelFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression, bool showColon = true, string className = "control-label")
    {
        var colText = htmlHelper.DisplayNameFor(expression);
        if (showColon) colText += "：";
        TagBuilder tb = new("label");
        tb.Attributes.Add("class", className);
        tb.InnerHtml.AppendHtml(colText);
        return tb;
    }

    public static IHtmlContent DisplayDataFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression, string className = "control-data")
    {
        var colText = htmlHelper.DisplayFor(expression);
        TagBuilder tb = new("label");
        tb.Attributes.Add("class", className);
        tb.InnerHtml.AppendHtml(colText);
        return tb;
    }

    private static bool IsRequired(string colName, string fullName, string meteName)
    {
        bool bln_required = false;
        PropertyInfo[] myPropertyInfo = null;
        PropertyInfo[] myMetadataInfo = null;
        if (Type.GetType(fullName) != null) myPropertyInfo = Type.GetType(fullName).GetProperties();
        if (Type.GetType(meteName) != null) myMetadataInfo = Type.GetType(meteName).GetProperties();
        if (myMetadataInfo != null)
        {
            PropertyInfo metaName = myMetadataInfo.Where(m => m.Name == colName).FirstOrDefault();
            if (IsRequiredAttribute(metaName)) bln_required = true;
        }
        else
        {
            PropertyInfo propName = myPropertyInfo.Where(m => m.Name == colName).FirstOrDefault();
            if (IsRequiredAttribute(propName)) bln_required = true;
        }
        return bln_required;
    }

    private static bool IsRequiredAttribute(PropertyInfo info)
    {
        bool bln_value = false;
        if (info != null)
        {
            RequiredAttribute required = (RequiredAttribute)Attribute.GetCustomAttribute(info, typeof(RequiredAttribute));
            if (required != null) bln_value = true;
        }
        return bln_value;
    }

    public static IHtmlContent NumbericFor<TModel, TResult>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TResult>> expression, object additionalViewData)
    {
        //呼叫原本的EditorFor
        IHtmlContent editorString = helper.EditorFor(expression);

        int int_index = 0;
        int int_id = 0;
        int int_name = 0;
        string str_id = "";
        string str_name = "";
        string str_addition = "";
        var keys = ((TagBuilder)editorString).Attributes.Keys;
        var values = ((TagBuilder)editorString).Attributes.Values;
        foreach (var key in keys)
        {
            int_index++;
            if (key == "id") int_id = int_index;
            if (key == "name") int_name = int_index;
        }
        int_index = 0;
        foreach (var value in values)
        {
            int_index++;
            if (int_index == int_id) str_id = value.ToString();
            if (int_index == int_name) str_name = value.ToString();
        }
        str_addition = additionalViewData.ToString();
        str_addition = str_addition.Replace("'", "\"").Trim();

        string str_command = $"<input type=\"number\" id=\"{str_id}\" name=\"{str_name}\" {str_addition} />";
        var valueString = new HtmlString(str_command);

        //TagBuilder div = new TagBuilder("div");
        //div.AddCssClass("col-md-3");
        //將mvc render出來的資料塞入div節點中
        //div.Attributes.Add("InnerHtml", editorString.ToString());

        //將div字串附值給 result
        //HtmlString result = new HtmlString(div.ToString());
        return valueString;
    }

    public static TagBuilder ActionLinkButton(this IHtmlHelper helper, enFormLinkButton buttonType)
    {
        return ActionLinkButton(helper, buttonType, 0, "");
    }
    public static TagBuilder ActionLinkButton(this IHtmlHelper helper, enFormLinkButton buttonType, int id)
    {
        return ActionLinkButton(helper, buttonType, id, "");
    }
    public static TagBuilder ActionLinkButton(this IHtmlHelper helper, enFormLinkButton buttonType, int id, object htmlAttributes)
    {
        string AreaName = string.IsNullOrEmpty(ActionService.Area) ? "" : $"/{ActionService.Area}";
        string ControllerName = string.IsNullOrEmpty(ActionService.Controller) ? "" : $"{ActionService.Controller}";
        string ActionUrl = "";
        string ActionText = "";
        string ActionName = "";
        string ClassName = "";
        string HtmlAttribute = "";
        int enValue = (int)buttonType;
        string enName = buttonType.ToString();

        var formButton = new EnumService<enFormLinkButton>();
        ActionName = formButton.GetText(enValue);
        if (buttonType == enFormLinkButton.Create) { id = 0; ActionText = "新增"; ClassName = "btn btn-primary"; }
        if (buttonType == enFormLinkButton.CreateEdit && id == 0) { ActionText = "新增"; ClassName = "btn btn-primary"; }
        if (buttonType == enFormLinkButton.CreateEdit && id > 0) { ActionText = "修改"; ClassName = "btn btn-success"; }
        if (buttonType == enFormLinkButton.Edit) { ActionText = "修改"; ClassName = "btn btn-success"; }

        ActionUrl = $"{ActionService.HttpHost}{AreaName}/{ControllerName}/{ActionName}/{id}";

        if (htmlAttributes != null)
        {
            HtmlAttribute = htmlAttributes.ToString();
            if (!string.IsNullOrEmpty(HtmlAttribute)) ClassName += " " + HtmlAttribute.Replace("'", "\"").Trim();
        }

        var location = new Uri(ActionUrl);
        TagBuilder tb = new("a");
        tb.Attributes.Add("href", location.ToString());
        tb.Attributes.Add("class", ClassName);
        tb.InnerHtml.AppendHtml(ActionText);
        return tb;
    }
    public static TagBuilder ActionReturnButton(this IHtmlHelper helper)
    {
        return ActionReturnButton(helper, "", "", "");
    }
    public static TagBuilder ActionReturnButton(this IHtmlHelper helper, string actionName, string actionText)
    {
        return ActionReturnButton(helper, actionName, actionText, "");
    }
    public static TagBuilder ActionReturnButton(this IHtmlHelper helper, string actionName, string actionText, object htmlAttributes)
    {
        string AreaName = string.IsNullOrEmpty(ActionService.Area) ? "" : $"/{ActionService.Area}";
        string ControllerName = string.IsNullOrEmpty(ActionService.Controller) ? "" : $"{ActionService.Controller}";
        string ActionUrl = "";
        string ActionText = "";
        string ActionName = "";
        string ClassName = "btn btn-success";
        string HtmlAttribute = "";

        ActionName = (string.IsNullOrEmpty(actionName)) ? "Index" : actionName;
        ActionText = (string.IsNullOrEmpty(actionText)) ? "返回列表" : actionText;

        ActionUrl = $"{ActionService.HttpHost}{AreaName}/{ControllerName}/{ActionName}";

        if (htmlAttributes != null)
        {
            HtmlAttribute = htmlAttributes.ToString();
            if (!string.IsNullOrEmpty(HtmlAttribute)) ClassName += " " + HtmlAttribute.Replace("'", "\"").Trim();
        }

        var location = new Uri(ActionUrl);
        TagBuilder tb = new("a");
        tb.Attributes.Add("href", location.ToString());
        tb.Attributes.Add("class", ClassName);
        tb.InnerHtml.AppendHtml(ActionText);
        return tb;
    }
    public static IHtmlContent InputButton(this IHtmlHelper helper, enFormInputButton buttonType)
    {
        return InputButton(helper, buttonType, 0, "", "");
    }
    public static IHtmlContent InputButton(this IHtmlHelper helper, enFormInputButton buttonType, int id)
    {
        return InputButton(helper, buttonType, id, "", "");
    }
    public static IHtmlContent InputButton(this IHtmlHelper helper, enFormInputButton buttonType, int id, object routeValues, object htmlAttributes)
    {
        string ActionText = "";
        string ActionName = "";
        string ClassName = "";
        string ButtonCommand = "";
        string RouteValue = "";
        string HtmlAttribute = "";
        int enValue = (int)buttonType;
        string enName = buttonType.ToString();
        if (routeValues != null)
        {
            RouteValue = routeValues.ToString();
            if (!string.IsNullOrEmpty(RouteValue)) RouteValue = RouteValue.Replace("'", "\"").Trim();
        }
        if (htmlAttributes != null)
        {
            HtmlAttribute = htmlAttributes.ToString();
            if (!string.IsNullOrEmpty(HtmlAttribute)) HtmlAttribute = HtmlAttribute.Replace("'", "\"").Trim();
        }
        var formButton = new EnumService<enFormInputButton>();
        ActionName = formButton.GetText(enValue);
        if (buttonType == enFormInputButton.DeleteAlert) { ActionText = "刪除"; ClassName = "btn btn-danger"; }
        if (buttonType == enFormInputButton.DeleteConfirm) { ActionText = "刪除"; ClassName = "btn btn-danger"; }
        if (buttonType == enFormInputButton.Submit) { id = -1; ActionText = "異動存檔"; ClassName = "btn btn-primary"; }


        if (buttonType == enFormInputButton.Submit)
        {
            //<input type="submit" value="異動存檔" class="btn btn-primary" />
            ButtonCommand = "<input type=\"submit\" ";
            ButtonCommand += "value=\"" + ActionText + "\" ";
            ButtonCommand += "class=\"" + ClassName + "\" ";
            if (!string.IsNullOrEmpty(HtmlAttribute))
            {
                ButtonCommand += " " + HtmlAttribute + " ";
            }
            ButtonCommand += "/>";
        }
        else if (buttonType == enFormInputButton.DeleteAlert)
        {
            //<input type="button" id="Delete" value="刪除" class="btn btn-danger" onclick = "DeleteData(@ActionService.RowId , '@ActionService.RowData')" />
            ButtonCommand = "<input type=\"button\" ";
            ButtonCommand += "id=\"Delete\" ";
            ButtonCommand += "value=\"刪除\" ";
            ButtonCommand += "class=\"" + ClassName + "\" ";
            ButtonCommand += "onclick = \"DeleteData(" + SessionService.RowId + ", '" + SessionService.RowData + "')\" ";
            ButtonCommand += "/>";
        }
        else if (buttonType == enFormInputButton.DeleteConfirm)
        {
            //<input type="button" id="Delete" value="刪除" class="btn btn-danger" onclick = "return confirm('是否確定要刪除?');" />
            ButtonCommand = "<input type=\"button\" ";
            ButtonCommand += "id=\"Delete\" ";
            ButtonCommand += "value=\"刪除\" ";
            ButtonCommand += "class=\"" + ClassName + "\" ";
            ButtonCommand += "onclick = \"return confirm('是否確定要刪除?');\" ";
            ButtonCommand += "/>";
        }

        return new HtmlString(ButtonCommand);
    }

    public static IHtmlContent HelloWorldHTMLString(this IHtmlHelper htmlHelper)
           => new HtmlString("<strong>Hello World</strong>");

    public static String HelloWorldString(this IHtmlHelper htmlHelper)
        => "<strong>Hello World</strong>";
}

public enum enFormInputButton
{
    DeleteAlert,
    DeleteConfirm,
    Submit,
}
public enum enFormLinkButton
{
    Create,
    Edit,
    CreateEdit
}