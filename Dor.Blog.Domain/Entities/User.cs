using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dor.Blog.Domain.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Status { get; set; } = 1;
        [NotMapped]
        public virtual ICollection<String> RoleNames { get; set; }
    }
}
