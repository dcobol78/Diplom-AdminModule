namespace AdminModuleMVC.Models
{
    public class Theme
    {
        // Id Темы
        public int Id { get; set; }

        // Id Сектора
        public int IdSection { get; set; }

        // Имя Темы
        public string Name { get; set; }

        // Id теста Темы
        public int IdTest { get; set; }

        // Содержание Темы
        public string Content { get; set; }

        // Порядковый номер Темы
        public string Number { get; set; }

        // Продолжительность Темы
        public int Duration { get; set; }

        // Опыт получаемый за прохождение Темы ?
        public int Cost { get; set; }

        // Id задания 
        public int IdHomework { get; set; }
    }
}
