using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MicroBiz.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
    public class BaseDbContext : DbContext
    {
        public BaseDbContext()
            : base("DefaultConnection")
        {
        }
        public static BaseDbContext Create()
        {
            return new BaseDbContext();
        }
    }
    public class EntityDbContext : BaseDbContext
    {
        public System.Data.Entity.DbSet<MicroBiz.Entity> Entities { get; set; }
    }
    public class EntityTypeDbContext : BaseDbContext
    {
        public System.Data.Entity.DbSet<MicroBiz.EntityType> EntityTypes { get; set; }

    }
    public class CustomerDbContext : BaseDbContext
    {
        public System.Data.Entity.DbSet<MicroBiz.Customer> Customers { get; set; }

    }
    public class CategoryDbContext : BaseDbContext
    {
        public System.Data.Entity.DbSet<MicroBiz.Category> Categories { get; set; }

    }
    public class SubCategoryDbContext : BaseDbContext
    {
        public System.Data.Entity.DbSet<MicroBiz.SubCategory> SubCategories { get; set; }
    }
    public class ProductDbContext : BaseDbContext
    {
        public System.Data.Entity.DbSet<MicroBiz.Product> Products { get; set; }

    }
    public class AddressDbContext : BaseDbContext
    {
        public System.Data.Entity.DbSet<MicroBiz.Address> Addresses { get; set; }

    }

    public class StateDbContext : BaseDbContext
    {
        public System.Data.Entity.DbSet<MicroBiz.State> States { get; set; }

    }
    public class CountryDbContext : BaseDbContext
    {
        public System.Data.Entity.DbSet<MicroBiz.Country> Countries { get; set; }

    }
    public class OrderDbContext : BaseDbContext
    {
        public System.Data.Entity.DbSet<MicroBiz.Order> Orders { get; set; }
    }
    public class OrderDetailDbContext : BaseDbContext
    {
        public System.Data.Entity.DbSet<MicroBiz.OrderDetail> OrderDetails { get; set; }
    }
}