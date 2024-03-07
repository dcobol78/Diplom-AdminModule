using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AdminModuleMVC.Models
{
    public class Course
    {
        // Id курса
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Id автора
        public int AutorId { get; set; }

        // Имя курса
        public string Name { get; set; }

        // Описание курса
        public string Description { get; set; }

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

        // Список дат начала занятий (Не заносить в бд)?
        public DateTime StartingDate { get; set; }

    }
}
