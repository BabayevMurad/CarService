namespace CarService.Entities
{
    public class Category
    {
        public int Id { get; set; }
        #pragma warning disable CS8618 
        public string Name { get; set; }

        #pragma warning restore CS8618 
        public virtual List<Detail>? Details { get; set; }
    }
}
