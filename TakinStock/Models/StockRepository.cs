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
    }
}