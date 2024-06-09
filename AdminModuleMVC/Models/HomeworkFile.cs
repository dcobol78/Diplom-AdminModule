using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseShared.Models
{
    public class HomeworkFile
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Path { get; set; }

        public string? AutorId { get; set; }

        [ForeignKey("AutorId")]
        public Student? Author { get; set; }

        public Homework? Homework { get; set; }

    }
}
