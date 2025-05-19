using CarService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.DataAccess.Abstract
{
    public interface IService
    {
        Task<Car> GetCarForRepair();
        Task<Issue> GetCarIssue(int carId);
        Task CarRepairAndAddtoStock(int carId);
        Task<decimal> GetRepairPrice(int issueId);        
        decimal GetRepairPrice(Issue issue);        
    }
}
