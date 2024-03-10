using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AdminModuleMVC.Models
{
    public class Course
    {
        public Course(string id, string autorId) 
        {
            Id = id;
            AutorId = autorId;
            Name = "My Course";
            Description = string.Empty;
            AutorName = string.Empty;
            Duration = 0;
            CreationDate = DateTime.Now;
            HasTest = false;
            HasHomework = false;
            IsCoherent = false;
            IsPublic = false;
            Section = new();
            StartingDate = new();
        }

        public Course()
        { }

        // Id курса
        [ScaffoldColumn(false)]
        [Key]
        public string Id { get; set; }

        // Id автора
        public string AutorId { get; set; }

        // Имя курса
        public string Name { get; set; }

        // Описание курса
        public string Description { get; set; }

        // Список секторов
        public virtual List<Section> Section { get; set; }

        // Список тэгов (Доработать позже)
        //public virtual List<Tag> Tags { get; set; }

        // Список дат начала занятий ???
        public virtual List<DateTime> StartingDate { get; set; }

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

        // Есть ли тест (Поменять на ссылку на тест?)
        public bool HasTest { get; set; }

        // Есть ли домашнее задание (Поменять на ссылку на задание?)
        public bool HasHomework {  get; set; }

    }
}
