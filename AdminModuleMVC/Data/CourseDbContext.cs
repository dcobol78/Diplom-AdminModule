using AdminModuleMVC.Models;
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
        public DbSet<Section> Sections { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<CourseFile> CourseFiles { get; set;}
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Test> Tests { get; set; }

    }
}
