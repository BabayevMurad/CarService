namespace CarService.Entities
{
    public class DetailImage
    {
        public int Id { get; set; }
        public int DetailId { get; set; }
        public virtual Detail? Detail { get; set; }
        public string Url { get; set; }
    }
}
