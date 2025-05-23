using CarService.DataAccess.Abstract;
using CarService.Entities.Dto_s;
using Microsoft.AspNetCore.Mvc;

namespace CarService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MechanicAcceptController : ControllerBase
    {
        private readonly IMechanicAddWork _mechanicAddWork;

        public MechanicAcceptController(IMechanicAddWork mechanicAddWork)
        {
            _mechanicAddWork = mechanicAddWork;
        }

        [HttpPost]
        public async Task<ActionResult> GetToWork([FromBody] MechanicAcceptDto mechanicAccept)
        {
            await _mechanicAddWork.AcceptMechanic(mechanicAccept.Id);

            return Ok();
        }
    }
}
