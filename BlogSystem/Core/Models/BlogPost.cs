using BlogSystem.Areas.Identity.Data;

namespace BlogSystem.Core.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublicationDate { get; set; }
        public string CreatorId { get; set; }
        public string Image { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
    }
}
