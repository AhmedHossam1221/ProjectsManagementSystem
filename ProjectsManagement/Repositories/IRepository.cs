using ProjectsManagement.Models;


namespace ProjectsManagement.Repositories
{
    public interface IRepository<T> where T : BaseModel
    {
       
        IQueryable<T> GetAll();
        T GetByID(int id);
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        public void AddRange(List<T> entities);
        public IQueryable<T> GetAllPagination(int pageNumber, int pageSize);

        void SaveChanges();
    }
}
