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
        private ReceiptStatus Mode = ReceiptStatus.PO;
        private State state = State.Save;
        private PurchaseOrder SelectedPurchaseOrder;
        private List<Item> RemoveItemsList = new List<Item>();
        private SnackbarMessageQueue myMessageQueue;
        private bool IsViewDetail = false;
        private bool IsDone = false;
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

            myMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(4000));
            SnackbarResult.MessageQueue = myMessageQueue;
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

                FillDataGrid(itemsList);
            }

            else
            {
                AddNewitemsList.Add(new Item() { Id = StepAddItem, ProductMaster = wer, PoPrice = wer.Price.Value, PoQuantity = 0, PoItemsPrice = 0 });
                FillDataGrid(itemsList.Concat(AddNewitemsList).ToList());

            }


            GrdProductList.Visibility = Visibility.Hidden;
            GrdNewPurchersOrder.Visibility = Visibility.Visible;
            StepAddItem++;
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
            state = State.Update;
            lblSave.Content = "Update";
            FillPerchaseOrderPage(SelectedPurchaseOrder);
        }
        private void GrdListItem_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            string message = "You can not Edit After Done";
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


            if (Mode == ReceiptStatus.PO)
            {
                if (SelectedPurchaseOrder.CreatedPO == true || SelectedPurchaseOrder.CreatedAsn == true)
                {
                    myMessageQueue.Enqueue(message);
                }
                else
                {
                    if (header.CompareTo("Price") == 0)
                    {
                        var Price = Convert.ToDecimal(((TextBox)e.EditingElement).Text);
                        SelectItem.PoItemsPrice = Convert.ToDecimal(t.CurrentQuantity * Price);
                        SelectItem.PoPrice = Price;
                    }
                    else
                    {
                        var QyT = Convert.ToInt32(((TextBox)e.EditingElement).Text);
                        SelectItem.PoItemsPrice = Convert.ToDecimal(t.PoPrice * QyT);
                        SelectItem.PoQuantity = QyT;
                    }
                }


            }
            else if (Mode == ReceiptStatus.Asn)
            {
                if (SelectedPurchaseOrder.CreatedAsn == true)
                {
                    myMessageQueue.Enqueue(message);
                }
                else
                {
                    if (header.CompareTo("Price") == 0)
                    {
                        var Price = Convert.ToDecimal(((TextBox)e.EditingElement).Text);
                        SelectItem.AsnItemsPrice = Convert.ToDecimal(t.CurrentQuantity * Price);
                        SelectItem.AsnPrice = Price;
                    }
                    else
                    {
                        var QyT = Convert.ToInt32(((TextBox)e.EditingElement).Text);
                        SelectItem.AsnItemsPrice = Convert.ToDecimal(t.PoPrice * QyT);
                        SelectItem.AsnQuantity = QyT;
                    }
                }



            }
            else if (Mode == ReceiptStatus.Grn)
            {
                if (SelectedPurchaseOrder.CreatedGrn == true)
                {
                    myMessageQueue.Enqueue(message);
                }
                else
                {
                    if (header.CompareTo("Price") == 0)
                    {
                    }
                    else
                    {
                        var QyT = Convert.ToInt32(((TextBox)e.EditingElement).Text);
                        SelectItem.GrnQuantity = QyT;
                        SelectItem.Diffrent = SelectItem.AsnQuantity - SelectItem.GrnQuantity;
                        if (SelectItem.Diffrent.Value > 0)
                            SelectItem.Checked = true;

                    }
                }


            }
            else
            {

            }
            CalculateTotalPriceItemsCount(Items);



            FillDataGrid(Items, false);

        }

        private void CalculateTotalPriceItemsCount(List<Item> items)
        {
            SumPrice = 0;
            ItemsCountInPO = 0;
            if (Mode == ReceiptStatus.PO)
            {
                foreach (Item item in items)
                {
                    SumPrice = item.PoItemsPrice + SumPrice;
                    ItemsCountInPO = item.PoQuantity + ItemsCountInPO;
                }
                TxtTotalPrice.Text = SumPrice.ToString();
            }
            else if (Mode == ReceiptStatus.Asn)
            {
                foreach (Item item in items)
                {
                    SumPrice = item.AsnItemsPrice + SumPrice;
                    ItemsCountInPO = item.AsnQuantity + ItemsCountInPO;
                }
                TxtTotalPrice.Text = SumPrice.ToString();
            }


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

        private void FillPerchaseOrderPage(PurchaseOrder pOrder)
        {

            AddNewitemsList = new List<Item>();
            itemsList.Clear();
            if (pOrder.Items != null)
                itemsList = pOrder.Items.ToList();

            if (Mode == ReceiptStatus.PO)
            {
                txtPoNumber.Text = pOrder.PoNumber.ShowPoNumber();
                cmbVendor.SelectedValue = pOrder.Vendor_fk;
                TxtTotalPrice.Text = pOrder.PoTotal.ToString();
                dpiOrderDate.SelectedDate = pOrder.OrderDate;
                dpiShipDate.SelectedDate = pOrder.ShipDate;
                dpiCancelDate.SelectedDate = pOrder.CancelDate;
                if (pOrder.LastEditDate != null)
                    txtLastEdit.Text = pOrder.LastEditDate.Value.ToLongDateString();
                lblOrderDate.Content = "Order Date ";
                lblPoTotal.Content = "PO Total";
                lblNumber.Content = "PO # ";
                FillDataGrid(itemsList);

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

                btnNewPurchaseOrder.Visibility = Visibility.Visible;
                btnAddItem.Visibility = Visibility.Visible;
                btnSave.Visibility = Visibility.Visible;
                btnRemove.Visibility = Visibility.Visible;
                btnDone.Visibility = Visibility.Visible;
            }
            else if (Mode == ReceiptStatus.Asn)
            {
                txtPoNumber.Text = pOrder.Asnumber.ShowAsnNumber();
                cmbVendor.SelectedValue = pOrder.Vendor_fk;
                TxtTotalPrice.Text = pOrder.AsnTotal.ToString();
                dpiOrderDate.SelectedDate = pOrder.AsnDate;
                dpiShipDate.SelectedDate = pOrder.ShipDate;
                dpiCancelDate.SelectedDate = pOrder.CancelDate;
                lblPoTotal.Content = "GIT Total";
                lblOrderDate.Content = "GIT Date ";
                if (pOrder.LastEditDate != null)
                    txtLastEdit.Text = pOrder.LastEditDate.Value.ToLongDateString();
                lblNumber.Content = "GIT # ";
                FillDataGrid(itemsList);

                btnNewPurchaseOrder.Visibility = Visibility.Visible;
                btnAddItem.Visibility = Visibility.Visible;
                btnSave.Visibility = Visibility.Visible;
                btnRemove.Visibility = Visibility.Visible;
                btnDone.Visibility = Visibility.Visible;
            }
            else if (Mode == ReceiptStatus.Grn)
            {
                txtPoNumber.Text = pOrder.Grnumber.ShowGrnNumber();
                cmbVendor.SelectedValue = pOrder.Vendor_fk;
                TxtTotalPrice.Text = pOrder.AsnTotal.ToString();
                dpiOrderDate.SelectedDate = pOrder.GrnDate;
                dpiShipDate.SelectedDate = pOrder.ShipDate;
                dpiCancelDate.SelectedDate = pOrder.CancelDate;
                lblPoTotal.Content = "GIT Total";
                lblOrderDate.Content = "GRN Date ";
                if (pOrder.LastEditDate != null)
                    txtLastEdit.Text = pOrder.LastEditDate.Value.ToLongDateString();
                lblNumber.Content = "GRN # ";
                FillDataGrid(itemsList);

                btnNewPurchaseOrder.Visibility = Visibility.Collapsed;
                btnAddItem.Visibility = Visibility.Collapsed;
                btnSave.Visibility = Visibility.Visible;
                btnRemove.Visibility = Visibility.Collapsed;
                btnDone.Visibility = Visibility.Visible;
            }
            else
            {

            }





            IsViewDetail = true;
            GrdPurchesOrderList.Visibility = Visibility.Hidden;
            GrdNewPurchersOrder.Visibility = Visibility.Visible;
        }

        void FillDataGrid(List<Item> list, bool Pre = true)
        {
            if (Mode == ReceiptStatus.PO)
            {
                DataGridItems.Columns[5].Header = "Previous Quantity";
                DataGridItems.Columns[6].Header = "Quantity ( PO )";
                foreach (var VARIABLE in list)
                {
                    if (Pre)
                        VARIABLE.PreviousQuantity = VARIABLE.PoQuantity;
                    VARIABLE.CurrentQuantity = VARIABLE.PoQuantity;
                    VARIABLE.TotalItemPrice = VARIABLE.PoItemsPrice;
                }
            }
            else if (Mode == ReceiptStatus.Asn)
            {
                DataGridItems.Columns[5].Header = "Quantity ( PO )";
                DataGridItems.Columns[6].Header = "Quantity ( GIT )";

                foreach (var VARIABLE in list)
                {

                    VARIABLE.PreviousQuantity = VARIABLE.PoQuantity;
                    VARIABLE.CurrentQuantity = VARIABLE.AsnQuantity;
                    VARIABLE.TotalItemPrice = VARIABLE.AsnItemsPrice;
                }
            }
            else if (Mode == ReceiptStatus.Grn)
            {
                DataGridItems.Columns[5].Header = "Quantity ( GIT )";
                DataGridItems.Columns[6].Header = "Quantity ( GRN )";

                foreach (var VARIABLE in list)
                {

                    VARIABLE.PreviousQuantity = VARIABLE.AsnQuantity;
                    VARIABLE.CurrentQuantity = VARIABLE.GrnQuantity;
                    VARIABLE.TotalItemPrice = VARIABLE.AsnItemsPrice;
                }
            }
            else
            {

            }

            DataGridItems.ItemsSource = null;
            DataGridItems.ItemsSource = list;
        }
        private void BtnNewPurchaseOrder_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            state = State.Save;
            lblSave.Content = "Save";
            SelectedPurchaseOrder = new PurchaseOrder() { CreatedPO = false, CreatedAsn = false, CreatedGrn = false };
            FillPerchaseOrderPage(new PurchaseOrder());
            GrdPurchesOrderList.Visibility = Visibility.Hidden;
            GrdNewPurchersOrder.Visibility = Visibility.Visible;

        }
        private void BtnPurchaseOrder_OnMouseDown(object sender, MouseButtonEventArgs e)
        {

            if (IsViewDetail)
            {
                if (Mode == ReceiptStatus.PO)
                {

                    lvPurchase.ItemsSource = db.PurchaseOrders.Include(p => p.Items).Include(p => p.Vendor).ToList();
                    GrdNewPurchersOrder.Visibility = Visibility.Hidden;
                    GrdPurchesOrderList.Visibility = Visibility.Visible;
                    SetSideBar();
                    IsViewDetail = false;
                }
                else if (Mode == ReceiptStatus.Asn)
                {
                    Mode = ReceiptStatus.PO;
                    lblNewPo.Content = "New PO";
                    FillPerchaseOrderPage(SelectedPurchaseOrder);
                }
                else if (Mode == ReceiptStatus.Grn)
                {
                    Mode = ReceiptStatus.PO;
                    lblNewPo.Content = "New PO";
                    FillPerchaseOrderPage(SelectedPurchaseOrder);
                }
                else
                {

                }
            }
            else
            {
                Mode = ReceiptStatus.PO;
                lblNewPo.Content = "New PO";
                lvPurchase.ItemsSource = db.PurchaseOrders.Include(p => p.Items).Include(p => p.Vendor).ToList();
                GrdNewPurchersOrder.Visibility = Visibility.Hidden;
                GrdPurchesOrderList.Visibility = Visibility.Visible;
                SetSideBar();

            }

            void SetSideBar()
            {
                btnNewPurchaseOrder.Visibility = Visibility.Visible;
                btnAddItem.Visibility = Visibility.Collapsed;
                btnSave.Visibility = Visibility.Collapsed;
                btnRemove.Visibility = Visibility.Collapsed;
                btnDone.Visibility = Visibility.Collapsed;
            }

        }


        private void BtnAsnVorchers_OnMouseDown(object sender, MouseButtonEventArgs e)
        {

            if (IsViewDetail)
            {
                if (Mode == ReceiptStatus.Asn)
                {

                    lvPurchase.ItemsSource = db.PurchaseOrders.Where(p => p.CreatedPO == true)
                        .Include(p => p.Items).Include(p => p.Vendor).ToList();
                    GrdNewPurchersOrder.Visibility = Visibility.Hidden;
                    GrdPurchesOrderList.Visibility = Visibility.Visible;
                    IsViewDetail = false;
                    SetSideBar();
                }
                else if (Mode == ReceiptStatus.PO)
                {
                    Mode = ReceiptStatus.Asn;
                    lblNewPo.Content = "New GIT";
                    FillPerchaseOrderPage(SelectedPurchaseOrder);
                }
                else if (Mode == ReceiptStatus.Grn)
                {
                    Mode = ReceiptStatus.Asn;
                    lblNewPo.Content = "New GIT";
                    FillPerchaseOrderPage(SelectedPurchaseOrder);
                }
                else
                {

                }
            }
            else
            {
                Mode = ReceiptStatus.Asn;
                lblNewPo.Content = "New GIT";

                lvPurchase.ItemsSource = db.PurchaseOrders.Where(p => p.CreatedPO == true)
                    .Include(p => p.Items).Include(p => p.Vendor).ToList();
                GrdNewPurchersOrder.Visibility = Visibility.Hidden;
                GrdPurchesOrderList.Visibility = Visibility.Visible;
                SetSideBar();
            }

            void SetSideBar()
            {
                btnNewPurchaseOrder.Visibility = Visibility.Visible;
                btnAddItem.Visibility = Visibility.Collapsed;
                btnSave.Visibility = Visibility.Collapsed;
                btnRemove.Visibility = Visibility.Collapsed;
                btnDone.Visibility = Visibility.Collapsed;
            }

        }

        private void BtnGrn_OnMouseDown(object sender, MouseButtonEventArgs e)
        {


            if (IsViewDetail)
            {
                if (Mode == ReceiptStatus.Grn)
                {

                    lvPurchase.ItemsSource = db.PurchaseOrders.Where(p => p.CreatedAsn == true)
                        .Include(p => p.Items).Include(p => p.Vendor).ToList();
                    GrdNewPurchersOrder.Visibility = Visibility.Hidden;
                    GrdPurchesOrderList.Visibility = Visibility.Visible;
                    IsViewDetail = false;
                    SetSideBar();
                }
                else if (Mode == ReceiptStatus.PO)
                {
                    Mode = ReceiptStatus.Grn;
                    FillPerchaseOrderPage(SelectedPurchaseOrder);
                }
                else if (Mode == ReceiptStatus.Asn)
                {
                    Mode = ReceiptStatus.Grn;
                    FillPerchaseOrderPage(SelectedPurchaseOrder);
                }
                else
                {

                }

            }
            else
            {
                Mode = ReceiptStatus.Grn;

                lvPurchase.ItemsSource = db.PurchaseOrders.Where(p => p.CreatedAsn == true)
                    .Include(p => p.Items).Include(p => p.Vendor).ToList();
                GrdNewPurchersOrder.Visibility = Visibility.Hidden;
                GrdPurchesOrderList.Visibility = Visibility.Visible;
                SetSideBar();

            }
            void SetSideBar()
            {
                btnNewPurchaseOrder.Visibility = Visibility.Collapsed;
                btnAddItem.Visibility = Visibility.Collapsed;
                btnSave.Visibility = Visibility.Collapsed;
                btnRemove.Visibility = Visibility.Collapsed;
                btnDone.Visibility = Visibility.Collapsed;
            }

        }

        private void BtnSave_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SaveAndUpdate(false);

        }

        private void BtnRemove_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            RemoveItemsList.Clear();
            RemoveItemsList = DataGridItems.SelectedItems.Cast<Item>().ToList();
            List<Item> tempRemove = RemoveItemsList.ToList();
            bool tt = false;
            foreach (Item VARIABLE in tempRemove)
            {
                itemsList.Remove(VARIABLE);
                tt = AddNewitemsList.Remove(VARIABLE);
                if (tt)
                    RemoveItemsList.Remove(VARIABLE);

            }
            List<Item> t = itemsList.Concat(AddNewitemsList).ToList();
            CalculateTotalPriceItemsCount(t);
            DataGridItems.ItemsSource = t;
        }

        private void BtnLogo_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            DataGridItems.Columns[0].Header = "Mansour";
            myMessageQueue.Enqueue("Wow, easy!");
        }

        private void BtnDone_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SaveAndUpdate(true);

        }

        void SaveAndUpdate(bool done)
        {
            string message = "";
            int vendorfk = 0;
            bool SaveMode = true;
            bool Save = false;

            if (state == State.Update)
            {
                SaveMode = false;
            }
            PurchaseOrder Po = new PurchaseOrder();
            if (Mode == ReceiptStatus.PO)
            {

                if (state == State.Save)
                {
                    Po.Id = db.PurchaseOrders.Max(p => p.Id) + 1;
                    Po.CreateOrder = DateTime.Now;
                    Save = true;
                }
                else
                {
                    Po = SelectedPurchaseOrder;
                }

                if (Po.CreatedPO == true || Po.CreatedAsn == true)
                {
                    MessageBox.Show("You Can not change after Done");
                }
                else
                {
                    if (Po.PoNumber == 0 && done)
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
                    if (Save)
                    {
                        db.PurchaseOrders.Add(Po);
                        db.SaveChanges();
                        int lastId = db.Items.Max(p => p.Id);
                        foreach (Item VARIABLE in itemsList)
                        {
                            lastId++;
                            VARIABLE.Id = lastId;
                            VARIABLE.Po_fk = Po.Id;
                            VARIABLE.ProductMaster_fk = VARIABLE.ProductMaster.Id;
                            VARIABLE.ProductMaster = null;
                            db.Items.Add(VARIABLE);

                        }
                    }

                    else
                    {
                        db.PurchaseOrders.Update(Po);
                        db.SaveChanges();
                        foreach (Item VARIABLE in itemsList)
                        {

                            db.Items.Update(VARIABLE);

                        }
                    }

                    if (done)
                        Po.CreatedPO = true;


                    if (done)
                    {
                        message = "Purchase Order Done";
                    }
                    else if (Save)
                    {
                        message = "Purchase Order Saved";
                    }
                    else
                    {
                        message = "Purchase Order Updated";
                    }



                    if (AddNewitemsList.Count > 0)
                    {
                        int lastId = db.Items.Max(p => p.Id);
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
                        }
                    }
                    db.SaveChanges();
                    AddNewitemsList.Clear();
                    RemoveItemsList.Clear();
                    myMessageQueue.Enqueue(message);
                }




            }

            else if (Mode == ReceiptStatus.Asn)
            {
                if (state == State.Save)
                {
                    Po.Id = db.PurchaseOrders.Max(p => p.Id) + 1;
                    Po.CreateOrder = DateTime.Now;
                    Save = true;
                }
                else
                {
                    Po = SelectedPurchaseOrder;
                }

                if (Po.CreatedAsn == true)
                {
                    MessageBox.Show("You Can not change after Done");
                }
                else
                {
                    if (Po.Asnumber == 0 && done)
                        Po.Asnumber = db.PurchaseOrders.Max(p => p.Asnumber) + 1;
                    vendorfk = Convert.ToInt32(cmbVendor.SelectedValue);
                    if (vendorfk != 0)
                        Po.Vendor_fk = vendorfk;
                    Po.AsnDate = dpiOrderDate.SelectedDate;
                    Po.ShipDate = dpiShipDate.SelectedDate;
                    Po.CancelDate = dpiCancelDate.SelectedDate;
                    Po.LastEditDate = DateTime.Now;
                    if (!string.IsNullOrWhiteSpace(TxtTotalPrice.Text))
                        Po.AsnTotal = Convert.ToDecimal(TxtTotalPrice.Text);
                    if (ItemsCountInPO != 0)
                        Po.ItemsAsnCount = ItemsCountInPO;
                    if (Save)
                    {
                        Po.CreatedPO = true;
                        db.PurchaseOrders.Add(Po);
                        db.SaveChanges();
                        int lastId = db.Items.Max(p => p.Id);
                        foreach (Item VARIABLE in itemsList)
                        {
                            lastId++;
                            VARIABLE.Id = lastId;
                            VARIABLE.Po_fk = Po.Id;
                            VARIABLE.ProductMaster_fk = VARIABLE.ProductMaster.Id;
                            VARIABLE.ProductMaster = null;
                            db.Items.Add(VARIABLE);

                        }
                    }

                    else
                    {
                        db.PurchaseOrders.Update(Po);

                        foreach (Item VARIABLE in itemsList)
                        {

                            db.Items.Update(VARIABLE);

                        }
                    }

                    if (done)
                        Po.CreatedAsn = true;

                    if (done)
                    {
                        message = "Goods In Transit Done";
                    }
                    else if (Save)
                    {
                        message = "Goods In Transit Saved";
                    }
                    else
                    {
                        message = "Goods In Transit Updated";
                    }



                    if (AddNewitemsList.Count > 0)
                    {
                        int lastId = db.Items.Max(p => p.Id);
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
                        }
                    }
                    if (done)
                    {
                        foreach (Item VARIABLE in itemsList.Concat(AddNewitemsList))
                        {
                            VARIABLE.ProductMaster.OnTheWayInventory += VARIABLE.AsnQuantity;

                        }
                    }
                    db.SaveChanges();
                    AddNewitemsList.Clear();
                    RemoveItemsList.Clear();
                    myMessageQueue.Enqueue(message);

                }



            }
            else if (Mode == ReceiptStatus.Grn)
            {

                Po = SelectedPurchaseOrder;

                if (Po.CreatedGrn == true)
                {
                    MessageBox.Show("You Can not change after Done");
                }
                else
                {
                    if (Po.Grnumber == 0 && done)
                        Po.Grnumber = db.PurchaseOrders.Max(p => p.Grnumber) + 1;
                    vendorfk = Convert.ToInt32(cmbVendor.SelectedValue);
                    Po.GrnDate = dpiOrderDate.SelectedDate;
                    Po.LastEditDate = DateTime.Now;
                    if (ItemsCountInPO != 0)
                        Po.ItemsGrnCount = ItemsCountInPO;
                    Po.LastEditDate = DateTime.Now;
                    if (done)
                        Po.CreatedGrn = true;
                    db.PurchaseOrders.Update(Po);
                    db.SaveChanges();
                    if (done)
                    {
                        message = "Goods Recive Note Done";
                    }
                    else
                    {
                        message = "Goods Recive Note Updated";
                    }



                    foreach (Item VARIABLE in itemsList)
                    {
                        db.Items.Update(VARIABLE);

                    }

                    if (done)
                    {
                        foreach (Item VARIABLE in itemsList)
                        {
                            VARIABLE.ProductMaster.Income += VARIABLE.GrnQuantity;
                            VARIABLE.ProductMaster.Inventory += VARIABLE.GrnQuantity;
                            VARIABLE.ProductMaster.OnTheWayInventory -= VARIABLE.AsnQuantity;
                            //VARIABLE.Diffrent = VARIABLE.AsnQuantity - VARIABLE.GrnQuantity;
                            //if (VARIABLE.Diffrent.Value > 0)
                            //    VARIABLE.Checked = true;
                        }
                    }


                    db.SaveChanges();
                    AddNewitemsList.Clear();
                    RemoveItemsList.Clear();
                    myMessageQueue.Enqueue(message);
                }


            }
            else
            {

            }
        }
    }
}
