using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vanme_Pro.Models.DomainModels
{
    class SoItem
    {
        public int Id { get; set; }
        public int So_fk { get; set; }
        public int ProductMaster_fk { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public  string Discount { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }

        public SaleOrder SaleOrder { get; set; }
        public ProductMaster ProductMaster { get; set; }
    }
}
