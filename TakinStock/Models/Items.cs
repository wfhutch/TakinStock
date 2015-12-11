using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TakinStock.Models
{
    public class Items
    {
        [Key]
        public int ItemsID { get; set; }
        public string Type { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public string PurchasePrice { get; set; }
        public string PurchaseDate { get; set; }
        public string PurchasedFrom { get; set; }
        public string Image { get; set; }
        public bool LostByDamage { get; set; }
        public bool Stolen { get; set; }
    }
}