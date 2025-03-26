namespace CarService.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Detail>? Details { get; set; }
    }
}
