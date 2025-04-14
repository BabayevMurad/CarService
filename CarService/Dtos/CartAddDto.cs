using CarService.Entities;

namespace CarService.Dtos
{
    public class CartAddDto
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public List<DetailDto> Details { get; set; }
    }
}
