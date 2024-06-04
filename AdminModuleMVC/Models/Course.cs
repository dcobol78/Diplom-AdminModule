using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CourseShared.Models
{
    public class Course: ICourseStage
    {
        public Course(string autorId) 
        {
            AutorId = autorId;
            Name = "My Course";
            Description = string.Empty;
            AutorName = string.Empty;
            Duration = 0;
            CreationDate = DateTime.Now;
            IsCoherent = false;
            IsPublic = false;
            Sections = new();
            Exp = 0;
            StartingDate = DateTime.Now.Date;
        }

        public Course()
        { }

        // Id курса
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        // Id автора
        public string AutorId { get; set; }

        // Имя курса
        public string Name { get; set; }

        // Описание курса
        public string Description { get; set; }

        // Список секторов
        public virtual List<Sector> Sections { get; set; }

        // Имя автора
        public string AutorName { get; set; }

        // Продолжительность курса
        public int Duration { get; set; }

        // Дата создания курса
        public DateTime CreationDate { get; set;}

        // Является ли курс публичным (Свободный вход, отоброжается в списке всех курсов)
        public bool IsPublic { get; set; }

        // Является ли курс последовательным (Свободный доступ ко всему контенту или последовательный)
        public bool IsCoherent { get; set; }

        public string? HomeworkId { get; set; }

        [ForeignKey("HomeworkId")]
        public virtual Homework Homework { get; set; }

        // Приложенные к курсу файлы
        public virtual List<CourseFile> CourseFiles { get; set; }

        public string? TestId { get; set; }

        [ForeignKey("TestId")]
        public Test Test { get; set; }

        //Опыт получаемый за прохождение курса
        public int Exp { get; set;} 

        // Список дат начала занятий ???
        public virtual DateTime StartingDate { get; set; }

        // Список событий
        public virtual List<Event> Events { get; set; }

        public virtual List<Student> Students { get; set; }
        public virtual List<Teacher> Teachers { get; set; }
        public virtual List<Message> Messages { get; set; }
    }
}
