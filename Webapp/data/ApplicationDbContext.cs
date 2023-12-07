using Microsoft.EntityFrameworkCore;
using Webapp.Models;

namespace Webapp.data
{
    public class ApplicationDbContext
        : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<CategoryModel> Categories { get; set; }


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