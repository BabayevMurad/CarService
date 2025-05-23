using CarService.DataAccess.Abstract;
using CarService.Entities;
using CarService.Entities.Dto_s;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserToAdminChatCorrectWorkController : ControllerBase
    {
        private readonly IAddUserToAdmin _addUserToAdmin;

        public UserToAdminChatCorrectWorkController(IAddUserToAdmin addUserToAdmin)
        {
            _addUserToAdmin = addUserToAdmin;
        }

        [HttpPost("GetAdminId")]
        public async Task<ActionResult<int>> GetAdminAndAddDatabase([FromBody] AddUserToAdminDto add)
        {
            var id = await _addUserToAdmin.GetAddmin(add.userId);

            await _addUserToAdmin.AddToDatabase(new AdminChatUsers
            {
                UserId = add.userId,
                AdminId = id
            });

            return Ok(id);
        }

        [HttpGet("GetUserList/{id}")]
        public async Task<List<User>> GetUserList(int id)
        {
            var userList = await _addUserToAdmin.GetUserList(id);
            return userList;
        }

        [HttpPost("EndChat")]
        public async Task<ActionResult<int>> EndChat([FromBody] AddUserToAdminDto add)
        {
            await _addUserToAdmin.DeleteUserFromChat(add.userId);

            return Ok();
        }

        [HttpGet("GetAdmin")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            var admin = await _addUserToAdmin.GetAdmin(id);
            return Ok(admin);
        }
    }
}