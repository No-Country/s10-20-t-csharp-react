using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using quejapp.Models;
using s10.Back.Models;

namespace s10.Back.Data
{
    public class RedCoContext : IdentityDbContext<AppUser>
    {
        public RedCoContext(DbContextOptions<RedCoContext> options) : base(options)
        { }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Locality> Locality { get; set; }
        public virtual DbSet<Queja> Queja { get; set; }
        public virtual DbSet<AppUser> AppUser { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Queja>()
                    .HasMany(e => e.Comments)
                    .WithOne(e => e.Complaint)
                    .HasForeignKey(e => e.Complaint_ID)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

            builder.Entity<Locality>()
                .HasMany(e => e.Districts)
                .WithOne(e => e.Locality)
                .HasForeignKey(e => e.Locality_ID)
                .IsRequired();

            //builder.Entity<AppUser>()
            //   .HasMany(e => e.Comments)
            //   .WithOne(e => e.User)
            //   .HasForeignKey(e => e.User_ID)
            //   .IsRequired();

            builder.Entity<Category>().Property(t => t.Category_ID).ValueGeneratedOnAdd();
            builder.Entity<Comment>().Property(p => p.Comment_ID).ValueGeneratedOnAdd();
            builder.Entity<District>().Property(p => p.District_ID).ValueGeneratedOnAdd();
            builder.Entity<Locality>().Property(m => m.Locality_ID).ValueGeneratedOnAdd();
            builder.Entity<Queja>().Property(m => m.Complaint_ID).ValueGeneratedOnAdd();
           
        }
    }

}
