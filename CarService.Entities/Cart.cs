namespace CarService.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Time { get; set; }
        public virtual List<CartDetail>? Details { get; set; }
    }
}
