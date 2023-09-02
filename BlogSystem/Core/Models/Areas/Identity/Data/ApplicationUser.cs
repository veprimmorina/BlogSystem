using BlogSystem.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace BlogSystem.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    }
}
