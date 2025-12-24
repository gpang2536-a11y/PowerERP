using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using powererp.Models;

namespace powererp.Controllers;

public class HomeController : Controller
{
    /// <summary>
    /// 前台首頁
    /// </summary>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// 訊息發送
    /// </summary>
    public IActionResult UserMessage()
    {
        string str_message = "您的訊息已發送，謝謝您的寶貴意見！！";
        object? email = Request.Form["email"];
        string str_email = email == null ? "" : email.ToString() ?? "";
        if (string.IsNullOrEmpty(str_email)) str_message = "請輸入電子郵件信箱！";
        TempData["SuccessMessage"] = str_message;
        return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = "" });
    }

    /// <summary>
    /// 訂閱電子報
    /// </summary>
    public IActionResult Newsletter()
    {
        string str_message = "感謝您的訂閱，我們將會寄送最新消息到您的信箱！";
        object? email = Request.Form["email"];
        string str_email = email == null ? "" : email.ToString() ?? "";
        if (string.IsNullOrEmpty(str_email)) str_message = "請輸入電子郵件信箱！";
        TempData["SuccessMessage"] = str_message;
        return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = "" });
    }
}
