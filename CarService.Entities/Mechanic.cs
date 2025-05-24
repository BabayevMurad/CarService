namespace CarService.Entities
{
    public class Mechanic
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; } 
        public string WorkType { get; set; }
        public string Username { get; set; }
        public int WorkYear { get; set; }
        public bool IsAccepted { get; set; } = false;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
    }
}
