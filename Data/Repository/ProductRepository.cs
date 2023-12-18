using Microsoft.EntityFrameworkCore;
using Webapp.data.Repository.IRepository;
using Webapp.Models;

namespace Webapp.data.Repository
{
    public class ProductRepository: Repository<Product>,IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db):base(db)
        {

            _db = db;
        }

        public void Update(Product productObj)
        {
            _db.Products.Update(productObj);
        }
    }
}
