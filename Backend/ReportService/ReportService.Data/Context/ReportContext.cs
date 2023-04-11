using Microsoft.EntityFrameworkCore;
using ReportService.Data.Models;

namespace ReportService.Data.Context
{
    public class ReportContext : DbContext
    {
        public DbSet<Report> Reports { get; set; }
        public ReportContext(DbContextOptions<ReportContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Report>().Property(x => x.ReporterUserId).HasConversion(v => v.ToString(), v => Guid.Parse(v));
            modelBuilder.Entity<Report>().Property(x => x.TweetId).HasConversion(v => v.ToString(), v => Guid.Parse(v));

            base.OnModelCreating(modelBuilder);
        }
    }
}