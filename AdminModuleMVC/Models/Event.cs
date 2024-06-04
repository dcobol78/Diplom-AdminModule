using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseShared.Models
{
    public enum EventType
    {
        Lecture,
        Offline,
        Other
    }

    public class Event
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public int IdCourse {  get; set; }

        public string Name { get; set; }

        public DateTime StartTime { get; set; } = DateTime.Now.Date;

        public EventType Type { get; set; }

        public string Content { get; set; }

        public List<Student> Attendance {  get; set; }
    }
}
