namespace CourseShared.Models
{
    public class TeacherPermissionViewModel
    {
        public string TeacherId { get; set; }
        public string TeacherName { get; set; }
        public bool CanEditCourse { get; set; }
        public bool CanAddStudents { get; set; }
        public bool CanRemoveStudents { get; set; }
        public bool CanManageContent { get; set; }
        public bool CanViewGrades { get; set; }
        public bool CanEditGrades { get; set; }
    }

}
