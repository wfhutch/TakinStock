using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;


namespace TakinStock.Models
{
    public class StockRepository
    {
        private StockContext _context;
        public StockContext Context { get { return _context; } }

        public StockRepository()
        {
            _context = new StockContext();
        }

        public StockRepository(StockContext context)
        {
            _context = context;
        }

        public List<Items> GetAllItems()
        {
            var query = from items in _context.Items select items;
            return query.ToList();
        }

        public List<Items> GetUserItems(TakinStockUsers user)
        {
            if (user != null)
            {
                var query = from u in _context.User where u.UserID == user.UserID select u;
                var item_query = from i in _context.Items where i.Owner.UserID == user.UserID select i;
                List<Items> my_items = item_query.ToList();
                TakinStockUsers found_user = query.SingleOrDefault<TakinStockUsers>();
                if (found_user == null)
                {
                    return new List<Items>();
                }
                if (my_items == null)
                {
                    return new List<Items>();
                }
                else
                {
                    return my_items;
                }
            }
            else
            {
                return new List<Items>();
            }
        }

        public List<TakinStockUsers> GetAllUsers()
        {
            var query = from users in _context.User select users;
            return query.ToList();
        }

        public bool AddNewItem(TakinStockUsers me, int id, string type, string make, string model, string description, string serialNumber, decimal purchasePrice, 
            DateTime purchaseDate, string purchasedFrom, string image, bool damaged, bool stolen)
        {

            bool is_added = true;
            Items new_item = new Items
            {
                Owner = me,
                ItemsID = id,
                Type = type,
                Make = make,
                Model = model,
                Description = description,
                SerialNumber = serialNumber,
                PurchasePrice = purchasePrice,
                PurchaseDate = purchaseDate,
                PurchasedFrom = purchasedFrom,
                Image = image,
                LostByDamage = damaged,
                Stolen = stolen
            };

            try
            {
                Items added_item = _context.Items.Add(new_item);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                is_added = false;
            }
            return is_added;
        }

        public List<Items> SearchByType(string search_string)
        {
            var query = from type in _context.Items select type;
            List<Items> found_items = query.Where(item => item.Type.Contains(search_string)).ToList();
            return found_items;
        }

        public List<Items> SearchByMake(string search_string)
        {
            var query = from make in _context.Items select make;
            List<Items> found_items = query.Where(make => make.Make.Contains(search_string)).ToList();
            return found_items;
        }

        public List<Items> SearchByDate(DateTime search_date)
        {
            var query = from date in _context.Items select date;
            List<Items> found_items = query.Where(date => date.PurchaseDate.Equals(search_date)).ToList();
            return found_items;
        }

        public bool AddNewUser(ApplicationUser user)
        {
            TakinStockUsers new_user = new TakinStockUsers { RealUser = user, Email = user.Email };
            bool is_added = true;
            try
            {
                TakinStockUsers added_user = _context.User.Add(new_user);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                is_added = false;
            }
            return is_added;
        }
    }
}