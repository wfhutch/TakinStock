using System;
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
            user.Items.Add(expected);
            bool is_added = true;
            try
            {
                Items added_item = _context.Items.Add(expected);
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