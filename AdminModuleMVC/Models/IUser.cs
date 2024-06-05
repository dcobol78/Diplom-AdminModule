using Microsoft.AspNetCore.Identity;

namespace CourseShared.Models
{
    public interface IUser
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Lastname { get; set; }

        public string? Patronymic { get; set; }

        public List<Course>? Courses { get; set; }
    }
}
