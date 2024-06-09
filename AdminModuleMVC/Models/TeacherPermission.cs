using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseShared.Models
{
    public class TeacherPermission
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }
        public bool CanEditCourse { get; set; }
        public bool CanAddStudents { get; set; }
        public bool CanRemoveStudents { get; set; }
        public bool CanManageContent { get; set; }
        public bool CanViewGrades { get; set; }
        public bool CanEditGrades { get; set; }
    }
}
