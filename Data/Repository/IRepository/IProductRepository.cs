using Webapp.Models;

namespace Webapp.data.Repository.IRepository
{
    public interface IProductRepository:IRepository<Product>
    {
        void Update(Product productObj);
    }
}
