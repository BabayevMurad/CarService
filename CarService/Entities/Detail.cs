namespace CarService.Entities
{
    public class Detail
    {
        public int Id { get; set; }
        public string Name { get; set; }
<<<<<<< HEAD
=======
        public string ImageUrl { get; set; }
        public int ImageId { get; set; }
>>>>>>> 74a0f62fc515818c61742c17be570d02bd9dfe90
        public virtual DetailImage? Image { get; set; }

    }
}
