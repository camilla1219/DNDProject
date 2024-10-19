using System;
using System.ComponentModel.DataAnnotations;

namespace DNDProject.Models
{
    public class Response
    {
        public int Id { get; set; }  

        [Required]
        public int QuestionId { get; set; }  

        [Required]
        public int SurveyId { get; set; }  

        public string Answer { get; set; }  

        public DateTime ResponseDate { get; set; }  
        
        public Question Question { get; set; } 
        
        public Survey Survey { get; set; }
        
    }
}
