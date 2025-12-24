/// <summary>
/// 底層控制器類別
/// </summary>
public class BaseController : Controller
{
    #region 欄位(Field)
    protected IConfiguration Configuration; //環境設定物件
    protected dbEntities db; //EF資料庫管理物件
    protected FormSecurity Security; //表單安全性檢查物件
    #endregion
    #region 共用功能
    /// <summary>
    /// 查詢
    /// </summary>
    /// <returns></returns>
    [Login()]
    [HttpPost]
    public virtual IActionResult Search()
    {
        //設定動作名稱
        ActionService.SetActionName();
        object obj_text = Request.Form[ActionService.SearchText];
        SessionService.SearchText = (obj_text == null) ? string.Empty : obj_text.ToString();
        return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
    }

    /// <summary>
    /// 欄位排序
    /// </summary>
    /// <param name="id">指定排序的欄位</param>
    /// <returns></returns>
    [Login()]
    [HttpGet]
    public virtual IActionResult Sort(string id)
    {
        //設定動作名稱
        ActionService.SetActionName();
        ActionService.SetActionSort(id);
        return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
    }

    /// <summary>
    /// 錯誤訊息處理
    /// </summary>
    /// <param name="id">錯誤訊息處理 Id</param>
    /// <returns></returns>
    public virtual IActionResult ErrorMessage(string id = "")
    {
        SessionService.ErrorMessage = "";
        string str_message = ActionService.GetErrorMessage(id);
        if (!string.IsNullOrEmpty(str_message)) SessionService.ErrorMessage = str_message;
        return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
    }
    #endregion
}