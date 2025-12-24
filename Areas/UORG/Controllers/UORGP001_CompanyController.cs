using Microsoft.AspNetCore.Mvc;

namespace powererp.Areas.Mis.Controllers
{
    /// <summary>
    /// UORGP001 公司基本資料維護
    /// </summary>
    [Area("UORG")]
    public class UORGP001_CompanyController : BaseAdminController
    {
        /// <summary>
        /// 控制器建構子
        /// </summary>
        /// <param name="configuration">環境設定物件</param>
        /// <param name="entities">EF資料庫管理物件</param>
        public UORGP001_CompanyController(IConfiguration configuration, dbEntities entities)
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
        [Login(RoleList = "User")]
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
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Display)]
        public ActionResult Index(int id = 1)
        {
            //設定目前頁面動作名稱、子動作名稱、動作卡片大小
            ActionService.SetActionName(enAction.Index);
            ActionService.SetSubActionName();
            ActionService.SetActionCardSize(enCardSize.Max);
            //取得資料列表集合
            using var sqlData = new z_sqlCompanys();
            var modelData = sqlData.GetDataList(SessionService.SearchText);
            int pageSize = (SessionService.IsPageSize) ? SessionService.PageDetailSize : modelData.Count();
            var model = modelData.ToPagedList(SessionService.PageMaster, pageSize);
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
        [Login(RoleList = "User")]
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
            using var sqlData = new z_sqlCompanys();
            var model = sqlData.GetData(id);
            //新增預設值
            if (id == 0)
            {
                model.IsDefault = true;
                model.IsEnabled = true;
                model.RegisterDate = DateTime.Today;
                model.Latitude = 0;
                model.Longitude = 0;
            }
            return View(model);
        }

        /// <summary>
        /// 資料新增或修改存檔
        /// </summary>
        /// <param name="model">使用者輸入的資料模型</param>
        /// <returns></returns>
        [HttpPost]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.AddEdit)]
        public IActionResult CreateEdit(Companys model)
        {
            //檢查是否有違反 Metadata 中的 Validation 驗證
            if (!ModelState.IsValid) return View(model);
            //檢查是否重覆輸入值
            using var dpr = new DapperRepository();
            if (dpr.IsDuplicated(model, "CompNo"))
            {
                ModelState.AddModelError("CompNo", "編號重覆輸入!!");
                return View(model);
            }
            //執行新增或修改資料
            using var sqlData = new z_sqlCompanys();
            sqlData.CreateEdit(model, model.Id);
            //返回資料列表
            return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
        }

        /// <summary>
        /// 公司簡介
        /// </summary>
        /// <param name="id">要修改的Key值</param>
        /// <returns></returns>
        [HttpGet]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Edit)]
        public IActionResult Aboutus(int id)
        {
            SessionService.RowId = id;
            SessionService.BaseNo = "Aboutus";
            SessionService.RowData = "公司簡介";
            return RedirectToAction("Editor", ActionService.Controller, new { area = ActionService.Area, id = id });
        }

        /// <summary>
        /// 支援說明
        /// </summary>
        /// <param name="id">要修改的Key值</param>
        /// <returns></returns>
        [HttpGet]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Edit)]
        public IActionResult Support(int id)
        {
            SessionService.RowId = id;
            SessionService.BaseNo = "Support";
            SessionService.RowData = "支援說明";
            return RedirectToAction("Editor", ActionService.Controller, new { area = ActionService.Area, id = id });
        }

        /// <summary>
        /// 退貨地址
        /// </summary>
        /// <param name="id">要修改的Key值</param>
        /// <returns></returns>
        [HttpGet]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Edit)]
        public IActionResult Return(int id)
        {
            SessionService.RowId = id;
            SessionService.BaseNo = "Return";
            SessionService.RowData = "退貨地址";
            return RedirectToAction("Editor", ActionService.Controller, new { area = ActionService.Area, id = id });
        }

        /// <summary>
        /// 出貨地址
        /// </summary>
        /// <param name="id">要修改的Key值</param>
        /// <returns></returns>
        [HttpGet]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Edit)]
        public IActionResult Shipping(int id)
        {
            SessionService.RowId = id;
            SessionService.BaseNo = "Shipping";
            SessionService.RowData = "出貨地址";
            return RedirectToAction("Editor", ActionService.Controller, new { area = ActionService.Area, id = id });
        }

        /// <summary>
        /// 付款資訊
        /// </summary>
        /// <param name="id">要修改的Key值</param>
        /// <returns></returns>
        [HttpGet]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Edit)]
        public IActionResult Payment(int id)
        {
            SessionService.RowId = id;
            SessionService.BaseNo = "Payment";
            SessionService.RowData = "付款資訊";
            return RedirectToAction("Editor", ActionService.Controller, new { area = ActionService.Area, id = id });
        }

        /// <summary>
        /// 修改資料
        /// </summary>
        /// <param name="id">公司 Id</param>
        /// <returns></returns>
        [HttpGet]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Edit)]
        public IActionResult Editor(int id)
        {
            //儲存目前 Id 值
            SessionService.RowId = id;
            if (SessionService.BaseNo == "Aboutus") SessionService.RowData = "公司簡介";
            else if (SessionService.BaseNo == "Support") SessionService.RowData = "支援說明";
            else if (SessionService.BaseNo == "Return") SessionService.RowData = "退貨地址";
            else if (SessionService.BaseNo == "Shipping") SessionService.RowData = "出貨地址";
            else if (SessionService.BaseNo == "Payment") SessionService.RowData = "付款資訊";
            else SessionService.RowData = "";
            //設定動作名稱、子動作名稱、動作卡片大小
            ActionService.SetActionCardSize(enCardSize.Max);
            ActionService.SetActionName(enAction.Edit);
            //取得新增或修改的資料結構及資料
            using var sqlData = new z_sqlCompanys();
            var model = sqlData.GetEditor(id, SessionService.BaseNo);
            return View(model);
        }

        /// <summary>
        /// 修改公司資料
        /// </summary>
        /// <param name="model">公司資料</param>
        /// <returns></returns>
        [HttpPost]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Edit)]
        public IActionResult Editor(Companys model)
        {
            //執行新增或修改資料
            using var sqlData = new z_sqlCompanys();
            sqlData.SetEditor(model, SessionService.BaseNo);
            //返回資料列表
            return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area, page = SessionService.Page });
        }

        /// <summary>
        /// CKEditor 圖片上傳
        /// </summary>
        /// <param name="upload">上傳的圖片檔案</param>
        /// <returns>JSON 格式的圖片 URL</returns>
        [HttpPost]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Upload)]
        public async Task<IActionResult> CKEditorUploadImage(IFormFile upload)
        {
            var imageService = new ImageService();
            var result = await imageService.CKEditorUploadImageAsync(upload, "company");
            return Json(result);
        }
    }
}