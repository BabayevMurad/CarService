namespace CarService.Entities
{
    public class Car
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        #pragma warning disable CS8618
        public string Name { get; set; }

        public string imageUrl { get; set; }

        #pragma warning restore CS8618
        public int Year { get; set; }
    }

}
