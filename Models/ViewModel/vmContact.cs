public class vmContact
{
    [Display(Name = "連絡人姓名")]
    [Required(ErrorMessage = "{0}不可空白!!")]
    public string ContactorName { get; set; } = "";
    [Display(Name = "連絡人Email")]
    [Required(ErrorMessage = "{0}不可空白!!")]
    [EmailAddress(ErrorMessage = "EmailAddressErrorMessage")]
    public string ContactorEmail { get; set; } = "";
    [Display(Name = "主旨")]
    [Required(ErrorMessage = "{0}不可空白!!")]
    public string ContactorSubject { get; set; } = "";
    [Display(Name = "內容")]
    [Required(ErrorMessage = "{0}不可空白!!")]
    public string ContactorMessage { get; set; } = "";
}