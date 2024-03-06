namespace AdminModuleMVC.Models
{
    public class Event
    {
        public int Id { get; set; }

        public int IdCourse {  get; set; }

        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public string Type { get; set; }

        public string Content { get; set; }
    }
}
