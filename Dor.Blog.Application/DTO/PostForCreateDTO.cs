namespace Dor.Blog.Application.DTO
{
    public record PostForCreateDTO
    {        
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;

    }
}
