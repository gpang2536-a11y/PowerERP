using Microsoft.AspNetCore.Mvc;
using powererp.Models;
using PowerERP.Models;

namespace powererp.Areas.Mis.Controllers
{
    /// <summary>
    /// UHRMP003 加班工時統計維護
    /// </summary>
    [Area("UHRM")]
    public class UHRMP003_HoursChartController : BaseAdminController
    {
        /// <summary>
        /// 控制器建構子
        /// </summary>
        /// <param name="configuration">環境設定物件</param>
        /// <param name="entities">EF資料庫管理物件</param>
        public UHRMP003_HoursChartController(IConfiguration configuration, dbEntities entities)
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
            SessionService.BaseNo = DateTime.Today.Year.ToString();
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
            //設定錯誤訊息文字
            SetIndexErrorMessage();
            //設定 ViewBag 及 TempData物件
            SetIndexViewBag();
            return View();
        }

        /// <summary>
        /// 指定年份的各月份加班時數圖表資料
        /// </summary>
        [HttpGet]
        [Login(RoleList = "User")]
        [Security(Mode = enSecurityMode.Display)]
        public JsonResult ChartData()
        {
            int int_year = int.Parse(SessionService.BaseNo);
            List<int> valueData = new List<int>();
            List<string> labels = new List<string>();
            var overtime = new z_sqlOvertimes();
            valueData = overtime.GetMonthhours(int_year);
            for (int i = 1; i <= 12; i++)
            {
                var month = $"{i} 月";
                labels.Add(month);
            }

            var result = new dmChartJS()
            {
                Title = SessionService.BaseNo + " 年各月份加班時數統計",
                SubTitle = "單位: 小時",
                YAxisTitle = "時數",
                XAxisTitle = "月份",
                SerialName = new List<string> { "加班時數" },
                Labels = labels,
                DataIntValue1 = valueData
            };
            return Json(result);
        }
    }
}