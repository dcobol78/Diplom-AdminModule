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

namespace AdminModuleMVC.Controllers
{
    public class GamificationController : Controller
    {
        private readonly CourseDbContext _context;

        public GamificationController(CourseDbContext context)
        {
            _context = context;
        }
        // GET: GamififcationController
        public ActionResult Index()
        {
            return View();
        }

        // Метод для начисления опыта
        public async Task<IActionResult> AwardExperience(string userId, int points)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            user.ExperiencePoints += points;
            await _context.SaveChangesAsync();
            return Ok();
        }

        // Метод для проверки и присвоения достижений
        public async Task<IActionResult> CheckAchievements(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var achievements = _context.Achievements
                .Where(a => a.PointsThreshold <= user.ExperiencePoints && !user.Achievements.Contains(a))
                .ToList();

            foreach (var achievement in achievements)
            {
                user.Achievements.Add(achievement);
                if (achievement.Reward != null)
                {
                    user.Rewards.Add(achievement.Reward);
                }
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        // Метод для получения данных таблицы лидеров
        public IActionResult GetLeaderboard()
        {
            var leaderboard = _context.Users
                .OrderByDescending(u => u.ExperiencePoints)
                .Take(10)
                .Select(u => new
                {
                    u.UserName,
                    u.ExperiencePoints,
                    u.Title
                })
                .ToList();

            return Ok(leaderboard);
        }

        // Метод для отображения профиля пользователя
        public async Task<IActionResult> UserProfile(string userId)
        {
            var user = await _context.Users
                .Include(u => u.Achievements)
                .Include(u => u.Rewards)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
