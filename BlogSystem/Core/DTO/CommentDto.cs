﻿namespace BlogSystem.Core.DTO
{
    public class CommentDto
    {
        public string AuthorName { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public int BlogPostId { get; set; }
        public int? ParentCommentId { get; set; }
    }
}
