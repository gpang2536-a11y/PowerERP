/// <summary>
/// 登入用 ViewModel
/// </summary>
public class vmLogin
{
    [Display(Name = "登入帳號")]
    [Required(ErrorMessage = "{0}不可空白!!")]
    public string UserNo { get; set; } = "";
    [Display(Name = "登入密碼")]
    [Required(ErrorMessage = "{0}不可空白!!")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";
    [Display(Name = "驗證碼")]
    public string CaptchaCode { get; set; } = "";
}