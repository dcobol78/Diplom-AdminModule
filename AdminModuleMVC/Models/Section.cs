namespace AdminModuleMVC.Models
{
    public class Section
    {
        // Id Сектора
        public int Id { get; set; }

        // Id курса
        public int IdCourse { get; set; }

        // Имя Сектора
        public string Name { get; set; }

        // Id теста сектора
        public int IdTest { get; set; }

        // Содержание сектора
        public string Content { get; set; }

        // Порядковый номер сектора ?
        public string Number { get; set; }

        // Продолжительность сектора ?
        public int Duration { get; set; }

        // Опыт получаемый за прохождение курса
        public int Cost { get; set; }

        // Id задания 
        public int IdHomework { get; set; }

    }
}
