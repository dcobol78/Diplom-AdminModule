using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseShared.Models
{
    public class Notification
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? ReciverEmail { get; set; }

        public bool Seen { get; set; } = false;

        public string? UserId { get; set; }

        public DateTime CreationTime { get; set; }

        public Notification() 
        {
            CreationTime = DateTime.Now;
        }
    }
}
