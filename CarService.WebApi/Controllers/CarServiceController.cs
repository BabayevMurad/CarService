using CarService.DataAccess.Abstract;
using CarService.Entities;
using CarService.Entities.Dto_s;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarServiceController : ControllerBase
    {

        private readonly ICarService _carService;

        public CarServiceController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("RandomCar")]
        public async Task<Car> GetRandomCar(int userid)
        {
            var car = await _carService.CarGenerator(userid);

            return car;
        }

        [HttpGet("RandomIssue")]
        public async Task<Issue> GetRandomIssue()
        {
            var issue = await _carService.CarIssueGenerator();
            return issue;
        }

        [HttpGet("AddDatabase")]
        public async Task AddDatabseIssue()
        {
            await _carService.AddIssueToSql();
        }

        [HttpGet("AddDatabaseCar")]
        public async Task AddDatabseCar()
        {
            await _carService.AddcarToSql();
        }

        [HttpGet("CarsInService")]
        public async Task<List<CarRepair>> GetCarsInService()
        {
            var cars = await _carService.CarsInService();

            return cars;
        }

        [HttpPost("CarToService")]
        public async Task<CarRepair> CarToService([FromBody]CarRepairDto car)
        {
            var carAdd = await _carService.GetCarById(car.CarId);

            var issueAdd = await _carService.GetIssueById(car.IssueId);

            carAdd.UserId = car.UserId;

            var newRapair = new CarRepair
            {
                CarId = carAdd.Id,
                Car = carAdd,
                IssueId = issueAdd.Id,
                Issue = issueAdd,
                DateTime = DateTime.Now,
                UserId = car.UserId,
            };

            var car1 = await _carService.CarGoService(newRapair);

            return car1;
        }

        [HttpDelete("RemoveCarFromService/{id}")]
        public Task RemoveCarFromService(int id)
        {
            return _carService.RemoveCarFromSevice(id);
        }

        [HttpGet("GetCarHistory")]
        public async Task<List<Car>> GetCarHistory()
        {
            var cars = await _carService.GetHistory();
            return cars;
        }
    }
}
