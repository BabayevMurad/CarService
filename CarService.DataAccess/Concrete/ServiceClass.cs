using CarService.DataAccess.Abstract;
using CarService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.DataAccess.Concrete
{
    public class ServiceClass : IService
    {
        private readonly AppDataContext _context;

        public ServiceClass(AppDataContext context)
        {
            _context = context;
        }

        public async Task CarRepairAndAddtoStock(int carRepairId)
        {
            var carRepair = await _context.CarsRepair.FirstOrDefaultAsync(c => c.CarId == carRepairId);

            _context.RepairHistories.Add(new RepairHistory
            {
                CarId = carRepair.CarId,
                RepairDate = DateTime.Now,
                Cost = GetRepairPrice(carRepair.Issue)
            });

            _context.CarsRepair.Remove(carRepair);

            await _context.SaveChangesAsync();
        }

        public async Task<Issue> GetCarIssue(int carId)
        {
            var issue = await _context.CarsRepair
                    .Include(c => c.Issue)
                    .FirstOrDefaultAsync(c => c.CarId == carId);

            return issue.Issue;
        }

        public async Task<Car> GetCarForRepair()
        {
            var car = await _context.CarsRepair.
                Include(c => c.Car)
                .FirstAsync();

            return car.Car;
        }

        public async Task<decimal> GetRepairPrice(int issueId)
        {
            var issue = await _context.Issues.FirstOrDefaultAsync(i => i.Id == issueId);

            if (issue.Level == "Medium")
            {
                return (decimal)160;
            }
            else if (issue.Level == "Difficult")
            {
                return (decimal)270;
            }
            else if (issue.Level == "Cant't Repair")
            {
                return (decimal)999999999999999;
            }
            else
            {
                return (decimal)0;
            }
        }

        public decimal GetRepairPrice(Issue issue)
        {
            if (issue.Level == "Medium")
            {
                return (decimal)160;
            }
            else if (issue.Level == "Difficult")
            {
                return (decimal)270;
            }
            else if (issue.Level == "Cant't Repair")
            {
                return (decimal)999999999999999;
            }
            else
            {
                return (decimal)0;
            }
        }
    }
}
