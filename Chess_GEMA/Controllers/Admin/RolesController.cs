
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Chess_GEMA.Models;
using Chess_GEMA.Models.Constants;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Chess_GEMA.Controllers.Admin
{
    /*
    * Diese Klasse ist grundsaetzlich dazu da um in unserer Webanwendung Eine Benutzerrollenvergabe zu 
    * ermoeglichen.
    * 
    */
    public class RolesController : DbContextController
    {
        // GET: Roles/Index
        public ActionResult Index()
        {
            var viewModel = new UserRoleModel
            {
                ApplicationRoles = GetIdentityRoles(),
                ApplicationUsers = _context.Users.ToList()

            };

            return View(CAdminViews.RolesIndex, viewModel);
        }


        // GET: Roles/Save
        public ActionResult New()
        {
            var emptyIdentityRole = new IdentityRole
            {
                Id = String.Empty
            };

            return View(CAdminViews.RolesForm, emptyIdentityRole);
        }


        // Edit: Roles/Edit/{roleId}
        public ActionResult Edit(string roleId)
        {
            var identityRole = GetIdentityRoles().SingleOrDefault(id => id.Id == roleId);

            if (identityRole == null)
            {
                return HttpNotFound("This role doesn't exist");
            }



            return View(CAdminViews.RolesForm, identityRole);

        }

        // POST: Roles/Save/{roleId}
        [HttpPost]
        public ActionResult Save(IdentityRole identityRole)
        {
            //Wenn der Admin nicht valide ist, gib Error aus
            if (identityRole.Name.IsNullOrWhiteSpace())
            {
                ViewBag.Error = "Das Feld \"Name\" darf nicht leer sein!";
                return View(CAdminViews.RolesForm, identityRole);
            }

            // Erstellt ein Objekt vom Typen RoleManager
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));

            //Wenn die Id nicht Valide ist
            if (identityRole.Id.IsNullOrWhiteSpace())
            {
                //Ueberpruefe ob die Rolle Existiert und falls nicht, erstelle ein Object mit dessen Namen
                if (identityRole.Name != null && !roleManager.RoleExists(identityRole.Name))
                {
                    var role = new IdentityRole
                    {
                        Name = identityRole.Name
                    };
                    //Speichere diese in der Datenbank ab
                    roleManager.Create(role);
                }
            }
            else
            {
                var role = GetIdentityRole(identityRole.Id);
                role.Name = identityRole.Name;
                roleManager.Update(role);
            }

            return RedirectToAction("Index", "Roles");
        }

        //Loescht eine Userrolle
        // Delete: Roles/Delete/{roleId}
        public ActionResult Delete(string roleId)
        {
            // Erstellt ein Objekt vom Typen RoleManager
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));
            var identityRole = GetIdentityRole(roleId);

            if (roleManager.RoleExists(identityRole.Name))
            {
                roleManager.Delete(identityRole);
            }

            return RedirectToAction("Index", "Roles");
        }


        public ActionResult CreateRoles(string newRoleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));       
            if (!roleManager.RoleExists(newRoleName))
            {
                //Als erstes erstellen wir einen Admin
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = newRoleName;
                roleManager.Create(role);
            }
            return RedirectToAction("Index", "User");
        }
    }
}