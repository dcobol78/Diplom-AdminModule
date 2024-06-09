using CourseShared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseShared.Models
{
    public class TestInstance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string TestId { get; set; }

        [ForeignKey("TestId")]
        public Test Test { get; set; }

        [Required]
        public string StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public virtual List<TestAnswer> Answers { get; set; }

        public TestInstance()
        {
            Answers = new List<TestAnswer>();
        }
    }
}