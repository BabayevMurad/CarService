using CarService.Entities;


namespace CarService.DataAccess.Abstract
{
    public interface IAppRepository
    {
        Task AddAsync<T>(T entity) where T : class;
        Task<Cart> AddCartAsync(Cart cart);
        Task DeleteAsync<T>(T entity) where T : class;
        Task<bool> SaveAllAsync();
        Task<List<Detail>> GetAllDetails();
        Task<List<Detail>> GetAllDetailsByCategory(int id);
        Task<Detail> GetDetail(int id);
        Task<List<Category>> GetAllCategory();
        Task<Category> GetCategory(int id);
        Task<Detail> EditDetail(Detail detailNew, int id);
        Task<Category> EditCategory(Category categoryNew, int id);
    }
}
