using Webapp.Models;

namespace Webapp.data.Repository.IRepository
{
    public interface ICategoryRepository:IRepository<CategoryModel>
    {
        void Update(CategoryModel categoryObj);
    }
}
