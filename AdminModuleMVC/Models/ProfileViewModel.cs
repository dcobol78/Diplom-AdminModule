namespace CourseShared.Models
{
    public class ProfileViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string AvatarUrl { get; set; }

        public string Title { get; set; }
    }
}
