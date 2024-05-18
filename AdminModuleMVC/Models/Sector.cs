using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AdminModuleMVC.Models
{
    public class Sector: ICourseStage
    {
        public Sector() 
        { }

        public Sector(string idCourse, int number)
        {
            IdCourse = idCourse;
            Number = number;
            Name = $"Sector{number}";
            Themes = new();
            Content = string.Empty;
            Duration = 0;
            Exp = 0;
        }

        // Id Сектора
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        // Id курса
        public string IdCourse { get; set; }

        // Список тем
        public virtual List<Theme> Themes { get; set; }

        // Имя Сектора
        public string Name { get; set; }

        // Содержание сектора
        public string Content { get; set; }

        // Порядковый номер сектора
        public int Number { get; set; }

        // Продолжительность сектора ?
        public int? Duration { get; set; }

        public string? HomeworkId { get; set; }

        [ForeignKey("HomeworkId")]
        public virtual Homework Homework { get; set; }

        // Список файлов сектора
        public List<CourseFile> SectionFiles {  get; set; }

        //Опыт получаемый за прохождение
        public int Exp { get; set;}

        public string? TestId { get; set; }

        [ForeignKey("TestId")]
        public Test Test { get; set; }

    }
}
