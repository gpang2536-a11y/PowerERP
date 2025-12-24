using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;

namespace powererp.Areas.Mis.Controllers
{
    /// <summary>
    /// UORGP003 職稱基本資料維護
    /// </summary>
    [Area("UORG")]
    public class UORGP004_EmployeeController : BaseAdminController
    {
        /// <summary>
        /// 控制器建構子
        /// </summary>
        /// <param name="configuration">環境設定物件</param>
        /// <param name="entities">EF資料庫管理物件</param>
        public UORGP004_EmployeeController(IConfiguration configuration, dbEntities entities)
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
            using var sqlData = new z_sqlEmployees();
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
            if (!SecurityService.CheckSecurity(enSecurityMode.AddEdit, id))
                return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
            //儲存目前 Id 值
            SessionService.Id = id;
            //設定動作名稱、子動作名稱、動作卡片大小
            ActionService.SetActionCardSize(enCardSize.Medium);
            enAction action = (id == 0) ? enAction.Create : enAction.Edit;
            ActionService.SetActionName(action);
            //取得新增或修改的資料結構及資料
            using var sqlData = new z_sqlEmployees();
            var model = sqlData.GetData(id);
            //新增預設值
            if (id == 0)
            {

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
        public IActionResult CreateEdit(Employees model)
        {
            //檢查是否有違反 Metadata 中的 Validation 驗證
            if (!ModelState.IsValid) return View(model);
            //檢查是否重覆輸入值
            using var dpr = new DapperRepository();
            if (dpr.IsDuplicated(model, "EmpNo"))
            {
                ModelState.AddModelError("EmpNo", "編號重覆輸入!!");
                return View(model);
            }
            //執行新增或修改資料
            using var sqlData = new z_sqlEmployees();
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
            using var sqlData = new z_sqlEmployees(); return sqlData.Delete(id);
        }

        /// <summary>
        /// 取得城市地區列表
        /// </summary>
        /// <param name="id">縣市名稱</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCityAreaList(string id)
        {
            using var cityArea = new z_sqlCityAreas();
            var model = cityArea.GetDropDownList(id);
            return Json(model);
        }

        [HttpGet]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Upload)]
        public ActionResult Upload(int id)
        {
            SessionService.BaseNo = id.ToString();
            ActionService.SetActionName("上傳照片");
            ActionService.SetSubActionName("員工資料");
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
            using var sqlData = new z_sqlEmployees();
            int id = int.Parse(SessionService.BaseNo);
            var emp = sqlData.GetData(id);
            string errorMsg = imageService.UploadImage(file, "employees", $"{emp.EmpNo}.jpg");
            if (!string.IsNullOrEmpty(errorMsg)) TempData["ErrorMessage"] = errorMsg;
            return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
        }

        /// <summary>
        /// 匯出資料
        /// </summary>
        [HttpGet]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Download)]
        public IActionResult Export()
        {
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string currentTime = DateTime.Now.ToString("yyyyMMddHHmm");
            string fileName = $"員工資料列表_{currentTime}.xlsx";
            using var sqlEmployee = new z_sqlEmployees();
            var workbook = sqlEmployee.ExportToExcel();
            //寫入檔案並下載
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, contentType, fileName);
            }
        }

        /// <summary>
        /// 明細資料
        /// </summary>
        /// <param name="id">資料 Id 值</param>
        /// <returns></returns>
        [HttpGet]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Display)]
        public override IActionResult Detail(int id = 0)
        {
            //設定目前頁面動作名稱、子動作名稱、動作卡片大小
            ActionService.SetActionName(enAction.Detail);
            ActionService.SetSubActionName();
            ActionService.SetActionCardSize(enCardSize.Medium);
            var model = new Employees();
            //取得單筆明細資料
            using var sqlData = new z_sqlEmployees();
            model = sqlData.GetData(id);
            return View(model);
        }

        /// <summary>
        /// 員工附件管理
        /// </summary>
        /// <param name="id">員工 Id</param>
        /// <returns></returns>
        [HttpGet]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Upload)]
        public ActionResult Attachment(int id)
        {
            SessionService.DetailId = id;
            using var sqlEmp = new z_sqlEmployees();
            var userModel = sqlEmp.GetData(id);
            if (userModel.Id <= 0) return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area, page = SessionService.Page });
            //儲存員工編號及姓名到 SessionService
            SessionService.IntValue1 = id;
            SessionService.StringValue1 = userModel.EmpNo;
            SessionService.StringValue2 = userModel.EmpName;
            //取得資料列表集合
            var folderService = new FolderService("~/files/employee", userModel.EmpNo);
            var model = folderService.GetFileInfos();
            return View(model);
        }

        /// <summary>
        /// 儲存上傳的員工附件
        /// </summary>
        /// <param name="file">上傳的員工附件</param>
        /// <returns></returns>
        [HttpPost]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Upload)]
        public ActionResult Attachment(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                string projectRoot = Directory.GetCurrentDirectory();
                string webFolder = Path.Combine(projectRoot, "wwwroot", "files", "employee");

                if (!Directory.Exists(webFolder)) Directory.CreateDirectory(webFolder);

                webFolder = Path.Combine(webFolder, SessionService.StringValue1);
                if (!Directory.Exists(webFolder)) Directory.CreateDirectory(webFolder);

                string fileName = file.FileName;
                string filePathName = Path.Combine(webFolder, fileName);

                try
                {
                    if (System.IO.File.Exists(filePathName)) System.IO.File.Delete(filePathName);
                    using var stream = System.IO.File.Create(filePathName);
                    file.CopyTo(stream);
                }
                catch (Exception ex)
                {
                    TempData["MessageText"] = ex.Message;
                }
            }

            return RedirectToAction("Attachment", ActionService.Controller, new { area = ActionService.Area, id = SessionService.IntValue1 });
        }

        /// <summary>
        /// 刪除員工附件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Delete)]
        public ActionResult DeleteAttachment()
        {
            object objfileName = Request.Form["fileName"];
            object objfileFullName = Request.Form["fileFullName"];
            string fileName = objfileName?.ToString() ?? "";
            string fileFullName = objfileFullName?.ToString() ?? "";
            if (System.IO.File.Exists(fileFullName))
            {
                System.IO.File.Delete(fileFullName);
                TempData["MessageText"] = $"{fileName} 已刪除";
            }
            else
            {
                TempData["MessageText"] = $"{fileName} 不存在";
            }
            return RedirectToAction("Attachment", ActionService.Controller, new { area = ActionService.Area, id = SessionService.IntValue1 });
        }

        /// <summary>
        /// 下載員工附件
        /// </summary>
        /// <param name="fileName">要下載的檔案名稱</param>
        /// <returns></returns>
        [HttpPost]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Download)]
        public ActionResult DownloadAttachment(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                TempData["MessageText"] = "檔案名稱不可為空";
                return RedirectToAction("Attachment", ActionService.Controller, new { area = ActionService.Area, id = SessionService.IntValue1 });
            }

            string projectRoot = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(projectRoot, "wwwroot", "files", "employee", SessionService.StringValue1, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                TempData["MessageText"] = $"檔案 {fileName} 不存在";
                return RedirectToAction("Attachment", ActionService.Controller, new { area = ActionService.Area, id = SessionService.IntValue1 });
            }

            try
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                var folderService = new FolderService();
                string contentType = folderService.GetContentType(fileName);
                return File(fileBytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                TempData["MessageText"] = $"下載檔案時發生錯誤: {ex.Message}";
                return RedirectToAction("Attachment", ActionService.Controller, new { area = ActionService.Area, id = SessionService.IntValue1 });
            }
        }
    }
}