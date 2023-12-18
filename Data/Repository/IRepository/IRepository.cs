using System.Linq.Expressions;

namespace Webapp.data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null,string? includeProperties= null);
        T FirstOrDefault(Expression<Func<T, bool>> expr, string? includeProperties = null);

        void Create(T entity);
        
        void Delete(T entity);
        void DeleteRange(List<T> entities);
        
    }
}
