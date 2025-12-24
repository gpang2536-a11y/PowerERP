using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class SecurityAttribute : ActionFilterAttribute
{
    /// <summary>
    /// 權限模式
    /// </summary>
    public enSecurityMode Mode { get; set; } = enSecurityMode.Allow;

    /// <summary>
    /// 覆寫驗證程式
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // 不檢查權限
        if (Mode == enSecurityMode.Allow) return;

        // 檢查是否為除錯模式 (除錯模式不管權限問題)
        if (AppService.DebugMode) return;

        //檢查權限
        if (SecurityService.HasPermission(Mode)) return;

        SessionService.ErrorMessage = "您沒有權限執行此項作業，請洽系統管理員。";

        //權限檢查程式失敗
        context.Result = new RedirectToRouteResult(
            new RouteValueDictionary
            {
                    { "controller", "Home" },
                    { "action", "Index" },
                    { "area" , "Admin"}
            });
    }
}
