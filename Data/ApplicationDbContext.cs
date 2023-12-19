using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Webapp.Models;

namespace Webapp.data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
        
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CategoryModel>().HasData(new CategoryModel
            {
                CategoryId = 1,
                CategoryName = "TEST"

            });

        }


    }
}