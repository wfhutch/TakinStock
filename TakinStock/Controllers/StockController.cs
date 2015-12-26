using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TakinStock.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace TakinStock.Controllers
{
    public class StockController : Controller
    {
        public StockRepository Repo { get; set; }

        public StockController() : base()
        {
            Repo = new StockRepository();
        }


        // GET: Stock
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

            //List<Items> my_items = Repo.GetAllItems();
            //return View(my_items);
            //ViewBag.Title = "My Items!";

            //List<string> my_items = new List<string>();
            //my_items.Add("Item 1");
            //my_items.Add("Item 2");
            //my_items.Add("Item 3");
            //my_items.Add("Item 4");
            //my_items.Add("Item 5");

            //return View(my_items);
        }

        // GET: Stock/AddNewItem
        [Authorize]
        public ActionResult AddNewItem()
        {
            return View();
        }

        // GET: Stock/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Stock/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stock/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Stock/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Stock/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Stock/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Stock/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
