using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Vanme_Pro.Models.DomainModels
{
    class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Lavel { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsVisitor { get; set; }
        public bool IsCashier { get; set; }
        //public bool IsCashier { get; set; }

        public ICollection<PurchaseOrder> CreatePo { get; set; }
        public ICollection<PurchaseOrder> CreateAsn { get; set; }
        public ICollection<PurchaseOrder> CreateGrn { get; set; }
        public ICollection<SaleOrder> SaleOrders { get; set; }
        public ICollection<Customer> Customers { get; set; }

    }
}
