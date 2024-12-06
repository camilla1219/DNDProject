namespace WebAPI.Models
{
    public class Question
    {
        public int Id {get; set; }

        public required string Text {get; set; }

         public required string Type { get; set; }


        public List<Option> Options {get; set; } = new List<Option>();

    }
}