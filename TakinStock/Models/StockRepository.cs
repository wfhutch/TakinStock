﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public List<Users> GetAllUsers()
        {
            var query = from users in _context.User select users;
            return query.ToList();
        }

        public bool AddNewItem(Users user, Items expected)
        {
            bool is_added = true;
            try
            {
                user.Items.Add(expected);
                _context.SaveChanges();

            }
            catch (Exception)
            {
                is_added = false;
            }
            return is_added;
        }

        public bool AddNewItem(Users test_user, string type, string make, string model, string serialNumber, DateTime purchaseDate, string purchasedFrom, string image, bool damaged, bool stolen)
        {
            bool is_added = true;
            Items new_item = new Items
            {
                Owner = test_user,
                Type = type,
                Make = make,
                Model = model,
                SerialNumber = serialNumber,
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
    }
}