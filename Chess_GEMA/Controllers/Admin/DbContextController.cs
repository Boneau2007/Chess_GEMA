using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Chess_GEMA.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Chess_GEMA.Controllers.Admin
{
    public class DbContextController : Controller
    {
        protected ApplicationDbContext _context;

        public DbContextController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        /**
        * Gibt alle existierenden Rollen im System zurueck
        */
        protected IEnumerable<IdentityRole> GetIdentityRoles()
        {
            var roleStore = new RoleStore<IdentityRole>(_context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            return roleManager.Roles;
        }

        /**
         * Gibt ein Rolle anhand ihrer Id zurueck
         */
        protected IdentityRole GetIdentityRole(string roleId)
        {
            return GetIdentityRoles().FirstOrDefault(id => id.Id == roleId);
        }

        /**
         * Gibt alle Rollen eines Users zurueck
         */
        protected IList<string> GetRolesForUser(string userId)
        {
            var userStore = new UserStore<ApplicationUser>(_context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = userManager.FindById(userId);
            return userManager.GetRoles(user.Id);
        }


        /**
         * Gibt einen UserManager zurueck
         */
        protected UserManager<ApplicationUser> GetUserManager()
        {
            var userStore = new UserStore<ApplicationUser>(_context);
            return new UserManager<ApplicationUser>(userStore);
        }
    }
}