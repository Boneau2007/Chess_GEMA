using System.Linq;
using Chess_GEMA.Controllers.Admin;
using Chess_GEMA.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Chess_GEMA.Startup))]
namespace Chess_GEMA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }





        //Hier erstellen wir standard Admin und Rollen fuer die Anwendung 
        private void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // Wir erstellen einen standard Admin, beim start des Projekts
            if (!roleManager.RoleExists("Admin"))
            {

               // Als aller erstes erstellen wir eine rolle Admin
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Und hier wird der eigentliche Benutzer erstellt
                var user = new ApplicationUser();
                user.UserName = "admin";
                user.Email = "admin@schachclub-gema.de";
                user.PhoneNumber = "01234/567890123";
                user.Title = "Administrator";
                user.Sex = "Herr";
                user.FirstName = "Admin";
                user.SurName = "Admos";
                user.City = "Admin City";
                user.Street = "Adminstreet";
                user.Postcode = 666666;
                user.StreetNumber = 15;
                string userPWD = "123456";

                var chkUser = userManager.Create(user, userPWD);

                //Und zum schluss dieser noch der Rolle Admin hinzugefueegt
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Admin");

                }
            }
            
            // Wir erstellen uns eine Rolle Moderator
            if (!roleManager.RoleExists("Moderator"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Moderator";
                roleManager.Create(role);

            }

            // Wir erstellen uns eine Rolle Benutzer
            if (!roleManager.RoleExists("User"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "User";
                roleManager.Create(role);

            }
        }
    }
}
