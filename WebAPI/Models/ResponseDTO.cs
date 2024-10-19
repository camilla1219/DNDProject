namespace DNDProject.Models

{
    public class ResponseDTO
    {
        public long Id { get; set; }
        public string Answer { get; set; } = string.Empty;
        public long QuestionId { get; set; }
        public long SurveyId { get; set; }  
        public DateTime ResponseDate { get; set; }  
    }
}
