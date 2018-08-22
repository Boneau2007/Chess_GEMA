using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Chess_GEMA.Models
{
    public class UserRoleModel
    {
        public ApplicationUser ApplicationUser { get; set; }
        public IdentityRole IdentityRole { get; set; }

        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }

        public IList<string> OldUserRoles { get; set; }
        public IEnumerable<IdentityRole> ApplicationRoles { get; set; }
    }
}