using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gighub.Models;

namespace Gighub.Data
{
    public class AppDBContext:IdentityDbContext<AppUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options)
        {

        }
        public DbSet<Gig> Gig { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Following>().HasKey(f => new {f.FolloweeId,f.FollowerId });
          // builder.Entity<Following>().HasNoKey();
           // builder.Entity<Following>().HasOne(u => u.Followee).WithMany(u => u.Follower).IsRequired().OnDelete(DeleteBehavior.Restrict);
           // builder.Entity<Following>().HasOne(u => u.Follower).WithMany(u => u.Followee).IsRequired().OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
