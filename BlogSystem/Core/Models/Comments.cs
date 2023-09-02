namespace BlogSystem.Core.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string AuthorName { get; set; } 
        public string Email { get; set; } 
        public string Content { get; set; }
        public int BlogPostId { get; set; }
        public int? ParentCommentId { get; set; }

        public virtual BlogPost BlogPost { get; set; }
        public virtual Comment ParentComment { get; set; }
        public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>();

    }
}
