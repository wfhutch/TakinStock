using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TakinStock.Models;
using Microsoft.AspNet.Identity;


namespace TakinStock.Controllers
{
    public class ItemsController : Controller
    {
        //private StockContext db = new StockContext();

        public StockRepository Repo { get; set; }

        public ItemsController() : base()
        {
            Repo = new StockRepository();
        }


        // GET: Items
        [Authorize]
        public ActionResult Index()
        {
            string user_id = User.Identity.GetUserId();
            ApplicationUser real_user = Repo.Context.Users.FirstOrDefault(u => u.Id == user_id);
            Users me = null;
            try
            {
                me = Repo.GetAllUsers().Where(u => u.RealUser.Id == user_id).Single();

            }
            catch (Exception)
            {
                bool successful = Repo.AddNewUser(real_user);
            }
            List<Items> list_of_items = Repo.GetUserItems(me);
            return View(list_of_items);
        }

        // GET: Items/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Items items = Repo.Items.Find(id);
        //    if (items == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(items);
        //}

        // GET: Items/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemsID,Owner,Type,Make,Model,Description,SerialNumber,PurchasePrice,PurchaseDate,PurchasedFrom,Image")] Items items)
        {
            if (ModelState.IsValid)
            {
                Repo.Context.Items.Add(items);
                Repo.Context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(items);
        }

        // GET: Items/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Items items = db.Items.Find(id);
        //    if (items == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(items);
        //}

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ItemsID,Type,Make,Model,Description,SerialNumber,PurchasePrice,PurchaseDate,PurchasedFrom,Image,LostByDamage,Stolen")] Items items)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(items).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(items);
        //}

        // GET: Items/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Items items = db.Items.Find(id);
        //    if (items == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(items);
        //}

        // POST: Items/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Items items = db.Items.Find(id);
        //    db.Items.Remove(items);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
