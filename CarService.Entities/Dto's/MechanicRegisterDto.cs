using System.ComponentModel.DataAnnotations;

namespace CarService.Entities.Dto_s
{
    public class MechanicRegisterDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string WorkType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
