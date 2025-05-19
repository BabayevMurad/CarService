using CarService.Entities;

namespace CarService.DataAccess.Abstract
{
    public interface ICarService
    {
        Task<Car> CarGenerator(int userId);
        Task<Issue> CarIssueGenerator();
        Task<CarRepair> CarGoService(CarRepair car);
        Task<List<CarRepair>> CarsInService();
        Task RemoveCarFromSevice(int id);
        Task AddIssueToSql();
        Task AddcarToSql();
        Task<Car> GetCarById(int id);
        Task<Issue> GetIssueById(int id);
        Task<List<Car>> GetHistory();
    }
}
