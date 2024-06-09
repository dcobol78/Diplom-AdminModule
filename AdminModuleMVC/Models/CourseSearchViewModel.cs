namespace CourseShared.Models
{
    public class CourseSearchViewModel
    {
        public string SearchTerm { get; set; }
        public List<CourseDetailsViewModel> Courses { get; set; }
        public CourseFilter Filter { get; set; }
    }

    public class CourseDetailsViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AutorName { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsPublic { get; set; }
        public Image CourseImage { get; set; }
    }

    public enum CourseFilter
    {
        All,
        Upcoming,
        Ongoing,
        Ended
    }
}
