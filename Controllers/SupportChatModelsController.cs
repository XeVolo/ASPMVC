using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SystemyBazDanychP1.Models;

namespace SystemyBazDanychP1.Controllers
{
    public class SupportChatModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SupportChatModels
        public ActionResult Index()
        {
            var supportChatModels = db.SupportChats.Include(s => s.Admin).Include(s => s.User);
            return View(supportChatModels.ToList());
        }

        // GET: SupportChatModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupportChatModel supportChatModel = db.SupportChats.Find(id);
            if (supportChatModel == null)
            {
                return HttpNotFound();
            }
            return View(supportChatModel);
        }

        // GET: SupportChatModels/Create
        [Authorize]
        public ActionResult Create()
        {
            IdentityManager im = new IdentityManager();
            string CiD = HttpContext.User.Identity.Name;
            var usersId = db.Users.Where(u => u.UserName == CiD).ToList();
            var adminUsers = db.Users
            .Where(u => u.Roles.Any(ur => ur.RoleId == "d0d956b8-f0a1-4737-83b5-1b5f52c68010"))
            .ToList();
            string s = usersId[0].Id;
            var query = db.SupportChats.Where(a => a.ClientId.Equals(s)).ToList();
            if (query.Count != 0)
            {
                return RedirectToAction("Edit", new {id = query[0].Id });
                
            }
            
            ViewBag.AdminId = new SelectList(adminUsers, "Id", "Name");
            ViewBag.ClientId = new SelectList(usersId, "Id", "Name");
            return View();
        }

        // POST: SupportChatModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClientId,AdminId,Conversation")] SupportChatModel supportChatModel)
        {
            if (ModelState.IsValid)
            {
                db.SupportChats.Add(supportChatModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdminId = new SelectList(db.Users, "Id", "Name", supportChatModel.AdminId);
            ViewBag.ClientId = new SelectList(db.Users, "Id", "Name", supportChatModel.ClientId);
            return View(supportChatModel);
        }

        // GET: SupportChatModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //viewbag do ktorego zaladujesz konwersacje zamiast supportChatModel
            SupportChatModel supportChatModel = db.SupportChats.Find(id);
            if (supportChatModel == null)
            {
                return HttpNotFound();
            }
            MessageViewModel messageViewModel = new MessageViewModel();
            messageViewModel.Conversation = supportChatModel.Conversation;
            messageViewModel.Id = supportChatModel.Id;
            return View(messageViewModel);
        }

        // POST: SupportChatModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MessageViewModel messageViewModel)
        {
            var query = db.SupportChats.Find(messageViewModel.Id);
            query.Conversation += $"\n{messageViewModel.Message}";

            if (ModelState.IsValid)
            {
                db.Entry(query).State = EntityState.Modified;              
                db.SaveChanges();
                return RedirectToAction("Index");
            }        
            return View(messageViewModel);
        }

        // GET: SupportChatModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupportChatModel supportChatModel = db.SupportChats.Find(id);
            if (supportChatModel == null)
            {
                return HttpNotFound();
            }
            return View(supportChatModel);
        }

        // POST: SupportChatModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SupportChatModel supportChatModel = db.SupportChats.Find(id);
            db.SupportChats.Remove(supportChatModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
