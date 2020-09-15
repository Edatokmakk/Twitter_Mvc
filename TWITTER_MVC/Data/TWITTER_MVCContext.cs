using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TWITTER_MVC.Models;

namespace TWITTER_MVC.Data
{
    public class TWITTER_MVCContext : DbContext
    {
        public TWITTER_MVCContext(DbContextOptions<TWITTER_MVCContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(b => b.CreateTime)
                .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Tweets>().Property(b => b.CreateDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Like>().HasKey(x => x.LikeId);
            modelBuilder.Entity<Retweets>().HasKey(k => k.RetweetId);
        //modelBuilder.Entity<Like>().HasKey(x => x.TweetID);
        //modelBuilder.Entity<Like>().HasKey(x => x.UserID);
    }
        public DbSet<TWITTER_MVC.Models.User> Users { get; set; }
        public DbSet<TWITTER_MVC.Models.Tweets> Tweets { get; set; }
        public DbSet<TWITTER_MVC.Models.Follower> Follower { get; set; }
        public DbSet<TWITTER_MVC.Models.Like> Likes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=TW_DB;Trusted_Connection=True;");
        }
        
    }
        }
