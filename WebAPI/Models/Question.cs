using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DNDProject.Models
{
    public class Question
    {
        public int Id { get; set; }

        public int QuestionNo { get; set; }

        public string QuestionTitle { get; set; }

        public string QuestionText { get; set; }

        public int QuestionPriority { get; set; }
    }
}
