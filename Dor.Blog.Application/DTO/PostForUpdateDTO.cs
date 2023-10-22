namespace Dor.Blog.Application.DTO
{
    public record PostForUpdateDTO
    {     
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
