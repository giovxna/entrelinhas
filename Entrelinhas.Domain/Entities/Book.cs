namespace Entrelinhas.Domain.Entities
{
    public class Book
    {
        public Guid BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateOnly? PublicationDate { get; set; }
        public string? Author { get; set; }
        public string ApiId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
