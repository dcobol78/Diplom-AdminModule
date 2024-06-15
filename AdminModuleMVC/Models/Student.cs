using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace CourseShared.Models
{

    public class Student
    {

        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Patronymic { get; set; }

        public DateOnly? DOB { get; set; }

        public string? Email { get; set; }

        public string? Title { get; set; }

        public Image? Avatar { get; set; }

        public virtual List<Course> Courses { get; set; }

        public virtual List<HomeworkGrade> HomeworkGrades { get; set; }

        public virtual List<TestGrade> TestGrades { get; set; }

        public virtual List<Achivement> Achivements { get; set; }

        public virtual List<Item> Inventory { get; set; }

        public virtual List<HomeworkFile> HomeworkFiles { get; set; }

        public virtual List<Notification> Notifications { get; set; }

        public virtual List<CourseStudentExp> CourseStudentExp { get; set; }

        public virtual List<UserChat> UserChats { get; set; }

        public virtual List<CourseChat> CourseChats { get; set; }

        public virtual List<TestInstance> TestInstances { get; set; }

        public string? UserId { get; set; }

        public Student() { }
    }

    public class StudentDetailsViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateOnly? DOB { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string AvatarUrl { get; set; }
    }
}
