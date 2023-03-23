using authService.DomainModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace authService.Data.Context
{
    public class AuthContext : IdentityDbContext<ApplicationUser>
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
