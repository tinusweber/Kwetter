using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetService.Data.Models;

namespace TweetService.Data.Context
{
    public class TweetContext : DbContext
    {
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<User> Users { get; set; }
        public TweetContext(DbContextOptions<TweetContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tweet>().Property(x => x.TweeterId).HasConversion(v => v.ToString(), v => Guid.Parse(v));
            base.OnModelCreating(modelBuilder);
        }
    }
}
