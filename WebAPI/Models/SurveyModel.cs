namespace WebAPI.models
{
    public class Survey
    {
        public int Id {get; set; }
        public required string Title {get; set; }

        public required string Description {get; set; }

        public List<Question> Questions {get; set; } = new List<Question>();

    }
}