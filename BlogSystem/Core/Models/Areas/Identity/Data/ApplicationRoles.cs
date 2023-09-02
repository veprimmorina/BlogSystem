using Microsoft.AspNetCore.Identity;

namespace BlogSystem.Areas.Identity.Data
{
    public class ApplicationRoles : IdentityRole
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }
}
