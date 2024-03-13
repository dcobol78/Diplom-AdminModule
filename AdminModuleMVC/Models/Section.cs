using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AdminModuleMVC.Models
{
    public class Section
    {
        public Section() 
        { }

        public Section(string idCourse, int number)
        {
            IdCourse = idCourse;
            Number = number;
            Name = $"Sector{number}";
            Themes = new();
            Content = string.Empty;
            Duration = 0;
            Cost = 0;
        }
        public Section(string idCourse)
        {
            IdCourse = idCourse;
            Number = 0;
            Name = $"Sector{Number}";
            Themes = new();
            Content = string.Empty;
            Duration = 0;
            Cost = 0;
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
        public int Duration { get; set; }

        // Опыт получаемый за прохождение курса
        public int Cost { get; set; }

        [AllowNull]
        public Homework Homework { get; set; }

        [AllowNull]
        public Test Test { get; set; }

        // Список файлов сектора
        public List<CourseFile> SectionFiles {  get; set; }
    }
}
