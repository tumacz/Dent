namespace TheApp.Domain.Entities
{
    public class ServiceTag
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}