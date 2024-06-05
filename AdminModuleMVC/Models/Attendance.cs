using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CourseShared.Models;

namespace diplomV1.Models
{
    public class Attendance
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public virtual List<Student> Students { get; set; }

        public virtual List<Event> Events {  get; set; }
    }
}
