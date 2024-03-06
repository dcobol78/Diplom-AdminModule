namespace AdminModuleMVC.Models
{
    public class Sector
    {
        // Id Сектора
        public int Id { get; set; }

        // Имя Сектора
        public string Name { get; set; }

        // Содержание сектора
        public string Content { get; set; }

        // Порядковый номер сектора ?
        public string Number { get; set; }

        // Следующий сектор
        public int NextSectorId { get; set; }

        // Следующий сектор
        public int FirstThemeId { get; set; }

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
