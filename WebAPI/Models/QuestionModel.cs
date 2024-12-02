namespace WebAPI.models
{
    public class Question
    {
        public int Id {get; set; }

        public required string Text {get; set; }

         public required string Type { get; set; }


        public List<string> Options {get; set; } = new List<string>();

    }
}