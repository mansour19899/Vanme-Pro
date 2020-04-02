using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vanme_Pro.Models.DomainModels
{
    class Warehouse
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Note { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
