﻿namespace Webapp.data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);

        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(List<T> entities);
    }
}
