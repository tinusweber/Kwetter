using CommentService.Data.Models;
using Microsoft.EntityFrameworkCore;
namespace CommentService.Data.Context
{
    public class CommentContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public CommentContext(DbContextOptions<CommentContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Comment>().Property(x => x.CommenterId).HasConversion(v => v.ToString(), v => Guid.Parse(v));
            modelBuilder.Entity<Comment>().Property(x => x.TweetId).HasConversion(v => v.ToString(), v => Guid.Parse(v));

        }
    }
}
