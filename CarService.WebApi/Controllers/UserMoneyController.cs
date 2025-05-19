using CarService.DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CarService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMoneyController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserMoneyController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetUserMoney/{id}")]
        public async Task<decimal> GetUserMoney(int userId)
        {
            var moneyHoney = await _userService.GetUserMoney(userId);

            return moneyHoney;
        }

        [HttpGet("AddUserMoney/{id}")]
        public async Task<decimal> AddUserMoney(int userId, decimal moneyHoney)
        {
            var money = await _userService.AddUserMoney(userId, moneyHoney);

            return money;
        }

        [HttpGet("UserPay/{id}")]
        public async Task<decimal> UserPay(int userId, decimal moneyHoney)
        {
            var money = await _userService.UserPay(userId, moneyHoney);

            return money;
        }
    }
}
