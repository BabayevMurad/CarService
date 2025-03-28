namespace CarService.Entities
{
    public class Detail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public virtual DetailImage? Image { get; set; }

    }
}
