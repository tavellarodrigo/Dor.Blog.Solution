using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dor.Blog.Domain.Entities
{
    public class User : IdentityUser
    {
        //random fields, just for simulate a real case
        public string Name { get; set; }=String.Empty;
        public string LastName { get; set; } = String.Empty;
        public int Status { get; set; } = 1;

        [NotMapped]
        public virtual ICollection<String> RoleNames { get; set; }= new HashSet<string>();
        [NotMapped]
        public String token { get; set; }=String.Empty;
    }
}
