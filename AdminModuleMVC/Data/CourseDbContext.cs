using CourseShared.Models;
using diplomV1.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminModuleMVC.Data
{
    public class CourseDbContext : DbContext
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Sector> Sections { get; set; }

        public DbSet<Theme> Themes { get; set; }

        public DbSet<CourseFile> CourseFiles { get; set; }

        public DbSet<Homework> Homeworks { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Event> Events { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<UserFile> UserFiles { get; set; }

        public DbSet<CourseCode> CourseCodes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ChatInstance> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Achivement> Achivements { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<HomeworkGrade> HomeworkGrades { get; set; }
        public DbSet<TestGrade> TestGrades { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<TeacherRights> TeacherRights { get; set; }
        public DbSet<TestSession> TestSessions { get; set; }

    }
}
