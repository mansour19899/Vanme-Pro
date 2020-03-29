using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vanme_Pro.Models.DomainModels
{
    class PurchaseOrder
    {

        public int Id { get; set; }
        public int? ItemsCount { get; set; }
        public string PoNumber { get; set; }
        public int? Vendor_fk { get; set; }
        public string PoType { get; set; }
        public string ShipToStore { get; set; }
        public string Associate { get; set; }
        public string PoTerms { get; set; }
        public string Account { get; set; }
        public string FormSO { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? AsnDate { get; set; }
        public DateTime? GrnDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public DateTime? LastEditDate { get; set; }
        public decimal? Freight { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? DiscountDollers { get; set; }
        public decimal? FeeType { get; set; }
        public decimal? Fee { get; set; }
        public decimal? PoTotal { get; set; }
        public decimal? AsnTotal { get; set; }
        public  bool? CreatedPO { get; set; }
        public  bool? CreatedAsn { get; set; }
        public  bool? CreatedGrn { get; set; }

        public ICollection<Item> Items { get; set; }
        public Vendor Vendor { get; set; }
    }
}
