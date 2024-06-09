using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseShared.Models
{
    public class UserChat
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public virtual List<Message> Messages { get; set; }

        public string? FirstUserName { get; set; }
        public string? FirstUserId { get; set; }

        public string? SecondUserName { get; set; }
        public string? SecondUserId { get; set; }

        public DateTime? LastInteraction { get; set; }

    }
}
