using BlogSystem.Areas.Identity.Data;

namespace BlogSystem.Core.Models
{
    public class Like
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int BlogPostId { get; set; }
        public virtual BlogPost BlogPost { get; set; }
    }
}
