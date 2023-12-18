using Microsoft.EntityFrameworkCore;
using Webapp.data.Repository.IRepository;
using Webapp.Models;

namespace Webapp.data.Repository
{
    public class CategoryRepository: Repository<CategoryModel>,ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db):base(db)
        {

            _db = db;
        }

        public void Update(CategoryModel categoryObj)
        {
            _db.Categories.Update(categoryObj);
        }
    }
}
