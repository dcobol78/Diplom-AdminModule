using AdminModuleMVC.Data;
using CourseShared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace AdminModuleMVC.Views.Shared.Components.TeacherDetails
{

    public class TeacherDetailsViewComponent : ViewComponent
    {
        private readonly CourseDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public TeacherDetailsViewComponent(CourseDbContext context, UserManager<IdentityUser> userManager)
        {
            _dbContext = context;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke(string id)
        {

            var teacher = _dbContext.Teachers.
                Include(t => t.Avatar).
                FirstOrDefault(t => t.Id == id);

            var viewModel = new TeacherDetailsViewModel
            {
                Id = teacher.Id,
                Name = teacher.Name,
                Surname = teacher.Surname,
                Patronymic = teacher.Patronymic,
                DOB = teacher.DOB,
                Email = teacher.Email,
                UserId = teacher.UserId,
                AvatarUrl = teacher.Avatar?.GetImageDataUrl()
            };

            return View("PartialTeacherDetails", viewModel);
        }
    }
}
