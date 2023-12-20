using Microsoft.EntityFrameworkCore;
using Models;
using Webapp.data.Repository.IRepository;
using Webapp.Models;

namespace Webapp.data.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db):base(db)
        {

            _db = db;
        }

       

        public void Update(ShoppingCart shoppingCart)
        {
            _db.ShoppingCarts.Update(shoppingCart);
        }
    }
}
