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

        [HttpPost("GetToWork")]
        public async Task<ActionResult> GetToWork([FromBody] MechanicAcceptDto mechanicAccept)
        {
            await _mechanicAddWork.AcceptMechanic(mechanicAccept.Id);

            return Ok();
        }

        public async Task<ActionResult> RejectToWork([FromBody] MechanicAcceptDto mechanicAccept)
        {
            await _mechanicAddWork.RejectMechanic(mechanicAccept.Id);
            return Ok();
        }

        [HttpGet("GetAllMechanics")]
        public async Task<ActionResult<List<MechanicDto>>> GetAllMechanics()
        {
            var mechanics = await _mechanicAddWork.GetAllMechanics();

            var mechanic = new List<MechanicDto>();

            foreach (var item in mechanics)
            {
                var mechanicDto = new MechanicDto
                {
                    Id = item.Id,
                    Username = item.Username,
                    WorkType = item.WorkType,
                    IsAccepted = item.IsAccepted
                };
                mechanic.Add(mechanicDto);
            }

            return Ok(mechanics);
        }
    }
}
