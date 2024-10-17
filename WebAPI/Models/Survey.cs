using Microsoft.EntityFrameworkCore;

namespace DNDProject.Models
{
    public class Survey
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public string? Description { get; set; }

        public string? CoverImageUrl { get; set; }

        public List<Question> Questions { get; set; } = new List<Question>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ExpirationDate { get; set; }

        public string? CreatorId { get; set; }

        public bool IsAnonymous { get; set; }
    }
}