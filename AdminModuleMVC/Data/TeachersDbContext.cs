using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminModuleMVC.Data
{
    public class TeachersDbContext : IdentityDbContext
    {
        public TeachersDbContext(DbContextOptions<TeachersDbContext> options)
            : base(options)
        {
        }

    }
}
