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
        public virtual Users Owner { get; set; }
        public string Type { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Description { get; set; }

        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }

        [Display(Name = "Purchase Price")]
        [DataType(DataType.Currency)]
        public decimal PurchasePrice { get; set; }

        [Display(Name = "Purchase Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PurchaseDate { get; set; }

        [Display(Name = "Purchased From")]
        public string PurchasedFrom { get; set; }
        public string Image { get; set; }
        public bool LostByDamage { get; set; }
        public bool Stolen { get; set; }
    }
}