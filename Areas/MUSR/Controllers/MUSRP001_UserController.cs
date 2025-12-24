using Microsoft.AspNetCore.Mvc;

namespace powererp.Areas.Mis.Controllers
{
    /// <summary>
    /// MUSRP001 使用者資料維護
    /// </summary>
    [Area("MUSR")]
    public class MUSRP001_UserController : BaseAdminController
    {
        /// <summary>
        /// 控制器建構子
        /// </summary>
        /// <param name="configuration">環境設定物件</param>
        /// <param name="entities">EF資料庫管理物件</param>
        public MUSRP001_UserController(IConfiguration configuration, dbEntities entities)
        {
            db = entities;
            Configuration = configuration;
        }

        /// <summary>
        /// 資料初始事件
        /// </summary>
        /// <param name="id">程式編號</param>
        /// <param name="initPage">初始頁數</param>
        /// <returns></returns>
        [HttpGet]
        [Login(RoleList = "Mis")]
        [Security(Mode = enSecurityMode.Display)]
        public IActionResult Init(string id = "", int initPage = 1)
        {
            //設定程式編號及名稱
            SessionService.BaseNo = "User";
            SessionService.IsReadonlyMode = false; //非唯讀模式
            SessionService.IsLockMode = false; //非表單模式
            SessionService.IsConfirmMode = false; //非確認模式
            SessionService.IsCancelMode = false; //非作廢/結束模式
            SessionService.IsMultiMode = false; //非表頭明細模式
            //這裏可以寫入初始程式
            ActionService.ActionInit();
            //返回資料列表
            SessionService.PageMaster = initPage;
            return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
        }

        /// <summary>
        /// 資料列表
        /// </summary>
        /// <param name="id">目前頁數</param>
        /// <returns></returns>
        [HttpGet]
        [Login(RoleList = "Mis")]
        [Security(Mode = enSecurityMode.Display)]
        public ActionResult Index(int id = 1)
        {
            //設定目前頁面動作名稱、子動作名稱、動作卡片大小
            ActionService.SetActionName(enAction.Index);
            ActionService.SetSubActionName();
            ActionService.SetActionCardSize(enCardSize.Max);
            //取得資料列表集合
            using var sqlData = new z_sqlUsers();
            var modelData = sqlData.GetDataList(SessionService.SearchText);
            int pageSize = (SessionService.IsPageSize) ? SessionService.PageDetailSize : modelData.Count();
            var model = modelData.Where(m => m.RoleNo == SessionService.BaseNo).ToPagedList(SessionService.PageMaster, pageSize);
            if (!string.IsNullOrEmpty(sqlData.ErrorMessage)) SessionService.ErrorMessage = sqlData.ErrorMessage;
            //儲存目前頁面資訊
            SessionService.SetPageInfo(SessionService.PageMaster, SessionService.PageDetailSize, model.TotalItemCount);
            //設定錯誤訊息文字
            SetIndexErrorMessage();
            //設定 ViewBag 及 TempData物件
            SetIndexViewBag();
            return View(model);
        }

        /// <summary>
        /// 資料新增或修改輸入 (id = 0 為新增 , id > 0 為修改)
        /// </summary>
        /// <param name="id">要修改的Key值</param>
        /// <returns></returns>
        [HttpGet]
        [Login(RoleList = "Mis")]
        [Security(Mode = enSecurityMode.AddEdit)]
        public IActionResult CreateEdit(int id = 0)
        {
            //儲存目前 Id 值
            SessionService.Id = id;
            //設定動作名稱、子動作名稱、動作卡片大小
            ActionService.SetActionCardSize(enCardSize.Medium);
            enAction action = (id == 0) ? enAction.Create : enAction.Edit;
            ActionService.SetActionName(action);
            //取得新增或修改的資料結構及資料
            using var sqlData = new z_sqlUsers();
            var model = sqlData.GetData(id);
            //新增預設值
            if (id == 0)
            {
                model.GenderCode = "M";
                model.RoleNo = "User";
                model.IsValid = true;
            }
            return View(model);
        }

        /// <summary>
        /// 資料新增或修改存檔
        /// </summary>
        /// <param name="model">使用者輸入的資料模型</param>
        /// <returns></returns>
        [HttpPost]
        [Login(RoleList = "Mis")]
        [Security(Mode = enSecurityMode.AddEdit)]
        public IActionResult CreateEdit(Users model)
        {
            //檢查是否有違反 Metadata 中的 Validation 驗證
            if (!ModelState.IsValid) return View(model);
            //檢查是否重覆輸入值
            using var dpr = new DapperRepository();
            if (dpr.IsDuplicated(model, "UserNo"))
            {
                ModelState.AddModelError("UserNo", "編號重覆輸入!!");
                return View(model);
            }
            //執行新增或修改資料
            model.RoleNo = "User";
            using var sqlData = new z_sqlUsers();
            sqlData.CreateEdit(model, model.Id);
            //返回資料列表
            return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
        }

        /// <summary>
        /// 資料刪除
        /// </summary>
        /// <param name="id">要刪除的Key值</param>
        /// <returns></returns>
        [HttpGet]
        [Login(RoleList = "Mis")]
        [Security(Mode = enSecurityMode.Delete)]
        public override int DeletData(int id = 0)
        {
            using var sqlData = new z_sqlUsers(); return sqlData.Delete(id);
        }

        [HttpGet]
        [Login(RoleList = "Mis")]
        [Security(Mode = enSecurityMode.Edit)]
        public IActionResult ResetPassword(int id)
        {
            if (!SecurityService.CheckSecurity(enSecurityMode.Edit, id))
                return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
            using var sqlData = new z_sqlUsers();
            var user = sqlData.GetData(id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "找不到使用者資料!";
                return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
            }
            string str_password = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 5);
            var model = new vmResetPassword() { OldPassword = "super", NewPassword = str_password, ConfirmPassword = str_password };
            sqlData.ResetPassword(model);
            TempData["SuccessMessage"] = $"使用者 {user.UserNo} 的密碼已重設為 {str_password}，請通知使用者盡快修改密碼!";
            return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
        }
    }
}