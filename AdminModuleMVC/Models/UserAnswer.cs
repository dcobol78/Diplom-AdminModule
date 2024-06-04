using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseShared.Models
{
    public class UserAnswer
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public bool IsCorrect { get; set; } = false;

        public string Content { get; set; } = string.Empty;

        public int? Number {  get; set; }
    }
}
