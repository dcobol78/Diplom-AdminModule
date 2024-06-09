using AdminModuleMVC.Data;
using CourseShared.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminModuleMVC.Views.Shared.Components.GenerateCode
{
    public class GenerateCodeViewComponent : ViewComponent
    {
        private readonly CourseDbContext _context;

        public GenerateCodeViewComponent(CourseDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string courseId)
        {
            if (string.IsNullOrEmpty(courseId))
            {
                return View("Error", "Course ID is required");
            }

            var course = await _context.Courses.FindAsync(courseId);
            if (course == null)
            {
                return View("Error", "Course not found");
            }

            var courseCode = new CourseCode
            {
                Course = course,
                UsesLeft = 10, // Set default uses left
                Code = GenerateUniqueCode()
            };

            _context.CourseCodes.Add(courseCode);
            await _context.SaveChangesAsync();

            var viewModel = new GenerateCodeViewModel
            {
                CourseId = course.Id,
                UsesLeft = courseCode.UsesLeft,
                Code = courseCode.Code
            };

            return View("GenerateCode", viewModel);
        }

        private string GenerateUniqueCode()
        {
            return Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }
    }
}
