namespace DNDProject.Models

{
    public class ResponseDTO
    {
        public int Id { get; set; }
        public string Answer { get; set; } = string.Empty;
        public int QuestionId { get; set; }
        public int SurveyId { get; set; }  
        public DateTime ResponseDate { get; set; }  
    }
}
