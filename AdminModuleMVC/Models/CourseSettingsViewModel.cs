using CourseShared.Models;

namespace CourseShared.Models
{
    public class CourseSettingsViewModel
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public List<TeacherPermission> TeacherPermissions { get; set; }
    }
}
