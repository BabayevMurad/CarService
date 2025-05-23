namespace CarService.Entities
{
    public class Mechanic
    {
        public int Id { get; set; }
        #pragma warning disable CS8618
        public string Name { get; set; }
        public string Surname { get; set; }
        public string WorkType { get; set; }
        public string Username { get; set; }
        #pragma warning restore CS8618
        public bool IsAccepted { get; set; } = false;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
    }
}
