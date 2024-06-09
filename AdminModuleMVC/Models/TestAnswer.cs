using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseShared.Models
{
    public class TestAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string TestInstanceId { get; set; }

        [ForeignKey("TestInstanceId")]
        public TestInstance TestInstance { get; set; }

        [Required]
        public string QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public Question Question { get; set; }

        [Required]
        public string AnswerId { get; set; }

        [ForeignKey("AnswerId")]
        public Answer Answer { get; set; }
    }
}