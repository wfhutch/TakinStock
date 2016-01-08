using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TakinStock.Models
{
    public class TakinStockUsers
    {
        [Key]
        public int UserID { get; set; }
        public virtual ApplicationUser RealUser { get; set; }
        public string Email { get; set; }
        public List<Items> Items { get; set; }
    }
}