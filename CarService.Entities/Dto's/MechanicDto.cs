namespace CarService.Entities.Dto_s
{
    public class MechanicDto
    {
        public int Id { get; set; }
        #pragma warning disable CS8618
        public string WorkType { get; set; }
        public string Username { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        #pragma warning restore CS8618
        public bool IsAccepted { get; set; }
    }
}
