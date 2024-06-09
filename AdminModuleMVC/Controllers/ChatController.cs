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
    public class ChatController : Controller
    {
        private readonly CourseDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public ChatController(CourseDbContext context, UserManager<IdentityUser> userManager)
        {
            _dbContext = context;
            _userManager = userManager;
        }

        
        public async Task<IActionResult> UserChat(string chatId)
        {
            var user = await _userManager.GetUserAsync(User);
            var chat = await _dbContext.UserChats.
                Include(c => c.Messages).
                FirstOrDefaultAsync(c => c.Id == chatId);
            var student = await _dbContext.Teachers.FirstOrDefaultAsync(c => c.UserId == user.Id);

            var model = new ChatViewModel
            {
                ChatId = chatId,
                LastInteraction = chat.LastInteraction,
                Name = user.Id == chat.FirstUserId ? chat.SecondUserName : chat.FirstUserName,
                Messages = chat.Messages.Select(c => new MessageViewModel
                {
                    AuthorName = c.UserName,
                    Content = c.Content,
                    CreationTime = c.CreationTime,
                    AuthorAvatarUrl = _dbContext.Images.FirstOrDefault(i => i.Id == c.ImageId).GetImageDataUrl(),
                    AuthorTitle = "Преподаватель"
                }).ToList()
            };

            return PartialView("PartialUserChat", model);
        }

        public async Task<IActionResult> CourseChat(string chatId)
        {
            var user = await _userManager.GetUserAsync(User);
            var chat = await _dbContext.CourseChats.
                Include(c => c.Messages).
                FirstOrDefaultAsync(c => c.Id == chatId);
            var student = await _dbContext.Teachers.FirstOrDefaultAsync(c => c.UserId == user.Id);
            var model = new ChatViewModel
            {
                ChatId = chatId,
                LastInteraction = chat.LastInteraction,
                Name = chat.CourseName,
                Messages = chat.Messages.Select(c => new MessageViewModel
                {
                    AuthorName = c.UserName,
                    Content = c.Content,
                    CreationTime = c.CreationTime,
                    AuthorAvatarUrl = _dbContext.Images.FirstOrDefault(i => i.Id == c.ImageId).GetImageDataUrl(),
                    AuthorTitle = "Преподаватель"
                }).ToList()
            };

            return PartialView("PartialCourseChat", model);
        }

        public async Task<IActionResult> StartChat(string userId)
        {
            var user = await _userManager.GetUserAsync(User);
            var student = await _dbContext.Teachers.
                Include(s => s.UserChats).
                FirstOrDefaultAsync(s => s.UserId == user.Id);

            var otherUser = await _dbContext.Teachers.
                Include(s => s.UserChats).
                FirstOrDefaultAsync(s => s.UserId == userId);

            var userChat = new UserChat
            {
                FirstUserId = user.Id,
                FirstUserName = student.Name + " " + student.Surname,

                SecondUserId = userId,
                SecondUserName = otherUser.Name + " " + otherUser.Surname
            };

            student.UserChats.Add(userChat);
            otherUser.UserChats.Add(userChat);

            await _dbContext.SaveChangesAsync();

            var chat = _dbContext.UserChats.FirstOrDefaultAsync(c => (c.FirstUserId == userId && c.SecondUserId == user.Id) || (c.FirstUserId == user.Id && c.SecondUserId == userId));

            return RedirectToAction("UserChat", new {chatId = chat.Id});
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var student = await _dbContext.Teachers.
                Include(s => s.UserChats).
                Include(s => s.CourseChats).
                FirstOrDefaultAsync(s => s.UserId == user.Id);

            var chats = student.UserChats.Select(u => new ChatViewModel
            {
                ChatId = u.Id,
                LastInteraction = u.LastInteraction,
                Name = user.Id == u.FirstUserId ? u.SecondUserName : u.FirstUserName
            }
            ).ToList();

            chats.AddRange(
                student.CourseChats.Select(u => new ChatViewModel
                {
                    ChatId = u.Id,
                    LastInteraction = u.LastInteraction,
                    Name = u.CourseName
                }
            ).ToList()
                );

            return View(new IndexChatViewModel 
            {
                Chats = chats
            });
        }



        [HttpPost]
        public async Task<IActionResult> SendUserMessage(string chatId, string messageContent)
        {
            var user = await _userManager.GetUserAsync(User);
            var student = await _dbContext.Teachers.
                Include(s => s.Avatar).
                FirstOrDefaultAsync(s => s.UserId == user.Id);
            var chat = await _dbContext.UserChats.
                Include(c => c.Messages).
                FirstOrDefaultAsync(c => c.Id == chatId);

            var newMessage = new Message()
            {
                ChatId = chatId,
                Content = messageContent,
                CreationTime = DateTime.UtcNow,
                UserId = user.Id,
                UserName = student.Name + " " + student.Surname,
                ImageId = student.Avatar.Id,
            };

            chat.Messages.Add(newMessage);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("UserChat", new { chatId = chatId });
        }

        [HttpPost]
        public async Task<IActionResult> SendCourseMessage(string chatId, string messageContent)
        {
            var user = await _userManager.GetUserAsync(User);
            var student = await _dbContext.Teachers.
                Include(s => s.Avatar).
                FirstOrDefaultAsync(s => s.UserId == user.Id);
            var chat = await _dbContext.CourseChats.
                Include(c => c.Messages).
                FirstOrDefaultAsync(c => c.Id == chatId);

            var newMessage = new Message()
            {
                ChatId = chatId,
                Content = messageContent,
                CreationTime = DateTime.UtcNow,
                UserId = user.Id,
                UserName = student.Name + " " + student.Surname,
                ImageId = student.Avatar.Id,
            };

            chat.Messages.Add(newMessage);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("CourseChat", new { chatId = chatId });
        }
    }
}
