using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace powererp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        /// <summary>
        /// 儀表板首頁
        /// </summary>
        [HttpGet]
        [Login(RoleList = "Mis,User")]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(SessionService.ErrorMessage))
            {
                TempData["ErrorMessage"] = SessionService.ErrorMessage;
                SessionService.ErrorMessage = "";
            }
            ActionService.SetPrgInfo("Dashboard", "首頁儀表板");
            ActionService.SetActionName();
            ActionService.SetSubActionName();
            ActionService.SetActionCardSize(enCardSize.Max);
            return View();
        }





        // [HttpGet]
        // [Login(RoleList = "Mis,User")]
        // public IActionResult Index()
        // {
        //     if (!string.IsNullOrEmpty(SessionService.ErrorMessage))
        //     {
        //         TempData["ErrorMessage"] = SessionService.ErrorMessage;
        //         SessionService.ErrorMessage = "";
        //     }

        //     ActionService.SetPrgInfo("Dashboard", "首頁儀表板");
        //     ActionService.SetActionName();
        //     ActionService.SetSubActionName();
        //     ActionService.SetActionCardSize(enCardSize.Max);

        //     // ✅ 在 Controller 中準備資料
        //     using var calendar = new z_sqlCalendars();
        //     using var news = new z_sqlNews();

        //     ViewBag.CalendarList = calendar.GetNearCalendar("User", SessionService.UserNo, 6);
        //     ViewBag.NewsList = news.GetNearNews(6);

        //     return View();
        // }







        /// <summary>
        /// 程式未完成
        /// </summary>
        [HttpGet]
        [Login(RoleList = "Mis,User")]
        [Route("Admin/Home/Open/{prgNo}/{id?}")]
        public IActionResult Open(string prgNo, string id = "")
        {
            //取得使用者程式權限檢查值
            SecurityService.SetProgram(prgNo);

            if (!SessionService.FormSecurity.IsDisplay)
            {
                TempData["ErrorMessage"] = "您沒有權限使用此功能，請洽系統管理員！";
                return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
            }
            var prg = new z_sqlPrograms();
            var model = prg.GetData(prgNo);
            if (model == null)
            {
                TempData["ErrorMessage"] = "找不到此程式，請洽系統管理員！";
                return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
            }
            //登入記錄到日誌中
            // LogService.AddLog(new LogModel { LogCode = "Open", TargetNo = model.PrgNo, LogMessage = "開啟程式", LogNo = SessionService.UserNo });

            return RedirectToAction(model.ActionName, model.ControllerName, new { area = model.AreaName, id = model.ParmValue });
        }

        /// <summary>
        /// 個人行事曆
        /// </summary>
        [HttpGet]
        [Login(RoleList = "Mis,User")]
        public IActionResult Calendar()
        {
            ActionService.SetPrgInfo("Calendar", "個人行事曆");
            ActionService.SetActionName("個人行事曆");
            ActionService.SetSubActionName();
            ActionService.SetActionCardSize(enCardSize.Max);

            CalendarService.TargetCode = "User";
            CalendarService.TargetCodeName = "個人行事曆";
            //行事曆代號：C001 公司
            CalendarService.TargetNo = SessionService.UserNo;
            CalendarService.TargetName = SessionService.UserName;
            return View();
        }

        /// <summary>
        /// 取得行事曆某行程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetEvent(int id)
        {
            using var calendar = new z_sqlCalendars();
            CalendarService.Id = id;
            Calendars events = calendar.GetData(id);
            return Json(events);
        }

        /// <summary>
        /// 取得行事曆所有行程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetEvents()
        {
            using var calendar = new z_sqlCalendars();
            List<CalendarEvent> events = new List<CalendarEvent>();
            var data = calendar.GetDataList(CalendarService.TargetCode, CalendarService.TargetNo);
            if (data != null)
            {
                events = data.Select(e => new CalendarEvent()
                {
                    id = e.Id,
                    groupId = 0,
                    title = e.SubjectName,
                    url = "",
                    start = e.StartDate.ToString("yyyy-MM-dd") + " " + e.StartTime + ":00",
                    end = e.EndDate.ToString("yyyy-MM-dd") + " " + e.EndTime + ":00",
                    allDay = e.IsFullday,
                    description = e.Description
                }).ToList();
            }
            return Json(events);
        }

        /// <summary>
        /// 新增/修改行事曆行程
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddEditEvent()
        {
            using var calendar = new z_sqlCalendars();
            object obj_allday = Request.Form["EventAllDay"];
            string str_id = Request.Form["eventid"].ToString();
            string str_start_date = Request.Form["StartDate"].ToString();
            string str_start_hour = Request.Form["StartHour"].ToString();
            string str_start_minute = Request.Form["StartMinute"].ToString();
            string str_end_date = Request.Form["EndDate"].ToString();
            string str_end_hour = Request.Form["EndHour"].ToString();
            string str_end_minute = Request.Form["EndMinute"].ToString();
            string str_title = Request.Form["EventTitle"].ToString();
            string str_description = Request.Form["EventDescription"].ToString();
            string str_place_name = Request.Form["EventPlace"].ToString();
            string str_contact_name = Request.Form["EventContactor"].ToString();
            string str_contact_tel = Request.Form["EventContactTel"].ToString();
            string str_place_address = Request.Form["EventAddress"].ToString();
            string str_code_no = Request.Form["EventCodeNo"].ToString();
            string str_room_no = Request.Form["EventRoomNo"].ToString();
            string str_resource = Request.Form["EventResource"].ToString();
            int int_id = int.Parse(str_id);
            string str_allday = (obj_allday == null) ? "off" : obj_allday.ToString();
            str_allday = str_allday.ToLower();
            //設定欄位未輸入時的預設值
            str_start_hour = (string.IsNullOrEmpty(str_start_hour)) ? "00" : str_start_hour;
            str_start_minute = (string.IsNullOrEmpty(str_start_minute)) ? "00" : str_start_minute;
            str_end_hour = (string.IsNullOrEmpty(str_end_hour)) ? "00" : str_end_hour;
            str_end_minute = (string.IsNullOrEmpty(str_end_minute)) ? "00" : str_end_minute;
            str_title = (string.IsNullOrEmpty(str_title)) ? "未定義" : str_title;
            str_code_no = (string.IsNullOrEmpty(str_code_no)) ? "Public" : str_code_no;
            //設定時間格式
            str_start_hour = str_start_hour.PadLeft(2, '0');
            str_start_minute = str_start_minute.PadLeft(2, '0');
            str_end_hour = str_end_hour.PadLeft(2, '0');
            str_end_minute = str_end_minute.PadLeft(2, '0');

            Calendars calendarData = new Calendars();
            //修改行事曆
            if (int_id != 0)
            {
                calendarData = calendar.GetData(int_id);
                if (calendarData == null)
                {
                    return RedirectToAction("Index", ActionService.Controller, new { area = ActionService.Area });
                }
            }
            calendarData.StartDate = DateTime.Parse(str_start_date);
            calendarData.StartTime = $"{str_start_hour}:{str_start_minute}";
            calendarData.StartDateTime = DateTime.Parse(str_start_date + " " + str_start_hour + ":" + str_start_minute + ":00");
            calendarData.EndDate = DateTime.Parse(str_end_date);
            calendarData.EndTime = $"{str_end_hour}:{str_end_minute}";
            calendarData.StartDateTime = DateTime.Parse(str_end_date + " " + str_end_hour + ":" + str_end_minute + ":00");
            calendarData.SubjectName = str_title;
            calendarData.IsFullday = (str_allday == "on") ? true : false;
            calendarData.TargetCode = CalendarService.TargetCode;
            calendarData.TargetNo = CalendarService.TargetNo;
            calendarData.ColorName = "";
            calendarData.Remark = "";
            calendarData.Description = str_description;
            calendarData.PlaceName = str_place_name;
            calendarData.ContactName = str_contact_name;
            calendarData.ContactTel = str_contact_tel;
            calendarData.PlaceAddress = str_place_address;
            calendarData.RoomNo = str_room_no;
            calendarData.CodeNo = str_code_no;
            calendarData.ResourceText = str_resource;

            //新增/修改行事曆
            calendar.CreateEdit(calendarData, int_id);

            return RedirectToAction("Calendar", ActionService.Controller, new { area = ActionService.Area });
        }

        /// <summary>
        /// 刪除行事曆行程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteEvent()
        {
            using var calendar = new z_sqlCalendars();
            int int_id = CalendarService.Id;
            calendar.Delete(int_id);
            return RedirectToAction("Calendar", ActionService.Controller, new { area = ActionService.Area });
        }

        /// <summary>
        /// 前台首頁
        /// </summary>
        /// <returns></returns> 
        public IActionResult FrontendHome()
        {
            SessionService.IsLogin = false;
            SessionService.UserNo = "";
            SessionService.UserName = "訪客";
            return RedirectToAction(ActionService.Index, ActionService.Home, new { area = "" });
        }

        /// <summary>
        /// 程式未完成
        /// </summary>
        [HttpGet]
        [Login(RoleList = "Mis,User")]
        public IActionResult NotFinish()
        {
            TempData["ErrorMessage"] = "此功能尚未完成，敬請期待！";
            return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
        }

        /// <summary>
        /// 最新消息
        /// </summary>
        [HttpGet]
        [Login(RoleList = "Mis,User")]
        public IActionResult NewsMessage(int id)
        {
            ActionService.SetActionName("最新消息明細");
            ActionService.SetSubActionName();
            ActionService.SetActionCardSize(enCardSize.Medium);
            using var news = new z_sqlNews();
            var model = news.GetData(id);
            return View(model);
        }

        /// <summary>
        /// 最新消息
        /// </summary>
        [HttpGet]
        [Login(RoleList = "Mis,User")]
        public IActionResult News(int id = 1)
        {
            ActionService.SetActionName("最新消息");
            ActionService.SetSubActionName();
            ActionService.SetActionCardSize(enCardSize.Max);
            using var sqlData = new z_sqlNews();
            var model = sqlData.GetDataList().ToPagedList(id, 10);
            //儲存目前頁面資訊
            SessionService.SetPageInfo(id, 10, model.TotalItemCount);
            return View(model);
        }

        [HttpGet]
        [Login(RoleList = "Mis,User")]
        public JsonResult GetNewData(int id)
        {
            using var sqlData = new z_sqlNews();
            var model = sqlData.GetData(id);
            return Json(model);
        }
    }
}