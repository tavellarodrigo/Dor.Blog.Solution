namespace Dor.Blog.Application.Authorization
{
    public record CredentialDTO
    {
        public string UserName { get; set; } = string.Empty;  
        public string Password { get; set; } = string.Empty;
    }
}
