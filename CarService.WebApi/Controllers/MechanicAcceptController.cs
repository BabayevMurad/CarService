using CarService.DataAccess;
using CarService.DataAccess.Abstract;
using CarService.Entities;
using CarService.Entities.Dto_s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MechanicAcceptController : ControllerBase
    {
        private readonly IMechanicAddWork _mechanicAddWork;
        private readonly AppDataContext _context;

        public MechanicAcceptController(IMechanicAddWork mechanicAddWork, AppDataContext context)
        {
            _mechanicAddWork = mechanicAddWork;
            _context = context;
        }

        [HttpPost("GetToWork")]
        public async Task<ActionResult> GetToWork([FromBody] MechanicAcceptDto mechanicAccept)
        {
            await _mechanicAddWork.AcceptMechanic(mechanicAccept.Id);

            return Ok();
        }

        [HttpPost("RejectToWork")]
        public async Task<ActionResult> RejectToWork([FromBody] MechanicAcceptDto mechanicAccept)
        {
            await _mechanicAddWork.RejectMechanic(mechanicAccept.Id);
            return Ok();
        }

        [HttpGet("GetAllAccecptedMechanics")]
        public async Task<ActionResult<List<MechanicDto>>> GetAllAccecptedMechanics()
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

        [HttpGet("GetAllMechanicsToWork")]
        public async Task<ActionResult<List<MechanicDto>>> GetAllMechanicsToWork()
        {
            var mechanics = await _mechanicAddWork.GetAllMechanicsWork();

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

        [HttpGet("GetMechanicById/{id}")]
        public async Task<ActionResult<MechanicDto>> GetMechanicById(int id)
        {
            var mechanics = await _mechanicAddWork.GetMechanicById(id);

            var mechanicDto = new MechanicDto
            {
                Id = mechanics.Id,
                Username = mechanics.Username,
                WorkType = mechanics.WorkType,
                IsAccepted = mechanics.IsAccepted
            };

            return Ok(mechanics);
        }

        [HttpPost("AddInfoMechanic")]
        public async Task<ActionResult> AddInfoMechanic([FromBody] MechanicDto mechanicDto)
        {
            await _mechanicAddWork.AddInfoMenhanic(new Mechanic
            {
                Id = mechanicDto.Id,
                Name = mechanicDto.Name,
                Surname = mechanicDto.Surname,
                WorkType = mechanicDto.WorkType,
                Username = mechanicDto.Username,
                IsAccepted = mechanicDto.IsAccepted
            });

            return Ok();
        }

        [HttpGet("GetMechanicByUsername/{username}")]
        public async Task<ActionResult> GetMechanicByUsername(string username)
        {
            var user = await _context.Mechanics.FirstOrDefaultAsync(m => m.Username == username);
            if (user == null) return NotFound();

            return Ok(new
            {
                name = user.Name,
                surname = user.Surname,
                workType = user.WorkType
            });
        }

        [HttpGet("GetMechanicStar/{id}")]
        public async Task<int> GetMechanicStar(int id)
        {
            var mechanic = await _context.Mechanics.FirstOrDefaultAsync(m => m.Id == id);
            if (mechanic!.WorkYear > 0 && mechanic.WorkYear < 3)
                return 1;
            else if (mechanic.WorkYear >= 3 && mechanic.WorkYear < 5)
                return 2;
            else if (mechanic.WorkYear >= 5 && mechanic.WorkYear < 10)
                return 3;
            else if (mechanic.WorkYear >= 10 && mechanic.WorkYear < 15)
                return 4;
            else if (mechanic.WorkYear >= 15)
                return 5;
            else
                return 0; // Default case if no conditions are met
        }
    }
}
