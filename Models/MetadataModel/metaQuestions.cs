namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaQuestions))]
    public partial class Questions
    {
        [NotMapped]
        [Display(Name = "問題(部份內容)")]
        public string? ShortQuestionText
        {
            get
            {
                if (QuestionText != null && QuestionText.Length > 20)
                {
                    return QuestionText.Substring(0, 20) + "...";
                }
                else
                {
                    return QuestionText;
                }
            }
        }

        [NotMapped]
        [Display(Name = "答案(部份內容)")]
        public string? ShortAnswerText
        {
            get
            {
                if (AnswerText != null && AnswerText.Length > 20)
                {
                    return AnswerText.Substring(0, 20) + "...";
                }
                else
                {
                    return AnswerText;
                }
            }
        }
    }
}

public class z_metaQuestions
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "排序")]
    public string? SortNo { get; set; }
    [Display(Name = "問題")]
    public string? QuestionText { get; set; }
    [Display(Name = "答案")]
    public string? AnswerText { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}
