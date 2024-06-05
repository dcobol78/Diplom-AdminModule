using AdminModuleMVC.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("TeachersConnection") ?? throw new InvalidOperationException("Connection string 'TeachersConnection' not found.");

var courseConnectionString = builder.Configuration.GetConnectionString("CoursesConnection") ?? throw new InvalidOperationException("Connection string 'CoursesConnection' not found.");

// Add AplicationDbContext
builder.Services.AddDbContext<TeachersDbContext>(options =>
    options.UseSqlServer(connectionString));
// Add CourseDbContext to services.
builder.Services.AddDbContext<CourseDbContext>(options =>
    options.UseSqlServer(courseConnectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<TeachersDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
