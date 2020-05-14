﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Vanme_Pro.Annotations;

namespace Vanme_Pro.Models.DomainModels
{
    class SaleOrder : INotifyPropertyChanged
    {
        public int Id { get; set; }
        //WholeSale Is True and Retail is False
        public bool Type { get; set; }
        public DateTime? OrderedDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public int? SalesOrderNumber { get; set; }
        public int? Cashier_fk { get; set; }
        public int? Customer_fk { get; set; }
        public int? Warehouse_fk { get; set; }
        public int? ShipMethod_fk { get; set; }

        private decimal _subtotal;

        public decimal Subtotal
        {
            get
            {
                if (IsSaveDatabase)
                    return _subtotal;
                else
                    return Math.Round(_subtotal, 2, MidpointRounding.ToEven);

            }
            set
            {
                _subtotal = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SubtotalwithServices));
                CalculateTotalPrice();
            }
        }

        private decimal _soTotalPrice;

        public decimal SoTotalPrice
        {
            get
            {
                if (IsSaveDatabase)
                    return _soTotalPrice;
                else
                    return Math.Round(_soTotalPrice, 2, MidpointRounding.ToEven);

            }
            set
            {
                _soTotalPrice = value;
                OnPropertyChanged();
            }
        }

        public int? TaxArea_fk { get; set; }


        private decimal _tax;

        public decimal Tax
        {
            get
            {
                if (IsSaveDatabase)
                    return _tax;
                else
                    return Math.Round(_tax, 2, MidpointRounding.ToEven);
            }
            set { _tax = value; }
        }


        private decimal _handling;

        public decimal Handling
        {
            get
            {
                if (IsSaveDatabase)
                    return _handling;
                else
                    return Math.Round(_handling, 2, MidpointRounding.ToEven);

            }
            set
            {
                _handling = value;
                _subtotalwithServices = _subtotal + value + _freight;
                OnPropertyChanged(nameof(SoTotalPrice));
                OnPropertyChanged(nameof(SubtotalwithServices));
                OnPropertyChanged();
                CalculateTotalPrice();
            }
        }


        private decimal _freight;

        public decimal Freight
        {
            get
            {
                if (IsSaveDatabase)
                    return _freight;
                else
                    return Math.Round(_freight, 2, MidpointRounding.ToEven);


            }
            set
            {
                _freight = value;
                _subtotalwithServices = _subtotal + value + _handling;
                OnPropertyChanged(nameof(SoTotalPrice));
                OnPropertyChanged(nameof(SubtotalwithServices));
                OnPropertyChanged();
                CalculateTotalPrice();
            }
        }

        private decimal _totalDiscount;

        public decimal TotalDiscount
        {
            get
            {
                if (IsSaveDatabase)
                    return _totalDiscount;
                else
                    return Math.Round(_totalDiscount, 2, MidpointRounding.ToEven);

            }
            set
            {
                _totalDiscount = value;
                OnPropertyChanged();
            }
        }

        public string ShipToAddressName { get; set; }
        public string ShipToAddressNam1 { get; set; }
        public string ShipToAddressNam2 { get; set; }
        public string ShipToPostalCode { get; set; }
        public string ShipToPostalPhone1 { get; set; }
        public int? Quantity { get; set; } = 0;


        public ICollection<SoItem> SoItems { get; set; }
        public User User { get; set; }

        private Customer _customer;
        public Customer Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                OnPropertyChanged();

            }
        }
        public Province TaxArea { get; set; }
        public Warehouse Warehouse { get; set; }

        //-------------------------Not Mapping----------------------------------
        public string TaxName { get; set; }
        private decimal _subtotalwithServices;

        public decimal SubtotalwithServices
        {
            get
            {
                if (IsSaveDatabase)
                    return _subtotalwithServices;
                else
                    return Math.Round(_subtotalwithServices, 2, MidpointRounding.ToEven);

            }
            set
            {
                _subtotalwithServices = value;
                OnPropertyChanged();
            }
        }

        private List<decimal> _taxRate;

        public List<decimal> TaxRate
        {
            get { return _taxRate; }
            set
            {
                _taxRate = value;
                CalculateTotalPrice();
            }
        }

        private void CalculateTotalPrice()
        {
            _tax = 0;
            _subtotalwithServices = _subtotal + _freight + _handling;
            foreach (var VARIABLE in TaxRate)
            {
                _tax = VARIABLE * _subtotalwithServices + _tax;
            }
            _soTotalPrice = _subtotalwithServices + _tax;
            OnPropertyChanged(nameof(Tax));
            OnPropertyChanged(nameof(SoTotalPrice));
        }

        public bool IsSaveDatabase { get; set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
