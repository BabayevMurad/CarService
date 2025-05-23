using CarService.Entities;

namespace CarService.DataAccess.Abstract
{
    public interface IService
    {
        Task<Car> GetCarForRepair(string workType);
        Task<Issue> GetCarIssue(int carId);
        Task CarRepairAndAddtoStock(int carId);
        Task<decimal> GetRepairPrice(int issueId);        
        decimal GetRepairPrice(Issue issue);        
    }
}
