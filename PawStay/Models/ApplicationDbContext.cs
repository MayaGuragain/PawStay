using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PawStay.Models
{
   
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
       //public DbSet<OwnerModel> OwnerModels { get; set; }
        public DbSet<PetModel> PetModels { get; set; }
        public DbSet<BookingModel> BookingModels { get; set; }
        public DbSet<ContactUsModel> ContactUsModels { get; set; }
        public DbSet<PetToOwnerModel> PetOwnerModels { get; set; }
        

        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}