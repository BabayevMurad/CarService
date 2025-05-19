namespace CarService.Entities
{
    public class Issue
    {
        public int Id { get; set; }
        #pragma warning disable CS8618
        public string Level { get; set; }
        public string Problem { get; set; }
        public string Description { get; set; }

        #pragma warning restore CS8618
    }
}
