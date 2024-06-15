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
        public bool CanEditCourse { get; set; } = false;
        public bool CanAddStudents { get; set; } = false;
        public bool CanRemoveStudents { get; set; } = false;
        public bool CanManageContent { get; set; } = false;
        public bool CanViewGrades { get; set; } = false;
        public bool CanEditGrades { get; set; } = false;
    }
}
