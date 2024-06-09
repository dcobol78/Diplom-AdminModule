using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseShared.Models
{
    public class CourseChat
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public virtual List<Message> Messages { get; set; }

        public virtual List<Student> Students { get; set; }
        public virtual List<Teacher> Teachers { get; set; }

        public string CourseName { get; set; }
        public string CourseId { get; set; }

        public DateTime? LastInteraction { get; set; }

    }
}
