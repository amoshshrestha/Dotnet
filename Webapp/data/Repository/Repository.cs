using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
