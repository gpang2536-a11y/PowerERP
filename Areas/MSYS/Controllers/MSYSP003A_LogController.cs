using Microsoft.AspNetCore.Mvc;

namespace powererp.Areas.Mis.Controllers
{
    /// <summary>
    /// MSYSP003A 日誌資料統計表
    /// </summary>
    [Area("MSYS")]
    public class MSYSP003A_LogController : BaseAdminController
    {
        /// <summary>
        /// 控制器建構子
        /// </summary>
        /// <param name="configuration">環境設定物件</param>
        /// <param name="entities">EF資料庫管理物件</param>
        public MSYSP003A_LogController(IConfiguration configuration, dbEntities entities)
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
            SessionService.IsReadonlyMode = true; //非唯讀模式
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
            using var sqlData = new z_sqlRoles();
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
    }
}