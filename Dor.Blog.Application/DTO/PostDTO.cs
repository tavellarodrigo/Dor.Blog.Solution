namespace Dor.Blog.Application.DTO
{
    public record PostDTO
    {
        public int Id { get; set; }        
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; } = null;
    }
}
