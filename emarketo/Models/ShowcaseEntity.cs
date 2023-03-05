namespace emarketo.Models
{
    public class ShowcaseEntity
    {
        public int Id { get; set; }
        public string Title_1 { get; set; } = null!;
        public string Title_2 { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string? LinkUrl { get; set; }
    }
}
