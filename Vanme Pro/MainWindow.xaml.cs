using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using Vanme_Pro.Models.Context;
using Vanme_Pro.Models.DomainModels;

namespace Vanme_Pro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<ItemTemp> GrdItemsTemp;
        private List<Item> itemsList;
        private List<Item> AddNewitemsList;
        private dbContext db;
        private int StepAddItem = 1;
        decimal SumPrice = 0;
        private int ItemsCountInPO = 0;
        private ReceiptStatus Mode = ReceiptStatus.nothing;
        private State state = State.Save;
        private PurchaseOrder SelectedPurchaseOrder;
        private List<Item> RemoveItemsList=new List<Item>();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            PurchaseOrder PO = new PurchaseOrder() { Id = 1234 };
            txtPoNumber.Text = "1234";
            db = new dbContext();

            itemsList = new List<Item>();

            var productlList = db.Products.ToList();
            lvProducts.ItemsSource = productlList;



            var tre = db.Vendors.ToList();
            cmbVendor.ItemsSource = tre;

            DataGridItems.ItemsSource = null;
            // GrdItemsTemp = GrdListItem.ItemsSource.Cast<ItemTemp>().ToList();
        }

        private void lvProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            var wer = (ProductMaster)lvProducts.ItemContainerGenerator.ItemFromContainer(dep);

            if (state == State.Save)

            {
                itemsList.Add(new Item() { Id = StepAddItem, ProductMaster = wer, PoPrice = wer.Price.Value, PoQuantity = 0, PoItemsPrice = 0 });
                DataGridItems.ItemsSource = null;
                DataGridItems.ItemsSource = itemsList;
            }
              
            else
            {
                AddNewitemsList.Add(new Item() { Id = StepAddItem, ProductMaster = wer, PoPrice = wer.Price.Value, PoQuantity = 0, PoItemsPrice = 0 });
                DataGridItems.ItemsSource = null;
                DataGridItems.ItemsSource = itemsList.Concat(AddNewitemsList).ToList();

            }


            GrdProductList.Visibility = Visibility.Hidden;
            GrdNewPurchersOrder.Visibility = Visibility.Visible;
            StepAddItem++;
        }

        private void GrdListItem_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            List<Item> Items;

                Items = itemsList.Concat(AddNewitemsList).ToList();

            int x = 0;
            DataGridColumn col1 = e.Column;
            DataGridRow row1 = e.Row;
            Item t = e.Row.DataContext as Item;

            int row_index = ((DataGrid)sender).ItemContainerGenerator.IndexFromContainer(row1);
            int col_index = col1.DisplayIndex;

            var SelectItem = Items.FirstOrDefault(p => p.Id == t.Id);
            string header = col1.Header as string;
            if (header.CompareTo("Price") == 0)
            {
                var Price = Convert.ToDecimal(((TextBox)e.EditingElement).Text);
                SelectItem.PoItemsPrice = Convert.ToDecimal(t.PoQuantity * Price);
                SelectItem.PoPrice = Price;
            }
            else
            {
                var QyT = Convert.ToInt32(((TextBox)e.EditingElement).Text);
                SelectItem.PoItemsPrice = Convert.ToDecimal(t.PoPrice * QyT);
                SelectItem.PoQuantity = QyT;
            }

            DataGridItems.ItemsSource = null;
            DataGridItems.ItemsSource = Items;

         

            //SumPrice = 0;
            //ItemsCountInPO = 0;

            //foreach (Item item in Items)
            //{
            //    SumPrice = item.PoItemsPrice + SumPrice;
            //    ItemsCountInPO = item.PoQuantity + ItemsCountInPO;
            //}
            CalculateTotalPriceItemsCount(Items);

            TxtTotalPrice.Text = SumPrice.ToString();



        }

        void CalculateTotalPriceItemsCount(List<Item> items)
        {
            SumPrice = 0;
            ItemsCountInPO = 0;

            foreach (Item item in items)
            {
                SumPrice = item.PoItemsPrice + SumPrice;
                ItemsCountInPO = item.PoQuantity + ItemsCountInPO;
            }
            TxtTotalPrice.Text = SumPrice.ToString();
        }


        private void GrdListItem_Sorting(object sender, DataGridSortingEventArgs e)
        {
            //GrdItemsTemp = GrdListItem.ItemsSource.Cast<ItemTemp>().ToList();
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lvProducts.ItemsSource = db.Products.ToList();
            GrdNewPurchersOrder.Visibility = Visibility.Hidden;
            GrdProductList.Visibility = Visibility.Visible;

        }

        private void LvPurchase_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            SelectedPurchaseOrder = (PurchaseOrder)lvPurchase.ItemContainerGenerator.ItemFromContainer(dep);
            FillPerchaseOrderPage(SelectedPurchaseOrder);
        }

        private void BtnPurchaseOrder_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Mode = ReceiptStatus.PO;

            lvPurchase.ItemsSource = db.PurchaseOrders.Include(p => p.Items).Include(p => p.Vendor).ToList();
            GrdNewPurchersOrder.Visibility = Visibility.Hidden;
            GrdPurchesOrderList.Visibility = Visibility.Visible;
        }

        private void FillPerchaseOrderPage(PurchaseOrder pOrder)
        {
            AddNewitemsList=new List<Item>();
            itemsList.Clear();
            if(pOrder.Items!=null)
            itemsList = pOrder.Items.ToList();
            txtPoNumber.Text = pOrder.PoNumber.ShowPoNumber();
            cmbVendor.SelectedValue = pOrder.Vendor_fk;
            DataGridItems.ItemsSource = null;
            DataGridItems.ItemsSource = itemsList;
            TxtTotalPrice.Text = pOrder.PoTotal.ToString();
            dpiOrderDate.SelectedDate = pOrder.OrderDate;
            dpiShipDate.SelectedDate = pOrder.ShipDate;
            dpiCancelDate.SelectedDate = pOrder.CancelDate;


            if (pOrder.Id == 0)
            {
                state = State.Save;
                lblSave.Content = "Save";
            }
            else
            {
                state = State.Update;
                lblSave.Content = "Update";
            }
               

            GrdPurchesOrderList.Visibility = Visibility.Hidden;
            GrdNewPurchersOrder.Visibility = Visibility.Visible;
        }

        private void BtnNewPurchaseOrder_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            FillPerchaseOrderPage(new PurchaseOrder());
            GrdPurchesOrderList.Visibility = Visibility.Hidden;
            GrdNewPurchersOrder.Visibility = Visibility.Visible;

        }

        private void BtnAsnVorchers_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Mode = ReceiptStatus.Asn;
            lvPurchase.ItemsSource = db.PurchaseOrders.Where(p => p.CreatedPO == true)
                .Include(p => p.Items).Include(p => p.Vendor).ToList();
            GrdNewPurchersOrder.Visibility = Visibility.Hidden;
            GrdPurchesOrderList.Visibility = Visibility.Visible;

        }

        private void BtnGrn_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Mode = ReceiptStatus.Grn;
            lvPurchase.ItemsSource = db.PurchaseOrders.Where(p => p.CreatedAsn == true)
                .Include(p => p.Items).Include(p => p.Vendor).ToList();
            GrdNewPurchersOrder.Visibility = Visibility.Hidden;
            GrdPurchesOrderList.Visibility = Visibility.Visible;
        }

        private void BtnSave_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            int vendorfk = 0;
            bool SaveMode = true;
            if (state == State.Update)
            {
                SaveMode = false;
            }
            PurchaseOrder Po = new PurchaseOrder();
            if (Mode == ReceiptStatus.PO)
            {
                if (SaveMode)
                {
                    Po.Id = db.PurchaseOrders.Max(p => p.Id) + 1;
                    Po.PoNumber = db.PurchaseOrders.Max(p => p.PoNumber) + 1;
                     vendorfk = Convert.ToInt32(cmbVendor.SelectedValue);
                    if (vendorfk != 0)
                        Po.Vendor_fk = vendorfk;
                    Po.OrderDate = dpiOrderDate.SelectedDate;
                    Po.ShipDate = dpiShipDate.SelectedDate;
                    Po.CancelDate = dpiCancelDate.SelectedDate;
                    Po.LastEditDate = DateTime.Now;
                    if (!string.IsNullOrWhiteSpace(TxtTotalPrice.Text))
                        Po.PoTotal = Convert.ToDecimal(TxtTotalPrice.Text);
                    if (ItemsCountInPO != 0)
                        Po.ItemsPoCount = ItemsCountInPO;
                    Po.CreatedPO = true;
                    db.PurchaseOrders.Add(Po);
                    db.SaveChanges();

                }
                else
                {
                    Po = SelectedPurchaseOrder;
                      vendorfk = Convert.ToInt32(cmbVendor.SelectedValue);
                    if (vendorfk != 0)
                        Po.Vendor_fk = vendorfk;
                    Po.OrderDate = dpiOrderDate.SelectedDate;
                    Po.ShipDate = dpiShipDate.SelectedDate;
                    Po.CancelDate = dpiCancelDate.SelectedDate;
                    Po.LastEditDate = DateTime.Now;
                    if (!string.IsNullOrWhiteSpace(TxtTotalPrice.Text))
                        Po.PoTotal = Convert.ToDecimal(TxtTotalPrice.Text);
                    if (ItemsCountInPO != 0)
                        Po.ItemsPoCount = ItemsCountInPO;
                    db.PurchaseOrders.Update(Po);
                    db.SaveChanges();
                }

                

                int lastId = db.Items.Max(p => p.Id);
                foreach (Item VARIABLE in itemsList)
                {

                    if (SaveMode)
                    {
                        lastId++;
                        VARIABLE.Id = lastId;
                        VARIABLE.Po_fk = Po.Id;
                        VARIABLE.ProductMaster_fk = VARIABLE.ProductMaster.Id;
                        VARIABLE.ProductMaster = null;
                        db.Items.Add(VARIABLE);
                    }
                    else
                    {
                        db.Items.Update(VARIABLE);
                    }
                    
                }

                if (AddNewitemsList.Count > 0)
                {
                    lastId = db.Items.Max(p => p.Id);
                    foreach (var VARIABLE in AddNewitemsList)
                    {
                        lastId++;
                        VARIABLE.Id = lastId;
                        VARIABLE.Po_fk = Po.Id;
                        VARIABLE.ProductMaster_fk = VARIABLE.ProductMaster.Id;
                        VARIABLE.ProductMaster = null;
                        db.Items.Add(VARIABLE);
                    }

                }

                if (RemoveItemsList.Count > 0)
                {
                    foreach (var VARIABLE in RemoveItemsList)
                    {
                        db.Items.Remove(VARIABLE);
                        //Po.Items.FirstOrDefault(p => p.Id == VARIABLE.Id).Note = "Remove From:  "+Po.PoNumber.ShowPoNumber();
                        //Po.Items.FirstOrDefault(p => p.Id == VARIABLE.Id).Po_fk=1;


                    }
                }
                AddNewitemsList.Clear();
                RemoveItemsList.Clear();
                db.SaveChanges();

            }

        }

        private void BtnRemove_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            RemoveItemsList.Clear();
            RemoveItemsList = DataGridItems.SelectedItems.Cast<Item>().ToList(); ;

            foreach (Item VARIABLE in RemoveItemsList)
            {
                itemsList.Remove(VARIABLE);
                AddNewitemsList.Remove(VARIABLE);

            }
            List<Item> t= itemsList.Concat(AddNewitemsList).ToList();
            CalculateTotalPriceItemsCount(t);
            DataGridItems.ItemsSource = t;
        }

        private void BtnLogo_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("salam");
            SnackbarMessageQueue myMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(4000));
            SnackbarResult.MessageQueue = myMessageQueue;
            myMessageQueue.Enqueue("Wow, easy!");
        }
    }
}
