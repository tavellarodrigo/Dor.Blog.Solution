namespace Dor.Blog.Application.DTO
{
    public record UserDTO
    {
        public string Name { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public int Status { get; set; } = 1;
        public string UserName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string RolName { get; set; } = String.Empty;


    }
}
