using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Chess_GEMA.Models
{
    // Sie können Profildaten für den Benutzer durch Hinzufügen weiterer Eigenschaften zur ApplicationUser-Klasse hinzufügen. Weitere Informationen finden Sie unter "http://go.microsoft.com/fwlink/?LinkID=317594".
    public class ApplicationUser : IdentityUser
    {
        
        public string Sex { get; set; }
        public string Title { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public int Postcode { get; set; }
        public string City { get; set; }
        public string Company { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Beachten Sie, dass der "authenticationType" mit dem in "CookieAuthenticationOptions.AuthenticationType" definierten Typ übereinstimmen muss.
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Benutzerdefinierte Benutzeransprüche hier hinzufügen
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // Alle Image Tabellen
        public DbSet<Image> Image { get; set; }

        // Alle File Tabellen
        public DbSet<File> Files { get; set; }

        // Alle Gallery Tabellen
        public DbSet<Gallery> Gallery { get; set; }

        // Alle Videos Tabellen
        public DbSet<Movie> Movies { get; set; }

        // Alle Neuigkeiten Tabellen
        public DbSet<Articel> News { get; set; }
        
        // Alle NewsFeed Tabellen
        public DbSet<NewsFeed> NewsFeed { get; set; }

        // Alle Antwort Tabellen
        public DbSet<Answer> Answer { get; set; }

        // Alle Frage Tabellen
        public DbSet<Question> Question { get; set; }

        public DbSet<Home> Home { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}