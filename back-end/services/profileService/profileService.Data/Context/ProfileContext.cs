using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProfileService.Data.Models;

namespace ProfileService.Data.Context
{
    public class ProfileContext : DbContext
    {
        public DbSet<ProfileData> Profiles { get; set; }
        protected readonly IConfiguration Configuration;
        public ProfileContext(IConfiguration configuration,DbContextOptions<ProfileContext> options) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
