using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dor.Blog.Domain.Entities
{
    /// <summary>
    /// main table
    /// </summary>
    public class BlogPost
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; } = null;
        public bool Deleted { get; set; } = false;

        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = new User();

        //TO DO
        //add Category Table
        //add Comment Table

    }
}
