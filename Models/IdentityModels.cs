using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SystemyBazDanychP1.Models
{
    // Możesz dodać dane profilu dla użytkownika, dodając więcej właściwości do klasy ApplicationUser. Odwiedź stronę https://go.microsoft.com/fwlink/?LinkID=317594, aby dowiedzieć się więcej.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Element authenticationType musi pasować do elementu zdefiniowanego w elemencie CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Dodaj tutaj niestandardowe oświadczenia użytkownika
            return userIdentity;
        }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Surrname { get; set; }

		public int AddressId { get; set; }

		public virtual AddressModel Address { get; set; }
		public virtual ICollection<OrderModel> OrderModels { get; set; }
		public virtual ICollection<OpinionModel> OpinionModels { get; set; }
		public virtual ICollection<SupportChatModel> SupportChats { get; set; }
		public virtual ICollection<SaleAnnouncementModel> SaleAnnouncementModels { get; set; }
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
		public DbSet<AddressModel> Addresses { get; set; }
		public DbSet<BasketModel> Baskets { get; set; }
		public DbSet<CategoryModel> Categories { get; set; }
		public DbSet<OpinionModel> Opinions { get; set; }
		public DbSet<OrderModel> Orders { get; set; }
		public DbSet<OrderProduct> OrderProducts { get; set; }
		public DbSet<ProductModel> Products { get; set; }
		public DbSet<SaleAnnouncementModel> SaleAnnouncements { get; set; }
		public DbSet<SpecialOfferModel> SpecialOfferts { get; set; }
		public DbSet<SupportChatModel> SupportChats { get; set; }
		public DbSet<BasketConnectorModel> BasketConnectors { get; set; }
	}
	public class IdentityManager
	{
		public RoleManager<IdentityRole> LocalRoleManager
		{
			get
			{
				return new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
			}
		}


		public UserManager<ApplicationUser> LocalUserManager
		{
			get
			{
				return new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
			}
		}


		public ApplicationUser GetUserByID(string userID)
		{
			ApplicationUser user = null;
			UserManager<ApplicationUser> um = this.LocalUserManager;

			user = um.FindById(userID);

			return user;
		}


		public ApplicationUser GetUserByName(string email)
		{
			ApplicationUser user = null;
			UserManager<ApplicationUser> um = this.LocalUserManager;

			user = um.FindByEmail(email);

			return user;
		}


		public bool RoleExists(string name)
		{
			var rm = LocalRoleManager;

			return rm.RoleExists(name);
		}


		public bool CreateRole(string name)
		{
			var rm = LocalRoleManager;
			var idResult = rm.Create(new IdentityRole(name));

			return idResult.Succeeded;
		}


		public bool CreateUser(ApplicationUser user, string password)
		{
			var um = LocalUserManager;
			var idResult = um.Create(user, password);

			return idResult.Succeeded;
		}


		public bool AddUserToRole(string userId, string roleName)
		{
			var um = LocalUserManager;
			var idResult = um.AddToRole(userId, roleName);

			return idResult.Succeeded;
		}


		public bool AddUserToRoleByUsername(string username, string roleName)
		{
			var um = LocalUserManager;

			string userID = um.FindByName(username).Id;
			var idResult = um.AddToRole(userID, roleName);

			return idResult.Succeeded;
		}


		public void ClearUserRoles(string userId)
		{
			var um = LocalUserManager;
			var user = um.FindById(userId);
			var currentRoles = new List<IdentityUserRole>();

			currentRoles.AddRange(user.Roles);

			foreach (var role in currentRoles)
			{
				um.RemoveFromRole(userId, role.RoleId);
			}
		}
	}
}