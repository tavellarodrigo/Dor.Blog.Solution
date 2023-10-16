using System.ComponentModel.DataAnnotations;

namespace Dor.Blog.Application.Authorization
{
    public class CredentialDTO
    {
        public string UserName { get; set; } = string.Empty;  
        public string Password { get; set; } = string.Empty;
    }
}
