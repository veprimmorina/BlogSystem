using BlogSystem.Areas.Identity.Data;

namespace BlogSystem.Core.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public bool IsRead { get; set; }
    }
}
