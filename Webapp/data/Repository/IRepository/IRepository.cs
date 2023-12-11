using System.Linq.Expressions;

namespace Webapp.data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null);
        T FirstOrDefault(Expression<Func<T, bool>> expr);

        void Create(T entity);
        
        void Delete(T entity);
        void DeleteRange(List<T> entities);
        void save();
    }
}
