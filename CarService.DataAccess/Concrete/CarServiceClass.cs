using CarService.DataAccess.Abstract;
using CarService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace CarService.DataAccess.Concrete
{
    public class CarServiceClass : ICarService
    {
        private readonly AppDataContext _appDataContext;
        private readonly IAppRepository _appRepository;

        public CarServiceClass(AppDataContext appDataContext, IAppRepository appRepository)
        {
            _appDataContext = appDataContext;
            _appRepository = appRepository;
        }

        public async Task<Car> CarGenerator(int userId)
        {
            var carIds = await _appDataContext.Cars.Select(c => c.Id).ToListAsync();

            if (carIds.Count == 0)
                return null;

            Random random = new Random();
            int randomId = carIds[random.Next(carIds.Count)];

            var selectedCar = await _appDataContext.Cars.FirstOrDefaultAsync(c => c.Id == randomId);

            int randomYear = new Random().Next(2020, 2025);

            var car = new Car
            {
                Id = selectedCar.Id,
                Name = selectedCar.Name,
                Year = randomYear,
                UserId = userId,
                imageUrl = selectedCar.imageUrl
            };

            return car;
        }


        public async Task<CarRepair> CarGoService(CarRepair car)
        {
            var carReturn = await _appDataContext.CarsRepair.AddAsync(car);

            await _appDataContext.SaveChangesAsync();

            return carReturn.Entity;
        }

        public async Task<Issue> CarIssueGenerator()
        {
            var issueCount = await _appDataContext.Issues.CountAsync();
            Random random = new Random();
            int randomId = random.Next(1, issueCount + 1);

            var issue = await _appDataContext.Issues.FirstOrDefaultAsync(i => i.Id == randomId);

            return issue;
        }

        public async Task AddIssueToSql()
        {
            List<Issue> list = new List<Issue>
            {
                new Issue { Level= "Easy"         , Problem= "Flat Tire"             , Description= "The tire loses air pressure and needs replacement."          },
                new Issue { Level= "Easy"         , Problem= "Oil Change"            , Description= "The engine oil needs to be changed regularly."               },
                new Issue { Level= "Easy"         , Problem= "Air Filter Replacement", Description= "The air filter is dirty and needs replacement."              },
                new Issue { Level= "Easy"         , Problem= "Battery Replacement"   , Description= "The battery is dead and requires replacement."               },
                new Issue { Level= "Easy"         , Problem= "Wiper Blade Change"    , Description= "The wiper blades are worn out and need changing."            },
                new Issue { Level= "Medium"       , Problem= "Brake Pad Replacement" , Description= "Brake pads are worn and need replacement."                   },
                new Issue { Level= "Medium"       , Problem= "Coolant Leak Repair"   , Description= "Coolant system is leaking and requires repair."              },
                new Issue { Level= "Medium"       , Problem= "Spark Plug Replacement", Description= "Spark plugs are faulty and need changing."                   },
                new Issue { Level= "Medium"       , Problem= "Wheel Alignment"       , Description= "Wheels need realignment to fix steering issues."             },
                new Issue { Level= "Medium"       , Problem= "Headlight Replacement" , Description= "The headlights are broken or burnt out."                     },
                new Issue { Level= "Difficult"    , Problem= "Transmission Failure"  , Description= "Transmission system is failing completely."                  },
                new Issue { Level= "Difficult"    , Problem= "Engine Overheating"    , Description= "Engine overheats and may be seriously damaged."              },
                new Issue { Level= "Difficult"    , Problem= "Timing Belt Failure"   , Description= "Timing belt has broken and engine synchronization is lost."  },
                new Issue { Level= "Difficult"    , Problem= "Fuel Pump Failure"     , Description= "Fuel pump has failed, causing fuel delivery issues."         },
                new Issue { Level= "Difficult"    , Problem= "Clutch System Failure" , Description= "Clutch has completely failed and needs full replacement."    },
            };

            foreach (var item in list)
            {
                await _appDataContext.Issues.AddAsync(item);
            }

            await _appDataContext.SaveChangesAsync();
        }

        public async Task AddcarToSql()
        {
            List<Car> list = new List<Car>
            {
                new Car { Name= "Toyota Corolla", Year= 2020, imageUrl= "/images/Toyota-Corolla.jpg" },
                new Car { Name= "Mercedes A-Class", Year= 2021, imageUrl= "/images/Mercedes-A-Class.jpg" },
                new Car { Name= "Audi A4", Year= 2023, imageUrl= "/images/Audi-A4.jpg" },
                new Car { Name= "BMW X5", Year= 2025, imageUrl= "/images/BMW-X5.jpg" }
            };
            foreach (var item in list)
            {
                await _appDataContext.Cars.AddAsync(item);
            }

            await _appDataContext.SaveChangesAsync();
        }

        public async Task<List<CarRepair>> CarsInService()
        {
            var cars = await _appDataContext.CarsRepair.ToListAsync();

            foreach (var item in cars)
            {
                var car = await _appDataContext.Cars.FirstOrDefaultAsync(c => c.Id == item.CarId);

                car.UserId = item.UserId;

                item.Car = car;

                await Console.Out.WriteLineAsync($"{item.UserId}");
            }

            return cars;
        }

        public async Task<List<Car>> GetHistory()
        {
            var cars = await _appDataContext.RepairHistories.ToListAsync();
            var returnCar = new List<Car>();
            foreach (var item in cars)
            {
                var car = await _appDataContext.Cars.FirstOrDefaultAsync(c => c.Id == item.CarId);

                car.UserId = item.Car.UserId;

                returnCar.Add(item.Car);
            }
            return returnCar;
        }

        public Task RemoveCarFromSevice(int id)
        {
            var task = _appRepository.DeleteAsync(_appDataContext.Cars.FirstOrDefault(c => c.Id == id));

            return task;
        }

        public async Task<Car> GetCarById(int id)
        {
            var car = await _appDataContext.Cars.FirstOrDefaultAsync(c => c.Id == id);
            return car;
        }

        public async Task<Issue> GetIssueById(int id)
        {
            var issue = await _appDataContext.Issues.FirstOrDefaultAsync(i => i.Id == id);
            return issue;
        }
    }
}