namespace CourseShared.Models
{
    public class QuestionViewModel
    {
        public string TestInstanceId { get; set; }
        public int QuestionNumber { get; set; }
        public string QuestionContent { get; set; }
        public List<AnswerViewModel> Answers { get; set; }
    }

    public class AnswerViewModel
    {
        public string AnswerId { get; set; }
        public string Content { get; set; }
    }

    public class SubmitAnswerViewModel
    {
        public string TestInstanceId { get; set; }
        public string QuestionId { get; set; }
        public string SelectedAnswerId { get; set; }
        public int QuestionNumber { get; set; }
    }
}
