namespace BusinessObject.DTO
{
    public class PublisherCreateDTO
    {
        public string PublisherName { get; set; } = null!;
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
    }
}
