using CarService.Entities.Dto_s;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserToAdminChatCorrectWorkController : ControllerBase
    {
        // GET: api/<UserToAdminChatCorrectWorkController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<UserToAdminChatCorrectWorkController>
        [HttpPost]
        public void Post([FromBody] AddUserToAdminDto add)
        {

        }
    }
}
