using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vanme_Pro.Models.DomainModels
{
    class Receipt
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int ReceiptNumber { get; set; }
        public int SalesOrderNumber { get; set; }
        public int Cashier_fk { get; set; }
        public int Customer_fk { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TotalReceipt { get; set; }
        public decimal TotalTaxDollar { get; set; }
        public int DiscountPercent { get; set; }
        public int FeeName { get; set; }

    }
}
