using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DNDProject.Models;
using System;
using System.Linq;

namespace DNDProject.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DNDProjectContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DNDProjectContext>>()))
            {
                // Check if any questions already exist
                if (context.Question.Any())
                {
                    return;   // Database has been seeded, no need to add more
                }

                // If no data exists, add these seed questions
                context.Question.AddRange(
                    new Question
                    {
                         QuestionNo = 1,
                         QuestionTitle = "Programming Languages",
                         QuestionText = "What is your favorite programming language?",
                         QuestionPriority = 1
                    },
                    new Question
                    {
                        QuestionNo = 2,
                        QuestionTitle = "Database Preferences",
                        QuestionText = "Do you prefer SQL or NoSQL databases?",
                        QuestionPriority = 2
                    }
                );

                // Save changes to the SQL database
                context.SaveChanges();
            }
        }
    }
}
