using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AdminModuleMVC.Models
{
    public class Section
    {

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
        public string Number { get; set; }

        // Продолжительность сектора ?
        public int Duration { get; set; }

        // Опыт получаемый за прохождение курса
        public int Cost { get; set; }

        // Есть ли тест (Поменять на ссылку на тест?)
        public bool HasTest { get; set; }

        // Есть ли домашнее задание (Поменять на ссылку на задание?)
        public bool HasHomework { get; set; }
    }
}
