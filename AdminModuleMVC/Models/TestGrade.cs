using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseShared.Models
{
    public class TestGrade
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }

        public TestSession? TestSession { get; set; }

        public Test? Test { get; set; }

        public float? Grade {  get; set; }

        public int? MaxGrade { get; set; }
    }
}
