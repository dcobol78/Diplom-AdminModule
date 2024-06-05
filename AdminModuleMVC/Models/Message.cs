using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseShared.Models
{
    public class Message
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string? Content { get; set; }

        public ChatInstance? ChatInstance { get; set; }

        public DateTime? CreationTime {  get; set; }

        public Student? StudentAutor { get; set; }

        public Teacher? TeacherAutor { get; set; }
    }
}
