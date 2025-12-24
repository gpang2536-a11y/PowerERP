using Microsoft.AspNetCore.Mvc;

namespace powererp.Areas.Mis.Controllers
{
    /// <summary>
    /// UORGP002 採購入庫單資料維護
    /// </summary>
    [Area("UINV")]
    public class UINVP001_InventoryController : BaseAdminController
    {
        /// <summary>
        /// 控制器建構子
        /// </summary>
        /// <param name="configuration">環境設定物件</param>
        /// <param name="entities">EF資料庫管理物件</param>
        public UINVP001_InventoryController(IConfiguration configuration, dbEntities entities)
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
            SessionService.IsLockMode = true; //表單模式
            SessionService.IsConfirmMode = true; //確認模式
            SessionService.IsCancelMode = true; //非作廢/結束模式
            SessionService.IsMultiMode = true; //表頭明細模式
            //這裏可以寫入初始程式
            ActionService.ActionInit();
            //設定程式編號及名稱
            if (!SecurityService.CheckSecurity(enSecurityMode.Display))
                return RedirectToAction(AppService.MenuAction, AppService.MenuController, new { area = AppService.MenuArea });
            //返回資料列表
            return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
        }

        /// <summary>
        /// 資料列表
        /// </summary>
        /// <param name="id">表頭記錄 ID 值</param>
        /// <returns></returns>
        [HttpGet]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Display)]
        public ActionResult Index(int id = 0)
        {
            using var sqlData = new z_sqlInventorys();
            if (SessionService.PageMaster == -1) sqlData.SetLastData(); //未指定那一筆時，取得最後一筆表頭資料

            //設定目前頁面動作名稱、子動作名稱、動作卡片大小
            ActionService.SetActionName(enAction.Index);
            ActionService.SetSubActionName();
            ActionService.SetActionCardSize(enCardSize.Max);
            //取得資料列表集合
            using var model = new vmUINVP001_Inventory();
            //取得月份資料(統計圖表使用)
            if (model != null && model.MasterModel != null && model.MasterModel.SheetDate != null)
                SessionService.StringValue1 = model.MasterModel.SheetDate?.ToString("yyyy-MM-dd");
            else
                SessionService.StringValue1 = DateTime.Today.ToString("yyyy-MM-dd");
            //設定錯誤訊息文字
            SetIndexErrorMessage();
            //設定 ViewBag 及 TempData物件
            SetIndexViewBag();
            //設定表單狀態
            SessionService.IsConfirm = false;
            SessionService.IsCancel = false;
            SessionService.IsFormLocked = false;
            SetFormStatus(model.MasterModel.IsConfirm, model.MasterModel.IsCancel, model.MasterModel.SheetDate);
            return View(model);
        }

        /// <summary>
        /// 表頭資料列表
        /// </summary>
        /// <param name="id">頁次</param>
        [HttpGet]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Display)]
        public IActionResult List(int id = 1)
        {
            //設定目前頁面動作名稱、子動作名稱、動作卡片大小
            ActionService.SetActionName(enAction.List);
            ActionService.SetSubActionName();
            ActionService.SetActionCardSize(enCardSize.Max);
            using var inv = new z_sqlInventorys();
            var model = inv.GetDataList(SessionService.SearchText).ToPagedList(id, 10);
            ViewBag.SearchText = SessionService.SearchText;
            return View(model);
        }

        /// <summary>
        /// 表頭資料選取
        /// </summary>
        /// <param name="id">記錄 Id</param>
        [HttpGet]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Display)]
        public override IActionResult Select(int id = 0)
        {
            using var inv = new z_sqlInventorys();
            var model = inv.GetData(id);
            inv.SetBaseNo(model.BaseNo);
            return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
        }

        /// <summary>
        /// 表頭記錄移動
        /// </summary>
        /// <param name="id">移動方向</param>
        [HttpGet]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Display)]
        public override IActionResult Navigation(string id)
        {
            var sqlData = new z_sqlInventorys();
            if (id == "First")
                sqlData.SetFirstData();
            else if (id == "Prior")
                sqlData.SetPriorData();
            else if (id == "Next")
                sqlData.SetNextData();
            else if (id == "Last")
                sqlData.SetLastData();
            return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
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
            if (id == 0 && SessionService.PageMaster == 0)
            {
                TempData["ErrorMessage"] = "請先建立表頭資料!";
                return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
            }
            //儲存目前 Id 值
            SessionService.Id = id;
            //設定動作名稱、子動作名稱、動作卡片大小
            ActionService.SetActionCardSize(enCardSize.Medium);
            enAction action = (id == 0) ? enAction.Create : enAction.Edit;
            ActionService.SetActionName(action);
            //取得新增或修改的資料結構及資料
            using var sqlMaster = new z_sqlInventorys();
            using var sqlDetail = new z_sqlInventorysDetail();
            var masterModel = sqlMaster.GetMasterData();
            var detailModel = sqlDetail.GetData(id);
            //新增預設值
            if (id == 0)
            {
                detailModel.ParentNo = masterModel.BaseNo;
                detailModel.Qty = 1;
            }
            return View(detailModel);
        }

        /// <summary>
        /// 資料新增或修改存檔
        /// </summary>
        /// <param name="model">使用者輸入的資料模型</param>
        /// <returns></returns>
        [HttpPost]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.AddEdit)]
        public IActionResult CreateEdit(InventorysDetail model)
        {
            //檢查是否有違反 Metadata 中的 Validation 驗證
            if (!ModelState.IsValid) return View(model);
            //執行新增或修改資料
            using var sqlData = new z_sqlInventorysDetail();
            sqlData.CreateEdit(model, model.Id);
            //返回資料列表
            return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
        }

        /// <summary>
        /// 資料新增或修改輸入 (id = 0 為新增 , id > 0 為修改)
        /// </summary>
        /// <param name="id">要修改的Key值</param>
        /// <returns></returns>
        [HttpGet]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.AddEdit)]
        public IActionResult CreateEditMaster(int id = 0)
        {
            //儲存目前 Id 值
            SessionService.Id = id;
            //設定動作名稱、子動作名稱、動作卡片大小
            ActionService.SetActionCardSize(enCardSize.Medium);
            enAction action = (id == 0) ? enAction.Create : enAction.Edit;
            ActionService.SetActionName(action);
            //取得新增或修改的資料結構及資料
            using var sqlData = new z_sqlInventorys();
            using var sqlCode = new z_sqlInventorysType();
            var codeData = sqlCode.GetData(SessionService.BaseNo);
            string sheetCode = "I";
            if (codeData != null) sheetCode = codeData.QtyCode == 1 ? "I" : "O";
            var model = sqlData.GetData(id);
            //新增預設值
            if (id == 0)
            {
                model.Id = 0;
                model.IsConfirm = false;
                model.IsCancel = false;
                model.TypeNo = SessionService.BaseNo;
                model.SheetCode = sheetCode;
                model.SheetDate = DateTime.Today;
                model.HandleNo = SessionService.UserNo;
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
        public IActionResult CreateEditMaster(Inventorys model)
        {
            //檢查是否有違反 Metadata 中的 Validation 驗證
            if (!ModelState.IsValid) return View(model);
            //執行新增或修改資料
            using var sqlCode = new z_sqlInventorysType();
            var codeData = sqlCode.GetData(SessionService.BaseNo);
            string sheetCode = "I";
            if (codeData != null) sheetCode = codeData.QtyCode == 1 ? "I" : "O";
            string str_base_no = "";
            if (model.Id == 0)
            {
                str_base_no = Guid.NewGuid().ToString();
                model.BaseNo = str_base_no;
            }
            model.TypeNo = SessionService.BaseNo;
            model.SheetCode = sheetCode;
            using var sqlData = new z_sqlInventorys();
            sqlData.CreateEdit(model, model.Id);
            //設定目前的記錄位置
            if (!string.IsNullOrEmpty(str_base_no)) sqlData.SetBaseNo(str_base_no);
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
            using var sqlData = new z_sqlInventorysDetail(); return sqlData.Delete(id);
        }

        /// <summary>
        /// 資料主檔刪除
        /// </summary>
        /// <param name="id">要刪除的Key值</param>
        /// <returns></returns> 
        [HttpGet]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Delete)]
        public override int DeletDataMaster(int id = 0)
        {
            using var sqlMaster = new z_sqlInventorys();
            using var sqlDetail = new z_sqlInventorysDetail();
            //先刪除明細資料
            sqlDetail.DeleteMasterData(id);
            //再刪除主檔資料
            int int_row = sqlMaster.Delete(id);
            SessionService.PageCountMaster -= 1;
            if (SessionService.PageCountMaster < 1) SessionService.PageCountMaster = 0;

            SessionService.PageMaster -= 1;
            if (SessionService.PageMaster < 1) SessionService.PageMaster = 1;
            if (SessionService.PageMaster > SessionService.PageCountMaster) SessionService.PageMaster = SessionService.PageCountMaster;
            using var sqlData = new z_sqlInventorys();
            sqlData.GetMasterData(SessionService.PageMaster);
            return int_row;
        }

        /// <summary>
        /// 當前庫存類別、倉庫的當月份圖表資料
        /// </summary>
        [HttpGet]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Display)]
        public JsonResult ChartData()
        {
            var invDetail = new z_sqlInventorysDetail();
            var model1 = invDetail.GetInventoryDailyQty("PURIN", SessionService.StringValue1);
            var model2 = invDetail.GetInventoryDailyQty("SALOUT", SessionService.StringValue1);

            var result = new dmChartJS()
            {
                Title = DateTime.Parse(SessionService.StringValue1).ToString("yyyy年MM月") + "庫存異動數量統計",
                SubTitle = "單位: 件",
                YAxisTitle = "數量",
                XAxisTitle = "日期",
                SerialName = new List<string> { "進貨入庫數量", "銷售出庫數量" },
                Labels = Enumerable.Range(1, model1.Count).Select(i => i.ToString()).ToList(),
                DataIntValue1 = model1,
                DataIntValue2 = model2
            };
            return Json(result);
        }

        /// <summary>
        /// 表單確認
        /// </summary>
        /// <param name="id">要確認的Key值</param>
        /// <returns></returns>
        public override bool Confirm(int id = 0)
        {
            using var sqlData = new z_sqlInventorys();
            bool bln_result = sqlData.Confirm(id);
            MessageText = sqlData.ErrorMessage;
            return bln_result;
        }

        /// <summary>
        /// 表單作廢
        /// </summary>
        /// <param name="id">要作廢的Key值</param>
        public override bool Cancel(int id = 0)
        {
            using var sqlData = new z_sqlInventorys();
            bool bln_result = sqlData.Cancel(id);
            MessageText = sqlData.ErrorMessage;
            return bln_result;
        }

        /// <summary>
        /// 表單取消確認
        /// </summary>
        /// <param name="id">要取消確認的Key值</param>
        /// <returns></returns>
        public override bool Undo(int id = 0)
        {
            using var sqlData = new z_sqlInventorys();
            bool bln_result = sqlData.Undo(id);
            MessageText = sqlData.ErrorMessage;
            return bln_result;
        }
    }
}