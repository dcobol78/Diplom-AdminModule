using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AdminModuleMVC.Models
{

    public class Student
    {

        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Title { get; set; }

        public virtual List<Course> Courses { get; set; }

        public virtual List<HomeworkGrade> HomeworkGrades { get; set; }

        public virtual List<TestGrade> TestGrades { get; set; }

        public virtual List<Event> WasEvents { get; set; }

        //public List<Achivement> Achivements { get; set; }

        //public List<Items> Inventory { get; set; }

        public string UserId { get; set; }

        public Student() { }
    }
}
