using System.ComponentModel.DataAnnotations;

namespace CarService.Entities.Dto_s
{
    public class MechanicRegisterDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string WorkType { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
