namespace CarService.Entities.Dto_s
{
    public class AddDetailDto
    {
        #pragma warning disable CS8618
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        #pragma warning restore CS8618
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int Count { get; set; }
    }
}
