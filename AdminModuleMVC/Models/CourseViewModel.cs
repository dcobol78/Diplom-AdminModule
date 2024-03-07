namespace AdminModuleMVC.Models
{
    public class CourseViewModel
    {
        public Course Course { get; set; }

        public List<Section> Sections { get; set; }

        public List<Tag> Tags { get; set; }

        public Test CourseTest { get; set; }

        public Homework CourseHomework { get; set; }

        public List<FormFile> Files {  get; set; }

        public List<Event> Events { get; set; }

        public List<StartingDate> StartDates { get; set; }
    }
}
