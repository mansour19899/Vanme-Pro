using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanme_Pro.Models.DomainModels;

namespace Vanme_Pro.Models.ViewModels
{
    class CalculateMiscellaneous
    {
        public CalculateMiscellaneous()
        {

        }

        public CalculateMiscellaneous(PurchaseOrder pOrder)
        {
            _fright = pOrder.Freight == null ? 0 : pOrder.Freight.Value;
            _discountPercent = pOrder.DiscountPercent == null ? 0 : pOrder.DiscountPercent.Value;
            Percent =pOrder.Percent;
            _discountDollers = pOrder.DiscountDollers == null ? 0 : pOrder.DiscountDollers.Value;
            _insurance = pOrder.Insurance == null ? 0 : pOrder.Insurance.Value;
            _customsDuty = pOrder.CustomsDuty == null ? 0 : pOrder.CustomsDuty.Value;
            _handling = pOrder.Handling == null ? 0 : pOrder.Handling.Value;
            _forwarding = pOrder.Forwarding == null ? 0 : pOrder.Forwarding.Value;
            _landTransport = pOrder.LandTransport == null ? 0 : pOrder.LandTransport.Value;
            _others = pOrder.Others == null ? 0 : pOrder.Others.Value;

        }
        public decimal TotalAsnPrice { get; set; } = 0;
        private decimal _fright=0;

        public decimal Fright
        {
            get { return _fright; }
        }

        public string SetFright
        {
            set { _fright = Convert.ToDecimal(value); }
        } 

        private decimal _discountPercent = 0;

        public decimal DiscountPercent
        {
            get { return Math.Round(_discountPercent * TotalAsnPrice / 100, 2, MidpointRounding.ToEven); }
        }
        public string Percent { get; set; }
        public string SetDiscountPercent
        {
            set { _discountPercent = Convert.ToDecimal(value.Replace("-",""));
                Percent = value.Replace("-", "").ToString() + " %";
            }
            
        }

        private decimal _discountDollers = 0;

        public decimal DiscountDollers
        {
            get { return _discountDollers; }
        }

        public string SetDiscountDollers
        {
            set { _discountDollers = Convert.ToDecimal(value.Replace("-", "")); }
        }

        private decimal _insurance = 0;

        public decimal Insurance
        {
            get { return _insurance; }
        }

        public string SetInsurance
        {
            set { _insurance = Convert.ToDecimal(value); }
        }

        private decimal _customsDuty = 0;

        public decimal CustomsDuty
        {
            get { return _customsDuty; }
        }

        public string SetCustomsDuty
        {
            set { _customsDuty = Convert.ToDecimal(value); }
        }
        private decimal _handling = 0;

        public decimal Handling
        {
            get { return _handling; }
        }

        public string SetHandling
        {
            set { _handling = Convert.ToDecimal(value); }
        }
        private decimal _forwarding = 0;

        public decimal Forwarding
        {
            get { return _forwarding; }
        }

        public string SetForwarding
        {
            set { _forwarding = Convert.ToDecimal(value); }
        }
        private decimal _landTransport = 0;

        public decimal LandTransport
        {
            get { return _landTransport; }
        }

        public string SetLandTransport
        {
            set { _landTransport = Convert.ToDecimal(value); }
        }
        private decimal _others = 0;

        public decimal Others
        {
            get { return _others; }
        }

        public string SetOthers
        {
            set { _others = Convert.ToDecimal(value); }
        }


        public string getTotalString()
        {
            return "Total Charges:  " + GetTotal().ToString();
        }

        public decimal GetTotal()
        {
            return _fright-(_discountPercent * TotalAsnPrice / 100)-_discountDollers+_insurance+_customsDuty+_handling+_forwarding+_landTransport+_others;
        }

    }
}
