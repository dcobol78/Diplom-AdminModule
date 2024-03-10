using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AdminModuleMVC.Models
{
    public class Theme
    {
        // Id Темы
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        // Id Сектора
        public string IdSection { get; set; }

        // Имя Темы
        public string Name { get; set; }

        // Содержание Темы
        public string Content { get; set; }

        // Порядковый номер Темы
        public string Number { get; set; }

        // Продолжительность Темы
        public int Duration { get; set; }

        // Опыт получаемый за прохождение Темы ?
        public int Cost { get; set; }

        // Есть ли тест (Поменять на ссылку на тест?)
        public bool HasTest { get; set; }

        // Есть ли домашнее задание (Поменять на ссылку на задание?)
        public bool HasHomework { get; set; }
    }
}
