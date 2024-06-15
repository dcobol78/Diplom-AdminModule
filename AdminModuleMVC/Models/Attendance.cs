using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CourseShared.Models;

namespace CourseShared.Models
{
    public class Attendance
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public virtual Student Student { get; set; }

        public virtual Event Event {  get; set; }

        public bool Was { get; set; }
    }
}
