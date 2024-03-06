namespace AdminModuleMVC.Models
{
    public class Theme
    {
        // Id темы
        public int Id { get; set; }

        // Имя темы
        public string Name { get; set; }

        // Содержание темы
        public string Content { get; set; }

        // Порядковый номер темы ?
        public string Number { get; set; }

        // Следующая тема
        public int NextThemeId { get; set; }

        // Id курса
        public int CourseId { get; set; }

        // Продолжительность сектора ?
        public int Duration { get; set; }

        // Дата начала секции ?
        public DateTime StartingDate { get; set; }

        // Опыт получаемый за прохождение курса
        //public int Cost { get; set; }

        // Id теста сектора
        //public int TestId { get; set; }

        // Id задания 
        //public int AssignmentId { get; set; }

        // Загруженные файлы
        //public string FilesId { get; set; }
    }
}
