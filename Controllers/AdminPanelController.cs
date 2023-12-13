using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SystemyBazDanychP1.Controllers
{
    public class AdminPanelController : Controller
    {
        // GET: AdminPanel
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}