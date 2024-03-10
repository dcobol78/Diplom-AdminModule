namespace AdminModuleMVC.Models
{
    public class EditCourseViewModel
    {
        public Course Course { get; set; }

        //Не понадобилось (Возможно заменить на эллемент модели)
        //public List<Section> Sections { get; set; }

        //Скорее всего не понадобится 
        //public List<Tag> Tags { get; set; }

        public Test CourseTest { get; set; }

        public Homework CourseHomework { get; set; }

        public List<FormFile> Files {  get; set; }

        public List<Event> Events { get; set; }

        public List<StartingDate> StartDates { get; set; }
    }
}
