using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemyBazDanychP1.Models;

namespace SystemyBazDanychP1.Controllers
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

			im.AddUserToRoleByUsername("", "Admin");
			

			return "OK";
		}
	}
}