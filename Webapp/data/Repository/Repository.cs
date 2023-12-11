using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Webapp.data.Repository.IRepository;

namespace Webapp.data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        ApplicationDbContext _db;
        DbSet<T> _dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db= db;
            _dbSet = db.Set<T>()    ;
        }
        public void Create(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(List<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet;
            if(filter != null)
            {
                query =query.Where(filter);
            }
            return query;
        }

        public T FirstOrDefault(Expression<Func<T,bool> > expr)
        {
            return _dbSet.FirstOrDefault(expr);
        }

       
        public void save()
        {
            _db.SaveChanges();
        }
    }
}
