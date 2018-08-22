using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services.Description;
using Chess_GEMA.Models;
using Chess_GEMA.Models.Constants;
using Microsoft.AspNet.Identity;


namespace Chess_GEMA.Controllers.Admin
{
    [Authorize(Roles = CRoleNames.Administrator)]
    public class AdminController : DbContextController
    {
        // GET: Admin/Index
        [HttpGet]
        public ActionResult Index()
        {
            var users = _context.Users.Include(role => role.Roles).ToList();

            var viewModel = new UserRoleModel
            {
                ApplicationUsers = users,
                ApplicationRoles = GetIdentityRoles()
            };

            return View(CAdminViews.AdminIndex, viewModel);
        }


        // GET: Admin/Edit/{userId}
        [HttpGet]
        public ActionResult Edit(string userId)
        {
            var user = _context.Users.Include(role => role.Roles).SingleOrDefault(id => id.Id == userId);

            var viewModel = new UserRoleModel
            {
                ApplicationUser = user,
                OldUserRoles = GetRolesForUser(userId),
                ApplicationRoles = GetIdentityRoles()
            };

            return View(CAdminViews.AdminForm, viewModel);
        }


        // POST: Admin/Save{userId}
        [HttpPost]
        public ActionResult Save(string userId, string[] selectedRoles)
        {
            // kehrt zum Formular zurueck, bei falscher Eingabe
            if (!ModelState.IsValid || selectedRoles == null)
            {
                ViewBag.Error = "So geht das nicht!";
                return View(CAdminViews.AdminForm, GetUserRoleViewModel(userId));
            }

            var userManager = GetUserManager();
            var user = userManager.FindById(userId);
            var rolesForUser = GetRolesForUser(userId);

            // entfernt alle Rollen
            foreach (var role in rolesForUser)
            {
                userManager.RemoveFromRole(user.Id, role);
            }
            // fuegt alle ausgewaehlte Rollen hinzu
            foreach (var role in selectedRoles)
            {
                userManager.AddToRole(user.Id, role);
            }

            return RedirectToAction("Index", "Admin");
        }


        // GET: Admin/Delete/{userId}
        public ActionResult Delete(string userId)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return HttpNotFound("User not found!");
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }


        /**
         * Gibt ein UserRoleViewModel zurueck
         */
        private UserRoleModel GetUserRoleViewModel(string userId)
        {
            var user = _context.Users.Include(role => role.Roles).SingleOrDefault(id => id.Id == userId);

            var viewModel = new UserRoleModel
            {
                ApplicationUser = user,
                OldUserRoles = GetRolesForUser(userId),
                ApplicationRoles = GetIdentityRoles()
            };

            return viewModel;
        }

    }
}