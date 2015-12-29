﻿using System;
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
            if (Repo.GetAllUsers().Where(u => u.RealUser.Id == user_id).Count() < 1)
            {
                bool successful = Repo.AddNewUser(real_user);
            }
            else
            {
                me = Repo.GetAllUsers().Where(u => u.RealUser.Id == user_id).First();
            }

            List<Items> list_of_items = Repo.GetUserItems(me);
            return View(list_of_items);
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Items items = Repo.Context.Items.Find(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            return View(items);
        }

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
        public ActionResult Create([Bind(Include = "ItemsID,Type,Make,Model,Description,SerialNumber,PurchasePrice,PurchaseDate,PurchasedFrom,Image")] Items items, string Types)
        {
            string strDDLValue = Types;
            if (strDDLValue == "1")
            {
                strDDLValue = "Electronics";
            }
            else if (strDDLValue == "2")
            {
                strDDLValue = "Computers/Phones";
            }
            else
            {
                strDDLValue = "Instruments";
            }
            string user_id = User.Identity.GetUserId();
            ApplicationUser real_user = Repo.Context.Users.FirstOrDefault(u => u.Id == user_id);
            Users me = Repo.GetAllUsers().Where(u => u.RealUser.Id == user_id).First();


            if (ModelState.IsValid)
            {
                Repo.AddNewItem(me, items.ItemsID, strDDLValue, items.Make, items.Model, items.Description, items.SerialNumber,
                    items.PurchasePrice, items.PurchaseDate, items.PurchasedFrom, items.Image, items.LostByDamage, items.Stolen);
            }

            return RedirectToAction("Index");
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Items items = Repo.Context.Items.Find(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            return View(items);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemsID,Type,Make,Model,Description,SerialNumber,PurchasePrice,PurchaseDate,PurchasedFrom,Image,LostByDamage,Stolen")] Items items)
        {

            if (ModelState.IsValid)
            {
                Repo.Context.Entry(items).State = EntityState.Modified;
                Repo.Context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(items);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Items items = Repo.Context.Items.Find(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            return View(items);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Items items = Repo.Context.Items.Find(id);
            Repo.Context.Items.Remove(items);
            Repo.Context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Repo.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
