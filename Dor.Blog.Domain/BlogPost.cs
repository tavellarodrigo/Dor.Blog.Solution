using System.ComponentModel.DataAnnotations;

namespace Dor.Blog.Domain
{
    public class BlogPost
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; } = null;

    }
}
