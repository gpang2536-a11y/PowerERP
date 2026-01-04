using System.Xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace powererp.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// 登出系統
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Logout()
        {
            SessionService.IsLogin = false;
            return RedirectToAction(ActionService.Login, ActionService.Controller, new { area = "" });
        }

        /// <summary>
        /// 登入系統 (GET)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            ActionService.SetPrgInfo("admin", "後台管理系統");
            ActionService.SetActionName(enAction.Login);
            ActionService.SetSubActionName();
            ActionService.SetActionCardSize(enCardSize.Medium);
            var model = new vmLogin();
            return View(model);
        }

        /// <summary>
        /// 登入系統 (POST) - 使用原生非同步方法
        /// </summary>
        /// <param name="model">使用者輸入的資料模型</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(vmLogin model)
        {
            try
            {
                // 檢查輸入資料是否合格
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                // 使用非同步方式檢查登入帳號及密碼
                using var user = new z_sqlUsers();

                // ✅ 使用原生非同步方法
                var isValidLogin = await user.CheckLoginAsync(model);

                if (!isValidLogin)
                {
                    ModelState.AddModelError("UserNo", "登入帳號或密碼輸入不正確!!");
                    model.UserNo = "";
                    model.Password = "";
                    return View(model);
                }

                // 登入記錄到日誌中
                // await LogService.AddLogAsync(new LogModel { LogCode = "Login", TargetNo = model.UserNo, LogMessage = "使用者登入" });

                // 判斷使用者角色，進入不同的首頁
                var data = await user.GetDataAsync(model.UserNo);

                if (data.RoleNo == "Mis" || data.RoleNo == "User")
                {
                    return RedirectToAction(ActionService.Index, ActionService.Home, new { area = "Admin" });
                }

                if (data.RoleNo == "Member")
                {
                    return RedirectToAction("Index", "Home", new { area = "" });
                }

                // 角色不正確,引發自定義錯誤,並重新輸入
                ModelState.AddModelError("UserNo", "登入帳號角色設定不正確!!");
                model.UserNo = "";
                model.Password = "";
                return View(model);
            }
            catch (Exception ex)
            {
                // 記錄錯誤
                Console.WriteLine($"登入錯誤: {ex.Message}");
                Console.WriteLine($"堆疊追蹤: {ex.StackTrace}");

                ModelState.AddModelError("", "登入過程發生錯誤，請稍後再試。");
                model.Password = "";
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous()]
        public IActionResult Register()
        {
            ActionService.SetPrgInfo("admin", "後台管理系統");
            ActionService.SetActionName(enAction.Register);
            ActionService.SetSubActionName();
            ActionService.SetActionCardSize(enCardSize.Medium);
            vmRegister model = new vmRegister();
            model.GenderCode = "M";
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(vmRegister model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                // 檢查登入帳號及電子信箱是否有重覆
                using var user = new z_sqlUsers();

                var checkUserNo = await user.CheckRegisterUserNoAsync(model.UserNo);
                if (!checkUserNo)
                {
                    ModelState.AddModelError("UserNo", "登入帳號重覆註冊!!");
                    return View(model);
                }

                var checkEmail = await user.CheckRegisterEmailAsync(model.Email);
                if (!checkEmail)
                {
                    ModelState.AddModelError("Email", "電子信箱重覆註冊!!");
                    return View(model);
                }

                // 新增未審核的使用者記錄
                string str_code = await user.RegisterNewUserAsync(model);

                // 寄出驗證信
                using var sendEmail = new SendMailService();
                string str_message = await user.CheckMailValidateCodeAsync(str_code);

                if (string.IsNullOrEmpty(str_message))
                {
                    var userData = await user.GetValidateUserAsync(str_code);
                    var mailObject = new MailObject
                    {
                        MailTime = DateTime.Now,
                        ValidateCode = str_code,
                        UserNo = userData.UserNo,
                        UserName = userData.UserName,
                        ToName = userData.UserName,
                        ToEmail = userData.ContactEmail,
                        ReturnUrl = $"{ActionService.HttpHost}/User/RegisterValidate/{str_code}"
                    };

                    str_message = await Task.Run(() => sendEmail.UserRegister(mailObject));
                    if (string.IsNullOrEmpty(str_message))
                    {
                        str_message = "您的註冊資訊已建立，請記得收信完成驗證流程!!";
                    }
                }

                // 顯示註冊訊息
                TempData["MessageText"] = str_message;
                return RedirectToAction("MessageResult", "User", new { area = "" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"註冊錯誤: {ex.Message}");
                ModelState.AddModelError("", "註冊過程發生錯誤，請稍後再試。");
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous()]
        public async Task<IActionResult> RegisterValidate(string id)
        {
            try
            {
                using var user = new z_sqlUsers();
                var message = await Task.Run(() => user.RegisterConfirm(id));
                TempData["MessageText"] = message;
                return RedirectToAction("MessageResult", "User", new { area = "" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"驗證錯誤: {ex.Message}");
                TempData["MessageText"] = "驗證過程發生錯誤，請稍後再試。";
                return RedirectToAction("MessageResult", "User", new { area = "" });
            }
        }

        [HttpGet]
        [AllowAnonymous()]
        public IActionResult Forget()
        {
            ActionService.SetPrgInfo("admin", "後台管理系統");
            ActionService.SetActionName(enAction.Forget);
            ActionService.SetSubActionName("使用者");
            ActionService.SetActionCardSize(enCardSize.Medium);
            vmForget model = new vmForget();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Forget(vmForget model)
        {
            // 1.檢查輸入資料是否合格
            if (!ModelState.IsValid) return View(model);

            try
            {
                using var user = new z_sqlUsers();

                // 2.檢查帳號是否存在,存在則設定新的密碼也設定狀態為未審核
                string str_code = await Task.Run(() => user.Forget(model.UserNo));
                if (string.IsNullOrEmpty(str_code))
                {
                    ModelState.AddModelError("UserNo", "查無帳號或電子信箱資訊!!");
                    return View(model);
                }

                // 3.寄出忘記密碼驗證信
                using var sendEmail = new SendMailService();
                string str_message = await Task.Run(() => user.CheckMailValidateCode(str_code));

                if (string.IsNullOrEmpty(str_message))
                {
                    var userData = await Task.Run(() => user.GetValidateUser(str_code));
                    var mailObject = new MailObject
                    {
                        MailTime = DateTime.Now,
                        ValidateCode = str_code,
                        UserNo = userData.UserNo,
                        UserName = userData.UserName,
                        ToName = userData.UserName,
                        ToEmail = userData.ContactEmail,
                        Password = userData.Password,
                        ReturnUrl = $"{ActionService.HttpHost}/User/ForgetValidate/{str_code}"
                    };

                    str_message = await Task.Run(() => sendEmail.UserForget(mailObject));
                    if (string.IsNullOrEmpty(str_message))
                    {
                        str_message = "您重設密碼的要求已受理，請記得收信完成重設密碼的流程!!!";
                    }
                }

                // 顯示註冊訊息
                TempData["MessageText"] = str_message;
                return RedirectToAction("MessageResult", "User", new { area = "" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"忘記密碼錯誤: {ex.Message}");
                ModelState.AddModelError("", "處理過程發生錯誤，請稍後再試。");
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous()]
        public async Task<IActionResult> ForgetValidate(string id)
        {
            try
            {
                using var user = new z_sqlUsers();
                // 更新使用者狀態為已審核
                string str_message = await Task.Run(() => user.ForgetConfirm(id));
                // 顯示重設密碼訊息
                TempData["MessageText"] = str_message;
                return RedirectToAction("MessageResult", "User", new { area = "" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"密碼驗證錯誤: {ex.Message}");
                TempData["MessageText"] = "驗證過程發生錯誤，請稍後再試。";
                return RedirectToAction("MessageResult", "User", new { area = "" });
            }
        }

        [HttpGet]
        [Login()]
        public IActionResult ResetPassword()
        {
            ActionService.SetPrgInfo("admin", "後台管理系統");
            ActionService.SetActionName("重設密碼");
            ActionService.SetSubActionName("使用者");
            ActionService.SetActionCardSize(enCardSize.Small);
            vmResetPassword model = new vmResetPassword();
            return View(model);
        }

        [HttpPost]
        [Login()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(vmResetPassword model)
        {
            // 1.檢查輸入資料是否合格
            if (!ModelState.IsValid) return View(model);

            try
            {
                using var user = new z_sqlUsers();

                // 2.檢查帳號是否存在,存在則設定新的密碼也設定狀態為未審核
                string str_code = await Task.Run(() => user.ResetPasswordUpdate(model));
                if (string.IsNullOrEmpty(str_code))
                {
                    ModelState.AddModelError("OldPassword", "目前密碼不正確!!");
                    return View(model);
                }

                // 3.寄出忘記密碼驗證信
                using var sendEmail = new SendMailService();

                string str_message = await Task.Run(() => user.CheckMailValidateCode(str_code));
                if (string.IsNullOrEmpty(str_message))
                {
                    var userData = await Task.Run(() => user.GetValidateUser(str_code));
                    var mailObject = new MailObject
                    {
                        MailTime = DateTime.Now,
                        ValidateCode = str_code,
                        UserNo = userData.UserNo,
                        UserName = userData.UserName,
                        ToName = userData.UserName,
                        ToEmail = userData.ContactEmail,
                        Password = userData.Password,
                        ReturnUrl = $"{ActionService.HttpHost}/User/ResetPasswordValidate/{str_code}"
                    };

                    str_message = await Task.Run(() => sendEmail.UserResetPassword(mailObject));
                    if (string.IsNullOrEmpty(str_message))
                    {
                        str_message = "您重設密碼的要求已受理，請記得收信完成重設密碼的流程!!!";
                    }
                }
                else
                {
                    ModelState.AddModelError("", str_message);
                    return View(model);
                }

                // 3.登出使用者
                SessionService.IsLogin = false;
                SessionService.UserNo = "";
                SessionService.UserName = "";

                // 顯示註冊訊息
                TempData["MessageText"] = str_message;
                return RedirectToAction("MessageResult", "User", new { area = "" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"重設密碼錯誤: {ex.Message}");
                ModelState.AddModelError("", "處理過程發生錯誤，請稍後再試。");
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous()]
        public async Task<ActionResult> ResetPasswordValidate(string id)
        {
            try
            {
                using var user = new z_sqlUsers();
                // 更新使用者狀態為已審核
                string str_message = await Task.Run(() => user.ResetPasswordConfirm(id));
                // 顯示重設密碼訊息
                TempData["MessageText"] = str_message;
                return RedirectToAction("MessageResult", "User", new { area = "" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"密碼重設驗證錯誤: {ex.Message}");
                TempData["MessageText"] = "驗證過程發生錯誤，請稍後再試。";
                return RedirectToAction("MessageResult", "User", new { area = "" });
            }
        }

        [HttpGet]
        [AllowAnonymous()]
        public IActionResult MessageResult()
        {
            ViewBag.MessageText = (TempData["MessageText"] == null) ? "" : TempData["MessageText"].ToString();
            return View();
        }

        [HttpGet]
        [Login(RoleList = "Mis,User,Member")]
        public async Task<ActionResult> Profile()
        {
            try
            {
                ActionService.SetActionName("我的帳號");
                ActionService.SetSubActionName("使用者");
                ActionService.SetActionCardSize(enCardSize.Max);
                using var user = new z_sqlUsers();
                var model = await Task.Run(() => user.GetData(SessionService.UserNo));
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"載入個人資料錯誤: {ex.Message}");
                TempData["MessageText"] = "載入個人資料時發生錯誤。";
                return RedirectToAction("MessageResult");
            }
        }

        [HttpGet]
        [Login()]
        public ActionResult PhotoUpload()
        {
            ActionService.SetActionName("上傳照片");
            ActionService.SetSubActionName("我的帳號");
            ActionService.SetActionCardSize(enCardSize.Medium);
            return View();
        }

        [HttpPost]
        [Login()]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PhotoUpload(IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    string projectRoot = Directory.GetCurrentDirectory();
                    string webFolder = Path.Combine(projectRoot, "wwwroot", "images", "users");

                    // 檢查資料夾是否存在, 不存在則建立
                    if (!Directory.Exists(webFolder))
                    {
                        Directory.CreateDirectory(webFolder);
                    }

                    string fileName = $"{SessionService.UserNo}.jpg";
                    string filePath = Path.Combine(webFolder, fileName);

                    try
                    {
                        // 刪除已存在檔案
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        TempData["MessageText"] = $"刪除舊照片時發生錯誤: {ex.Message}";
                        return RedirectToAction("MessageResult", "User", new { area = "" });
                    }

                    // 使用非同步方式儲存檔案
                    using var stream = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(stream);
                }

                return RedirectToAction("Profile", "User", new { area = "" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"上傳照片錯誤: {ex.Message}");
                TempData["MessageText"] = "上傳照片時發生錯誤，請稍後再試。";
                return RedirectToAction("MessageResult", "User", new { area = "" });
            }
        }

        [HttpGet]
        [Login()]
        public async Task<ActionResult> EditProfile()
        {
            try
            {
                ActionService.SetActionName("修改個人資料");
                ActionService.SetSubActionName("我的帳號");
                ActionService.SetActionCardSize(enCardSize.Small);
                using var user = new z_sqlUsers();
                var model = await Task.Run(() =>
                    user.GetDataList()
                        .Where(m => m.UserNo == SessionService.UserNo)
                        .FirstOrDefault()
                );
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"載入編輯資料錯誤: {ex.Message}");
                TempData["MessageText"] = "載入資料時發生錯誤。";
                return RedirectToAction("MessageResult");
            }
        }

        [HttpPost]
        [Login()]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProfile(Users model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                using var user = new z_sqlUsers();
                await Task.Run(() => user.UpdateUserProfile(model));
                return RedirectToAction("Profile", "User", new { area = "" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"更新個人資料錯誤: {ex.Message}");
                ModelState.AddModelError("", "更新資料時發生錯誤，請稍後再試。");
                return View(model);
            }
        }
    }
}