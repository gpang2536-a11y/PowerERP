using Microsoft.AspNetCore.Mvc;

namespace powererp.Areas.Mis.Controllers
{
    /// <summary>
    /// UINVP004 倉庫庫存現況查詢
    /// </summary>
    [Area("UINV")]
    public class UINVP004_WarehouseStatusController : BaseAdminController
    {
        /// <summary>
        /// 控制器建構子
        /// </summary>
        /// <param name="configuration">環境設定物件</param>
        /// <param name="entities">EF資料庫管理物件</param>
        public UINVP004_WarehouseStatusController(IConfiguration configuration, dbEntities entities)
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
            SessionService.IsReadonlyMode = true; //唯讀模式
            SessionService.IsLockMode = false; //非表單模式
            SessionService.IsConfirmMode = false; //非確認模式
            SessionService.IsCancelMode = false; //非作廢/結束模式
            SessionService.IsMultiMode = true; //表頭明細模式
            SessionService.PageDetail = 1; //明細頁數
            //這裏可以寫入初始程式
            ActionService.ActionInit();
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
            using var sqlData = new z_sqlWarehouses();
            if (SessionService.PageMaster == -1) sqlData.SetFirstData(); //未指定那一筆時，取得第一筆表頭資料

            //設定目前頁面動作名稱、子動作名稱、動作卡片大小
            ActionService.SetActionName(enAction.Index);
            ActionService.SetSubActionName();
            ActionService.SetActionCardSize(enCardSize.Max);
            //取得資料列表集合
            using var model = new vmUINVP004_WarehouseStatus();
            //設定錯誤訊息文字
            SetIndexErrorMessage();
            //儲存目前頁面資訊
            SessionService.SetPageDetailInfo(SessionService.PageDetail, SessionService.PageDetailSize, model.DetailModel.TotalItemCount);
            //設定 ViewBag 及 TempData物件
            SetIndexViewBag();
            //設定表單狀態
            SessionService.IsConfirm = false;
            SessionService.IsCancel = false;
            SessionService.IsFormLocked = false;
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
            using var inv = new z_sqlWarehouses();
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
            using var inv = new z_sqlWarehouses();
            var model = inv.GetData(id);
            inv.SetBaseNo(model.WarehouseNo);
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
            var sqlData = new z_sqlWarehouses();
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
            using var sqlMaster = new z_sqlWarehouses();
            using var sqlDetail = new z_sqlInvDetails();
            var masterModel = sqlMaster.GetMasterData();
            var detailModel = sqlDetail.GetData(id);
            //新增預設值
            if (id == 0)
            {
                detailModel.WareHouseNo = masterModel.WarehouseNo;
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
        public IActionResult CreateEdit(InvDetails model)
        {
            //檢查是否有違反 Metadata 中的 Validation 驗證
            if (!ModelState.IsValid) return View(model);
            //執行新增或修改資料
            using var sqlData = new z_sqlInvDetails();
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
            using var sqlData = new z_sqlWarehouses();
            using var sqlCode = new z_sqlInvDetails();
            var codeData = sqlCode.GetData(SessionService.BaseNo);
            var model = sqlData.GetData(id);
            //新增預設值
            if (id == 0)
            {
                model.Id = 0;
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
        public IActionResult CreateEditMaster(Warehouses model)
        {
            //檢查是否有違反 Metadata 中的 Validation 驗證
            if (!ModelState.IsValid) return View(model);
            //執行新增或修改資料
            string str_base_no = "";
            if (model.Id == 0)
            {
                str_base_no = Guid.NewGuid().ToString();
                model.WarehouseNo = str_base_no;
            }
            using var sqlData = new z_sqlWarehouses();
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
            using var sqlData = new z_sqlInvDetails(); return sqlData.Delete(id);
        }

        /// <summary>
        /// 資料主檔刪除
        /// </summary>
        /// <param name="id">要刪除的商品編號</param>
        /// <returns></returns> 
        [HttpGet]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Delete)]
        public override int DeletDataMaster(int id = 0)
        {
            using var sqlMaster = new z_sqlWarehouses();
            using var sqlDetail = new z_sqlInvDetails();
            //先刪除明細資料
            var model = sqlMaster.GetData(id);
            sqlDetail.DeleteMasterData(model.WarehouseNo);
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
            var invDetail = new z_sqlInvDetails();
            string str_today = DateTime.Today.ToString("yyyy-MM") + "-01";
            var model1 = invDetail.GetProductYearQty("I", SessionService.MasterNo, str_today);
            var model2 = invDetail.GetProductYearQty("O", SessionService.MasterNo, str_today);
            var DateEnd = DateTime.Parse(str_today);
            List<string> labels = new List<string>();
            int int_index = 0;
            for (int i = 0; i < 12; i++)
            {
                int_index = i * -1;
                var month = DateEnd.AddMonths(int_index).ToString("yyyy/MM");
                labels.Add(month);
            }

            var result = new dmChartJS()
            {
                Title = SessionService.MasterNo + " 前 12 個月庫存異動數量統計",
                SubTitle = "單位: 件",
                YAxisTitle = "數量",
                XAxisTitle = "月份",
                SerialName = new List<string> { "入庫數量", "出庫數量" },
                Labels = labels,
                DataIntValue1 = model1,
                DataIntValue2 = model2
            };
            return Json(result);
        }
    }
}