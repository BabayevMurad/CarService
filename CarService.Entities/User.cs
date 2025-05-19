namespace CarService.Entities
{
    public class User
    {
        public int Id { get; set; }
        #pragma warning disable CS8618
        public string Username { get; set; }

        #pragma warning restore CS8618
        public decimal Money { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
    }
}
