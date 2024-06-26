﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseShared.Models
{
    public class Teacher
    {

        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Patronymic { get; set; }

        public DateOnly? DOB { get; set; }

        public string? Email { get; set; }

        public string? UserId { get; set; }

        public Image? Avatar { get; set; }

        public virtual List<UserChat> UserChats { get; set; }

        public virtual List<TeacherPermission> TeacherPermissions { get; set; }

        public virtual List<CourseChat> CourseChats { get; set; }

        public virtual List<Notification> Notifications { get; set; }

        public virtual List<Course> Courses { get; set; }

        public Teacher() { }

    }

    public class TeacherDetailsViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateOnly? DOB { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string AvatarUrl { get; set; }
    }
}
