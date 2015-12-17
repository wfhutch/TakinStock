using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TakinStock.Models;

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
        public ActionResult Index()
        {
            ViewBag.Title = "My Items!";

            List<string> my_items = new List<string>();
            my_items.Add("Item 1");
            my_items.Add("Item 2");
            my_items.Add("Item 3");
            my_items.Add("Item 4");
            my_items.Add("Item 5");

            return View(my_items);
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
