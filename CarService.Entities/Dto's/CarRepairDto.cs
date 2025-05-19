namespace CarService.Entities.Dto_s
{
    public class CarRepairDto
    {
        public DateTime DateTime { get; set; } = DateTime.Now;
        public int CarId { get; set; }
        public int IssueId { get; set; }
        public int UserId { get; set; }
    }
}
