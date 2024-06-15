using AdminModuleMVC.Data;
using CourseShared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace AdminModuleMVC.Views.Shared.Components.TeacherDetails
{

    public class StudentDetailsViewComponent : ViewComponent
    {
        private readonly CourseDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public StudentDetailsViewComponent(CourseDbContext context, UserManager<IdentityUser> userManager)
        {
            _dbContext = context;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke(string id)
        {

            var student = _dbContext.Students.
                Include(t => t.Avatar).
                FirstOrDefault(t => t.Id == id);

            var viewModel = new TeacherDetailsViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Surname = student.Surname,
                Patronymic = student.Patronymic,
                DOB = student.DOB,
                Email = student.Email,
                UserId = student.UserId,
                AvatarUrl = student.Avatar?.GetImageDataUrl() ?? "/files/images/default-avatar.png"
            };

            return View("PartialTeacherDetails", viewModel);
        }
    }
}
