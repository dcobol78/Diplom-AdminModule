using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AdminModuleMVC.Models
{
    public class Homework
    {
        public Homework()
        { }

        public Homework(string parentId)
        {
            Name = "Homework";
            ParentId = parentId;
            Duration = 0;
            Description = "";
        }

        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string ParentId { get; set; }

        public int Duration { get; set; }

        public string Description { get; set; }

        public string? HomeworkFileId { get; set; }


        // Файлы необходимые для выполнения ДЗ
        [ForeignKey("HomeworkFileId")]
        public virtual CourseFile HomeWorkFile { get; set; }

        //public int Cost {  get; set; }

        //public DateTime StartDate { get; set; }

        //public DateTime EndDate { get; set; }
    }
}
