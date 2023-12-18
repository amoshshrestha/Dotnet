namespace Webapp.data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public ICategoryRepository Category {  get; }
        public IProductRepository Product { get; }
        public void save();
    }
}
