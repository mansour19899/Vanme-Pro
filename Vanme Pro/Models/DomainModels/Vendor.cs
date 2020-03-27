using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vanme_Pro.Models.DomainModels
{
    class Vendor
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public  string Name { get; set; }
        public  string Code { get; set; }

    }
}
