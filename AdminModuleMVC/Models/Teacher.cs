using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseShared.Models
{
    public class Teacher
    {

        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id {  get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public virtual List<Course> Courses { get; set; }

        /*
         * 0 - Все права
         * 1 - Удаление курса
         * ...
         * x..x - Четотам
        */

        public virtual List<int> RightsTypes {  get; set; }

        public Teacher() { }



    }
}
