/// <summary>
/// 底層控制器類別
/// </summary>
public class BaseAdminController : Controller
{
    #region 欄位(Field)
    protected IConfiguration Configuration; //環境設定物件
    protected dbEntities db; //EF資料庫管理物件
    public string MessageText { get; set; } = ""; //訊息
    public string ControllerName { get { return this.GetType().Name.Replace("Controller.", ""); } }
    #endregion

    /// <summary>
    /// 設定 Index 頁面錯誤訊息
    /// </summary>
    public virtual void SetIndexErrorMessage()
    {
        if (!string.IsNullOrEmpty(SessionService.ErrorMessage))
        {
            ViewBag.ErrorMessage = SessionService.ErrorMessage;
            TempData["ErrorMessage"] = SessionService.ErrorMessage;
            SessionService.ErrorMessage = "";
        }
    }
    /// <summary>
    /// 設定 Index ViewBag 及 TempData
    /// </summary>
    public virtual void SetIndexViewBag()
    {
        ViewBag.SearchText = SessionService.SearchText;
        FormSecurity security = SessionService.FormSecurity;
        TempData.Put<FormSecurity>("FormSecurity", security);
    }

    /// <summary>
    /// 資料新增
    /// </summary>
    [HttpGet]
    [Security(Mode = enSecurityMode.Add)]
    public virtual IActionResult Create()
    {
        ActionService.SetActionName(enAction.Create);
        return RedirectToAction(ActionService.CreateEdit, ActionService.Controller, new { area = ActionService.Area, id = 0 });
    }

    /// <summary>
    /// 表頭資料新增
    /// </summary>
    [HttpGet]
    [Security(Mode = enSecurityMode.Add)]
    public virtual IActionResult CreateMaster()
    {
        ActionService.SetActionName(enAction.Create);
        return RedirectToAction(ActionService.CreateEditMaster, ActionService.Controller, new { area = ActionService.Area, id = 0 });
    }

    /// <summary>
    /// 資料修改
    /// </summary>
    /// <param name="id">要修改的Key值</param>
    [HttpGet]
    [Security(Mode = enSecurityMode.Edit)]
    public virtual IActionResult Edit(int id = 0)
    {
        ActionService.SetActionName(enAction.Edit);
        return RedirectToAction(ActionService.CreateEdit, ActionService.Controller, new { area = ActionService.Area, id = id });
    }

    /// <summary>
    /// 表頭資料修改
    /// </summary>
    /// <param name="id">要修改的Key值</param>
    [HttpGet]
    [Security(Mode = enSecurityMode.Edit)]
    public virtual IActionResult EditMaster(int id = 0)
    {
        ActionService.SetActionName(enAction.Edit);
        return RedirectToAction(ActionService.CreateEditMaster, ActionService.Controller, new { area = ActionService.Area, id = id });
    }

    /// <summary>
    /// 資料刪除
    /// </summary>
    /// <param name="id">要刪除的Key值</param>
    /// <returns></returns>
    [HttpPost]
    [Login()]
    [Security(Mode = enSecurityMode.Delete)]
    public virtual IActionResult Delete(int id = 0)
    {
        MessageText = "";
        if (!SecurityService.CheckSecurity(enSecurityMode.Delete))
            MessageText = "刪除權限不足,無法刪除!!";
        else
        {
            MessageText = "查無資料,無法刪除!!";
            if (DeletData(id) > 0) MessageText = "資料已刪除!!";
        }
        dmJsonMessage result = new dmJsonMessage() { Mode = true, Message = MessageText };
        return Json(result);
    }

    /// <summary>
    /// 資料刪除
    /// </summary>
    /// <param name="id">要刪除的Key值</param>
    /// <returns></returns>
    [HttpPost]
    [Login()]
    [Security(Mode = enSecurityMode.Delete)]
    public virtual IActionResult DeleteMaster(int id = 0)
    {
        MessageText = "";
        if (!SecurityService.CheckSecurity(enSecurityMode.Delete))
            MessageText = "刪除權限不足,無法刪除!!";
        else
        {
            MessageText = "查無資料,無法刪除!!";
            if (DeletDataMaster(id) > 0) MessageText = "資料已刪除!!";
        }
        dmJsonMessage result = new dmJsonMessage() { Mode = true, Message = MessageText };
        return Json(result);
    }

    /// <summary>
    /// 功能確認
    /// </summary>
    /// <param name="id">要修改的Key值</param>
    /// <param name="actionNo">動作代號</param>
    [HttpPost]
    [Security(Mode = enSecurityMode.Edit)]
    public virtual IActionResult ConfirmAction(int id = 0, string actionNo = "")
    {
        bool status = false;
        MessageText = "";
        if (actionNo == "Confirm") { status = Confirm(id); MessageText = (status) ? $"表單:{SessionService.MasterNo} 已確認" : MessageText; }
        else if (actionNo == "Undo") { status = Undo(id); MessageText = (status) ? $"表單:{SessionService.MasterNo} 已解除確認" : MessageText; }
        else if (actionNo == "Cancel") { status = Cancel(id); MessageText = (status) ? $"表單:{SessionService.MasterNo} 已作廢" : MessageText; }
        else
        {
            status = false;
            MessageText = "無此功能";
        }
        dmJsonMessage result = new dmJsonMessage() { Mode = status, Message = MessageText };
        return Json(result);
    }

    /// <summary>
    /// 表單資料確認
    /// </summary>
    /// <param name="id">要修改的Key值</param>
    public virtual bool Confirm(int id = 0)
    {
        return true;
    }

    /// <summary>
    /// 表單資料取消確認
    /// </summary>
    /// <param name="id">要修改的Key值</param>
    public virtual bool Undo(int id = 0)
    {
        return true;
    }

    /// <summary>
    /// 表單資料作廢
    /// </summary>
    /// <param name="id">要修改的Key值</param>
    public virtual bool Cancel(int id = 0)
    {
        return true;
    }

    /// <summary>
    /// 資料刪除
    /// </summary>
    /// <param name="id">要刪除的Key值</param>
    /// <returns></returns>
    public virtual int DeletData(int id = 0)
    {
        int result = 0;
        return result;
    }

    /// <summary>
    /// 資料刪除
    /// </summary>
    /// <param name="id">要刪除的Key值</param>
    /// <returns></returns>
    public virtual int DeletDataMaster(int id = 0)
    {
        int result = 0;
        return result;
    }

    /// <summary>
    /// 設定表單狀態
    /// </summary>
    /// <param name="isConfirm">是否確認</param>
    /// <param name="isCancel">是否取消</param>
    /// <param name="formDate">表單日期</param>
    public virtual void SetFormStatus(bool isConfirm, bool isCancel, DateTime? formDate)
    {
        SessionService.IsConfirm = isConfirm;
        SessionService.IsCancel = isCancel;
        if (formDate == null) formDate = DateTime.MaxValue;
        if (SessionService.IsLockMode)
            SessionService.IsFormLocked = (SessionService.LockDate >= formDate);
    }

    /// <summary>
    /// 資料明細
    /// </summary>
    /// <param name="id">要查看的Key值</param>
    [HttpGet]
    [Login()]
    [Security(Mode = enSecurityMode.Display)]
    public virtual IActionResult Detail(int id)
    {
        return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
    }

    /// <summary>
    /// 錯誤訊息
    /// </summary>
    [Login()]
    [HttpPost]
    [Security(Mode = enSecurityMode.Display)]
    public virtual IActionResult ErrorMessage(string id)
    {
        TempData["ErrorMessage"] = ActionService.GetErrorMessage(id);
        return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
    }

    /// <summary>
    /// 表頭資料選取
    /// </summary>
    /// <param name="id">記錄 Id</param>
    [Login()]
    [HttpGet]
    [Security(Mode = enSecurityMode.Display)]
    public virtual IActionResult Select(int id = 0)
    {
        return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
    }

    /// <summary>
    /// 查詢
    /// </summary>
    [Login()]
    [HttpPost]
    [Security(Mode = enSecurityMode.Display)]
    public virtual IActionResult Search()
    {
        if (!SecurityService.CheckSecurity(enSecurityMode.Display))
            return RedirectToAction(ActionService.ErrorMessage, ActionService.Controller, new { area = ActionService.Area, id = "Unauthorized" });
        object obj_text = Request.Form[ActionService.SearchText];
        SessionService.SearchText = (obj_text == null) ? string.Empty : obj_text.ToString();
        if (SessionService.IsMultiMode)
            return RedirectToAction(ActionService.List, ActionService.Controller, new { area = ActionService.Area });
        return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
    }

    /// <summary>
    /// 欄位排序
    /// </summary>
    /// <param name="id">指定排序的欄位</param>
    /// <returns></returns>
    [Login()]
    [HttpGet]
    [Security(Mode = enSecurityMode.Display)]
    public virtual IActionResult Sort(string id)
    {
        if (!SecurityService.CheckSecurity(enSecurityMode.Display))
            return RedirectToAction(ActionService.ErrorMessage, ActionService.Controller, new { area = ActionService.Area, id = "Unauthorized" });
        //設定動作名稱
        ActionService.SetActionName();
        ActionService.SetActionSort(id);
        if (SessionService.IsMultiMode)
            return RedirectToAction(ActionService.List, ActionService.Controller, new { area = ActionService.Area });
        return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
    }

    /// <summary>
    /// 欄位排序
    /// </summary>
    /// <param name="id">指定排序的欄位</param>
    /// <returns></returns>
    [Login()]
    [HttpGet]
    [Security(Mode = enSecurityMode.Display)]
    public virtual IActionResult SelectTab(int id)
    {
        SessionService.DetailPageTabIndex = id;
        return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
    }

    /// <summary>
    /// 變更表頭類別編號
    /// </summary>
    /// <param name="id">表頭類別編號</param>
    /// <returns></returns>
    [Login()]
    [HttpPost]
    [Security(Mode = enSecurityMode.Display)]
    public virtual IActionResult ChangeBaseNo(string id = "")
    {
        object obj_text = Request.Form["BaseNo"];
        id = (obj_text == null) ? string.Empty : obj_text.ToString();
        SessionService.BaseNo = id;
        return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
    }
    /// <summary>
    /// 設定表頭頁數
    /// </summary>
    /// <param name="id">頁數</param>
    /// <returns></returns>
    [Login()]
    [HttpGet]
    [Security(Mode = enSecurityMode.Display)]
    public virtual IActionResult MasterPage(int id = 1)
    {
        SessionService.PageMaster = id;
        return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
    }

    /// <summary>
    /// 設定明細頁數
    /// </summary>
    /// <param name="id">頁數</param>
    /// <returns></returns>
    [Login()]
    [HttpGet]
    [Security(Mode = enSecurityMode.Display)]
    public virtual IActionResult DetailPage(int id = 1)
    {
        SessionService.PageDetail = id;
        return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
    }

    #region Navigation 事件
    /// <summary>
    /// 表頭記錄移到第一筆
    /// </summary>
    /// <param name="id">類別</param>
    /// <returns></returns>
    [Login()]
    [HttpGet]
    [Security(Mode = enSecurityMode.Display)]
    public virtual IActionResult Navigation(string id)
    {
        return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
    }

    /// <summary>
    /// 表頭記錄移到第上一筆
    /// </summary>
    [Login()]
    [HttpGet]
    [Security(Mode = enSecurityMode.Display)]
    public virtual IActionResult First()
    {
        return RedirectToAction(ActionService.Navigation, ActionService.Controller, new { area = ActionService.Area, id = "First" });
    }
    /// <summary>
    /// 表頭記錄移到第上一筆
    /// </summary>
    [Login()]
    [HttpGet]
    [Security(Mode = enSecurityMode.Display)]
    public virtual IActionResult Prior()
    {
        return RedirectToAction(ActionService.Navigation, ActionService.Controller, new { area = ActionService.Area, id = "Prior" });
    }
    /// <summary>
    /// 表頭記錄移到第下一筆
    /// </summary>
    [Login()]
    [HttpGet]
    [Security(Mode = enSecurityMode.Display)]
    public virtual IActionResult Next()
    {
        return RedirectToAction(ActionService.Navigation, ActionService.Controller, new { area = ActionService.Area, id = "Next" });
    }
    /// <summary>
    /// 表頭記錄移到最得一筆
    /// </summary>
    [Login()]
    [HttpGet]
    [Security(Mode = enSecurityMode.Display)]
    public virtual IActionResult Last()
    {
        return RedirectToAction(ActionService.Navigation, ActionService.Controller, new { area = ActionService.Area, id = "Last" });
    }
    #endregion
}

public enum enNavigation
{
    /// <summary>
    /// 移到第一筆
    /// </summary>
    First,
    /// <summary>
    /// 移到上一筆
    /// </summary>
    Prior,
    /// <summary>
    /// 移到下一筆
    /// </summary>
    Next,
    /// <summary>
    /// 移到最後一筆
    /// </summary>
    Last
}