using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseShared.Models
{
    public class Homework
    {
        public Homework()
        { }

        public Homework(string parentId, string parentType)
        {
            Name = "Homework";
            ParentId = parentId;
            Duration = 0;
            Description = "";
            ParentType = parentType;
        }

        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string? Name { get; set; }

        public string? ParentId { get; set; }

        // Продолжительность в днях
        public int? Duration { get; set; }

        public string? Description { get; set; }

        [NotMapped]
        public IFormFile FormFile { get; set; }

        public string? HomeworkFileId { get; set; }

        // Файлы необходимые для выполнения ДЗ
        [ForeignKey("HomeworkFileId")]
        public virtual CourseFile HomeworkFile { get; set; }

        // Стоимость домашнего задания
        public int? Cost {  get; set; }

        //Опыт получаемый за прохождение курса
        public float? Exp { get; set;} 

        public string? ParentType { get; set; }

        // Разрешать загружать даже после срока
        public bool? AllowLateUpload { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
