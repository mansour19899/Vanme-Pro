using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Vanme_Pro.Models.DomainModels
{
    class SaleOrder
    {
        public int Id { get; set; }
        //WholeSale Is True and Retail is False
        public bool Type { get; set; }
        public DateTime? OrderedDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public int? SalesOrderNumber { get; set; }
        public int? Cashier_fk { get; set; }
        public int? Customer_fk { get; set; }
        public int? ShipMethod_fk { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? SoTotal { get; set; }
        public int?  TaxArea_fk  { get; set; }
        public string Tax { get; set; }
        public decimal? Handling { get; set; }
        public decimal? Freight { get; set; }
        public string ShipToAddressName { get; set; }
        public string ShipToAddressNam1 { get; set; }
        public string ShipToAddressNam2 { get; set; }
        public string ShipToPostalCode { get; set; }
        public string ShipToPostalPhone1 { get; set; }
        public int? Quantity { get; set; }


        public ICollection<SoItem> SoItems { get; set; }
        public User User { get; set; }
        public Customer Customer { get; set; }
        public Province TaxArea { get; set; }


    }
}
