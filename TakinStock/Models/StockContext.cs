﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TakinStock.Models
{
    public class StockContext : ApplicationDbContext
    {
        public virtual DbSet<TakinStockUsers> User { get; set; }
        public virtual DbSet<Items> Items { get; set; }
    }
}