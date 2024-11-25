namespace WebAPI.models
{
    public class SurveyResponse
    {
        public int Id {get; set; }
        public int SurveyId {get; set; }

        public required string RespondentName {get; set; }

        public DateTime SubmittedAt {get; set; }

        public List<ResponseAnswer> Answers {get; set; } = new List<ResponseAnswer>();

    }

    public class ResponseAnswer
    {
        public int QuestionId {get; set; }
        public required string SelectedOption {get; set; }
    }
}