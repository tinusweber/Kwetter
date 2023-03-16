using Microsoft.EntityFrameworkCore;
using ProfileService.Data.Models;

namespace ProfileService.Data.Context
{
    public class ProfileContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public ProfileContext(DbContextOptions<ProfileContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
