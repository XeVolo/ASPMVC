using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPMVC.Models;

namespace ASPMVC.Controllers
{
	public class RoleController : Controller
	{
		// GET: Role
		public string Create()
		{
			IdentityManager im = new IdentityManager();

			im.CreateRole("Admin");
			im.CreateRole("Seller");

			return "OK";
		}


		public string AddToRole()
		{
			IdentityManager im = new IdentityManager();

			im.AddUserToRoleByUsername("adamnowak@gmail.com", "Admin");
			

			return "OK";
		}
	}
}