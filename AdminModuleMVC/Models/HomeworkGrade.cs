using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseShared.Models
{
    public class HomeworkGrade
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public float Grade {  get; set; }

        public Homework Homework { get; set; }

        public UserFile UserFile { get; set; }

        public int MaxGrade {  get; set; }
    }
}
