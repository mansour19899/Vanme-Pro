using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vanme_Pro
{
    class TempDatabase
    {
    }

    class ItemTemp
    {
        public string StyleNumber { get; set; }
        public string Vendor { get; set; }
        public string Size { get; set; }
        public int PO_QYT { get; set; }
        [Key]
        public int Id { get; set; }
        public decimal price { get; set; }
        public decimal Totalprice { get; set; }

        public ItemTemp()
        {
                
        }

    }
}
