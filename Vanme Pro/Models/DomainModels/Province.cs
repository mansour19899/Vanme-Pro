using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vanme_Pro.Models.DomainModels
{
    class Province
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Hst { get; set; }
        public int? PSt { get; set; }
        public bool Active { get; set; }

        public ICollection<SaleOrder> SaleOrders { get; set; }
    }
}
