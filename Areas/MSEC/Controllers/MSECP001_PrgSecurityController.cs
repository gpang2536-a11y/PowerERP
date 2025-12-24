using Microsoft.AspNetCore.Mvc;

namespace powererp.Areas.Mis.Controllers
{
    /// <summary>
    /// MSECP001 程式權限設定
    /// </summary>
    [Area("MSEC")]
    public class MSECP001_PrgSecurityController : BaseAdminController
    {
        /// <summary>
        /// 控制器建構子
        /// </summary>
        /// <param name="configuration">環境設定物件</param>
        /// <param name="entities">EF資料庫管理物件</param>
        public MSECP001_PrgSecurityController(IConfiguration configuration, dbEntities entities)
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
            SessionService.BaseNo = id;
            SessionService.IsReadonlyMode = false; //非唯讀模式
            SessionService.IsLockMode = false; //非表單模式
            SessionService.IsConfirmMode = false; //非確認模式
            SessionService.IsCancelMode = false; //非作廢/結束模式
            SessionService.IsMultiMode = false; //非表頭明細模式
            //未設定主檔編號則取第一筆
            if (string.IsNullOrEmpty(SessionService.BaseNo))
            {
                var sqlData = new z_sqlPrograms();
                SessionService.BaseNo = sqlData.GetDataList().OrderBy(m => m.PrgNo).FirstOrDefault().PrgNo;
            }
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
            using var sqlData = new z_sqlSecuritys();
            var modelData = sqlData.GetPrgDataList(SessionService.BaseNo, SessionService.SearchText);
            int pageSize = (SessionService.IsPageSize) ? SessionService.PageDetailSize : modelData.Count();
            var model = modelData.ToPagedList(SessionService.PageMaster, pageSize);
            if (!string.IsNullOrEmpty(sqlData.ErrorMessage)) SessionService.ErrorMessage = sqlData.ErrorMessage;
            //儲存目前頁面資訊
            SessionService.SetPageInfo(SessionService.PageMaster, SessionService.PageDetailSize, model.TotalItemCount);
            //設定錯誤訊息文字
            SetIndexErrorMessage();
            //設定 ViewBag 及 TempData物件
            SetIndexViewBag();
            //設定模組下拉選單
            using var prg = new z_sqlPrograms();
            var prgList = prg.GetDropDownList(true);
            prgList.Where(m => m.Value == SessionService.BaseNo).ToList().ForEach(m => m.Selected = true);
            ViewBag.PrgList = prgList;
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
            using var sqlData = new z_sqlSecuritys();
            var model = sqlData.GetData(id);
            //新增預設值
            if (id == 0)
            {
                model.PrgNo = SessionService.BaseNo;
                model.IsAdd = true;
                model.IsConfirm = true;
                model.IsDelete = true;
                model.IsDownload = true;
                model.IsEdit = true;
                model.IsCancel = true;
                model.IsPrint = true;
                model.IsUndo = true;
                model.IsUpload = true;
                model.Remark = "";
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
        public IActionResult CreateEdit(Securitys model)
        {
            //檢查是否有違反 Metadata 中的 Validation 驗證
            if (!ModelState.IsValid) return View(model);
            //檢查是否重覆輸入值
            using var dpr = new DapperRepository();
            string condition = $" Securitys.PrgNo = '{SessionService.BaseNo}' ";
            if (dpr.IsDuplicated(model, "TargetNo", condition))
            {
                ModelState.AddModelError("TargetNo", "使用者代號重覆輸入!!");
                return View(model);
            }
            //執行新增或修改資料
            using var prg = new z_sqlPrograms();
            using var user = new z_sqlUsers();
            var prgData = prg.GetData(SessionService.BaseNo);
            var userData = user.GetData(model.TargetNo);
            model.PrgNo = SessionService.BaseNo;
            model.ModuleNo = prgData.ModuleNo;
            model.RoleNo = userData.RoleNo;
            using var sqlData = new z_sqlSecuritys();
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
            using var sqlData = new z_sqlSecuritys(); return sqlData.Delete(id);
        }
    }
}