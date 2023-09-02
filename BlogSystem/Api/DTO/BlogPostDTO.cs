namespace BlogSystem.Api.DTO
{
    public class BlogPostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Tag { get; set; }
        public string Content { get; set; }
        public DateTime PublicationDate { get; set; }
        public string CreatorId { get; set; }

    }
}
