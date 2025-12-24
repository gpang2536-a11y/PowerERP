using Microsoft.AspNetCore.Mvc;

namespace powererp.Areas.Mis.Controllers
{
    /// <summary>
    /// UHOMP003 客戶評價資料維護
    /// </summary>
    [Area("UHOM")]
    public class UHOMP003_TestimonialController : BaseAdminController
    {
        /// <summary>
        /// 控制器建構子
        /// </summary>
        /// <param name="configuration">環境設定物件</param>
        /// <param name="entities">EF資料庫管理物件</param>
        public UHOMP003_TestimonialController(IConfiguration configuration, dbEntities entities)
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
            using var sqlData = new z_sqlTestimonials();
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
            using var sqlData = new z_sqlTestimonials();
            var model = sqlData.GetData(id);
            //新增預設值
            if (id == 0)
            {
                model.SendDate = DateTime.Today;
                model.StarCount = 5;
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
        public IActionResult CreateEdit(Testimonials model)
        {
            if (!SecurityService.CheckSecurity(enSecurityMode.AddEdit, model.Id))
                return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
            //檢查是否有違反 Metadata 中的 Validation 驗證
            if (!ModelState.IsValid) return View(model);
            //執行新增或修改資料
            using var sqlData = new z_sqlTestimonials();
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
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Delete)]
        public override int DeletData(int id = 0)
        {
            using var sqlData = new z_sqlTestimonials(); return sqlData.Delete(id);
        }

        [HttpGet]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Upload)]
        public ActionResult Upload(int id)
        {
            SessionService.BaseNo = id.ToString();
            ActionService.SetActionName("上傳照片");
            ActionService.SetSubActionName("客戶評價");
            ActionService.SetActionCardSize(enCardSize.Medium);
            return View();
        }

        /// <summary>
        /// 圖片上傳
        /// </summary>
        /// <param name="file">上傳的圖片檔案</param>
        /// <returns>錯誤訊息</returns>
        [HttpPost]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Upload)]
        public ActionResult Upload(IFormFile file)
        {
            var imageService = new ImageService();
            using var sqlData = new z_sqlTestimonials();
            int id = int.Parse(SessionService.BaseNo);
            var data = sqlData.GetData(id);
            string errorMsg = imageService.UploadImage(file, "testimonials", $"{data.UserNo}.jpg");
            if (!string.IsNullOrEmpty(errorMsg)) TempData["ErrorMessage"] = errorMsg;
            return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
        }
    }
}