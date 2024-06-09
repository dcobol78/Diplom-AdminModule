// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AdminModuleMVC.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CourseShared.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AdminModuleMVC.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly CourseDbContext _dbContext;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            CourseDbContext context)
        {
            _dbContext = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public DateOnly? DOB { get; set; }

        public string AvatarUrl { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            public IFormFile ProfileImage { get; set; } // Property for file upload
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            var teacher = await _dbContext.Teachers
                                          .Include(s => s.Avatar)
                                          .FirstOrDefaultAsync(s => s.UserId == user.Id);

            if (teacher == null)
            {
                throw new Exception("Пользователь с таким UserId не найден");
            }

            Name = teacher.Name;
            Surname = teacher.Surname;
            Patronymic = teacher.Patronymic;
            DOB = teacher.DOB;

            AvatarUrl = teacher.Avatar?.GetImageDataUrl() ?? "/files/images/default-avatar.png"; // Default avatar URL

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            var teacher = await _dbContext.Teachers
                                          .Include(s => s.Avatar)
                                          .FirstOrDefaultAsync(s => s.UserId == user.Id);

            if (Input.ProfileImage != null && Input.ProfileImage.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Input.ProfileImage.CopyToAsync(memoryStream);
                    var imageData = memoryStream.ToArray();
                    var mimeType = Input.ProfileImage.ContentType; // Get the MIME type

                    if (teacher.Avatar == null)
                    {
                        teacher.Avatar = new Image
                        {
                            ImageTitle = Input.ProfileImage.FileName,
                            ImageData = imageData,
                            MimeType = mimeType // Save the MIME type
                        };
                    }
                    else
                    {
                        teacher.Avatar.ImageTitle = Input.ProfileImage.FileName;
                        teacher.Avatar.ImageData = imageData;
                        teacher.Avatar.MimeType = mimeType; // Update the MIME type
                    }
                }

                _dbContext.Teachers.Update(teacher);
                await _dbContext.SaveChangesAsync();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
