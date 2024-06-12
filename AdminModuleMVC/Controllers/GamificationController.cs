using AdminModuleMVC.Data;
using CourseShared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Html;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminModuleMVC.Controllers
{
    public class GamificationController : Controller
    {
        private readonly CourseDbContext _context;

        public GamificationController(CourseDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string id)
        {
            var achivements = await _context.Achivements.Include(a => a.Course).Include(a => a.Reward).Where(a => a.CourseId == id).ToListAsync();
            return View(new AchivementsViewModel { Achivements = achivements, courseId = id });
        }

        public IActionResult Create(string id)
        {
            return View(new AchivementViewModel { CourseId = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AchivementViewModel viewModel)
        {
            if (viewModel != null)
            {
                var achivement = new Achivement
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    ExpThreshold = viewModel.ExpThreshold,
                    CourseId = viewModel.CourseId
                };

                if (viewModel.Image != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await viewModel.Image.CopyToAsync(memoryStream);
                        achivement.Image = new CourseShared.Models.Image
                        {
                            ImageTitle = viewModel.Image.FileName,
                            MimeType = viewModel.Image.ContentType,
                            ImageData = memoryStream.ToArray()
                        };
                    }
                }

                if (viewModel.RewardName != null)
                {
                    achivement.Reward = new Item
                    {
                        Name = viewModel.RewardName,
                        Description = viewModel.RewardDescription,
                        Type = viewModel.RewardType
                    };
                }

                var course = await _context.Courses.Include(c => c.Achivements).FirstOrDefaultAsync(c => c.Id == viewModel.CourseId);

                course.Achivements.Add(achivement);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { id = viewModel.CourseId});
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achivement = await _context.Achivements.Include(a => a.Reward).Include(a => a.Image).FirstOrDefaultAsync(a => a.Id == id);
            if (achivement == null)
            {
                return NotFound();
            }

            var viewModel = new AchivementViewModel
            {
                Id = achivement.Id,
                Name = achivement.Name,
                Description = achivement.Description,
                ExpThreshold = achivement.ExpThreshold,
                CourseId = achivement.CourseId,
                RewardName = achivement.Reward?.Name,
                RewardDescription = achivement.Reward?.Description,
                RewardType = achivement.Reward?.Type,
                ExistingImageUrl = achivement.Image?.GetImageDataUrl()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, AchivementViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (viewModel != null)
            {
                try
                {
                    var achivement = await _context.Achivements.Include(a => a.Reward).Include(a => a.Image).FirstOrDefaultAsync(a => a.Id == id);
                    if (achivement == null)
                    {
                        return NotFound();
                    }

                    achivement.Name = viewModel.Name;
                    achivement.Description = viewModel.Description;
                    achivement.ExpThreshold = viewModel.ExpThreshold;
                    achivement.CourseId = viewModel.CourseId;

                    if (viewModel.Image != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await viewModel.Image.CopyToAsync(memoryStream);
                            achivement.Image = new CourseShared.Models.Image
                            {
                                ImageTitle = viewModel.Image.FileName,
                                MimeType = viewModel.Image.ContentType,
                                ImageData = memoryStream.ToArray()
                            };
                        }
                    }

                    if (viewModel.RewardName != null)
                    {
                        if (achivement.Reward == null)
                        {
                            achivement.Reward = new Item();
                        }
                        achivement.Reward.Name = viewModel.RewardName;
                        achivement.Reward.Description = viewModel.RewardDescription;
                        achivement.Reward.Type = viewModel.RewardType;
                    }

                    _context.Update(achivement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AchivementExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = viewModel.CourseId });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achivement = await _context.Achivements.Include(a => a.Course).Include(a => a.Reward).FirstOrDefaultAsync(a => a.Id == id);
            if (achivement == null)
            {
                return NotFound();
            }

            return View(achivement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var achivement = await _context.Achivements.
                Include(a => a.Reward).
                    ThenInclude(r => r.Image).
                Include(a => a.Image).
                FirstOrDefaultAsync(a => a.Id == id);
            var courseid = achivement.CourseId;
            _context.Achivements.Remove(achivement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = courseid });
        }

        private bool AchivementExists(string id)
        {
            return _context.Achivements.Any(e => e.Id == id);
        }
    }
}
