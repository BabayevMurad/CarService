namespace CarService.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Detail>? Details { get; set; }
    }
}
