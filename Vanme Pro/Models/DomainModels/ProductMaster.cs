using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vanme_Pro.Models.DomainModels
{
    class ProductMaster
    {

        public int Id { get; set; }

        public string Name { get; set; }
        public int VendorCode { get; set; }
        public string StyleNumber { get; set; }
        public string SKU { get; set; }
        public string UPC { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string MadeIn { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Margin { get; set; }
        public int Inventory { get; set; }
        public int Income { get; set; }
        public int Outcome { get; set; }
        public int OnTheWayInventory { get; set; }
        public string Image { get; set; }
        public Nullable<bool> Active { get; set; }

        public string Note { get; set; }

        public ICollection<Item> Items { get; set; }

    }
}
