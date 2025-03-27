using CarService.Entities;

namespace CarService.Data.Abstract
{
    public interface IAppRepository
    {
        Task AddAsync<T>(T entity) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;
        Task<bool> SaveAllAsync();
        Task<List<Detail>> GetAllDetails();
        Task<Detail> GetDetail(int id);
        Task<List<Category>> GetAllCategory();
        Task<Category> GetCategory(int id);

    }
}
