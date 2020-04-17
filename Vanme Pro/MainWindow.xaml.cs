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
using Vanme_Pro.Models.ViewModels;

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
        private Mode Mode = Mode.PO;
        private State state = State.Save;
        private PurchaseOrder SelectedPurchaseOrder;
        private ProductMaster selectedProductMaster;
        private List<Item> RemoveItemsList = new List<Item>();
        private SnackbarMessageQueue myMessageQueue;
        private bool IsViewDetail = false;
        private bool IsDone = false;
        private User user;
        private CalculateMiscellaneous calculateMiscellaneous = new CalculateMiscellaneous();
        private decimal TotalCharges = 0;
        private bool ModeAddItem = false;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            PurchaseOrder PO = new PurchaseOrder() { Id = 1234 };
            txtPoNumber.Text = "1234";
            db = new dbContext();
            ShowSideBar(Mode);
            itemsList = new List<Item>();
            AddNewitemsList = new List<Item>();
            //   var productlList = db.Products.ToList();
            var productlList = db.Products;

            lvProducts.ItemsSource = productlList.ToList();

            user = db.Users.FirstOrDefault();

            var VendorList = db.Vendors.ToList();
            cmbVendor.ItemsSource = VendorList;

            var StoreList = db.Warehouses.ToList();
            cmbShipToStore.ItemsSource = StoreList;
            cmbShipFrom.ItemsSource = StoreList;

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

            if (ModeAddItem)
            {
                if (state == State.Save)

                {
                    if (itemsList.Any(p => p.ProductMaster.Id == wer.Id))
                    {

                    }
                    else
                    {
                        itemsList.Add(new Item() { Id = StepAddItem, ProductMaster_fk = wer.Id, ProductMaster = wer, PoPrice = wer.FobPrice.Value, AsnPrice = wer.FobPrice.Value, PoQuantity = 0, PoItemsPrice = 0 });
                    }


                    FillDataGrid(itemsList);
                }

                else
                {
                    if (AddNewitemsList.Any(p => p.ProductMaster.Id == wer.Id) || itemsList.Any(p => p.ProductMaster.Id == wer.Id))
                    {

                    }
                    else
                    {
                        AddNewitemsList.Add(new Item() { Id = StepAddItem, ProductMaster = wer, PoPrice = wer.FobPrice.Value, AsnPrice = wer.FobPrice.Value, PoQuantity = 0, PoItemsPrice = 0 });
                    }

                    FillDataGrid(itemsList.Concat(AddNewitemsList).ToList());

                }

                ModeAddItem = false;
                HidePanel();
                GrdNewPurchersOrder.Visibility = Visibility.Visible;
                StepAddItem++;
            }
            else
            {
                FillProductInformation(wer);
                HidePanel();
                
                ShowSideBar(Mode.productInformation);
                GrdProductInformation.Visibility = Visibility.Visible;
            }

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

            if (state == State.Save)
                SelectedPurchaseOrder = new PurchaseOrder();

            if (Mode == Mode.PO)
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
                        SelectItem.AsnPrice = Price;
                    }
                    else
                    {
                        var QyT = Convert.ToInt32(((TextBox)e.EditingElement).Text);
                        SelectItem.PoItemsPrice = Convert.ToDecimal(t.PoPrice * QyT);
                        SelectItem.PoQuantity = QyT;
                    }
                }


            }
            else if (Mode == Mode.Asn)
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
                        SelectItem.AsnItemsPrice = Convert.ToDecimal(t.AsnPrice * QyT);
                        SelectItem.AsnQuantity = QyT;
                    }
                }



            }
            else if (Mode == Mode.Grn)
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
            if (Mode == Mode.PO)
            {
                SumPrice = items.Sum(p => p.PoItemsPrice);
                ItemsCountInPO = items.Sum(p => p.PoQuantity);

                TxtTotalPrice.Text = SumPrice.ToString();
            }
            else if (Mode == Mode.Asn)
            {
                SumPrice = items.Sum(p => p.AsnItemsPrice);
                ItemsCountInPO = items.Sum(p => p.AsnQuantity);
                TxtTotalPrice.Text = SumPrice.ToString();
            }
            else if (Mode == Mode.Grn)
            {
                ItemsCountInPO = items.Sum(p => p.GrnQuantity);
            }


        }

        private void GrdListItem_Sorting(object sender, DataGridSortingEventArgs e)
        {
            //GrdItemsTemp = GrdListItem.ItemsSource.Cast<ItemTemp>().ToList();
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ModeAddItem = true;
            lvProducts.ItemsSource = db.Products.ToList();
            HidePanel();
            GrdProductList.Visibility = Visibility.Visible;

        }

        private void FillPerchaseOrderPage(PurchaseOrder pOrder)
        {

            itemsList.Clear();
            if (pOrder.Items != null)
                itemsList = pOrder.Items.ToList();

            if (Mode == Mode.PO)
            {
                txtPoNumber.Text = pOrder.PoNumber.ShowPoNumber();
                cmbVendor.SelectedValue = pOrder.Vendor_fk;
                TxtTotalPrice.Text = pOrder.PoTotal.ToString();
                dpiOrderDate.SelectedDate = pOrder.OrderDate;
                dpiShipDate.SelectedDate = pOrder.ShipDate;
                dpiCancelDate.SelectedDate = pOrder.CancelDate;
                cmbShipFrom.SelectedValue = pOrder.FromWarehouse_fk;
                cmbShipToStore.SelectedValue = pOrder.ToWarehouse_fk;
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

                ShowSideBar(Mode.PO, 2);
            }
            else if (Mode == Mode.Asn)
            {
                txtPoNumber.Text = pOrder.Asnumber.ShowAsnNumber();
                cmbVendor.SelectedValue = pOrder.Vendor_fk;
                cmbShipFrom.SelectedValue = pOrder.FromWarehouse_fk;
                cmbShipToStore.SelectedValue = pOrder.ToWarehouse_fk;
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

                ShowSideBar(Mode.Asn, 2);
            }
            else if (Mode == Mode.Grn)
            {
                calculateMiscellaneous = new CalculateMiscellaneous(pOrder);

                txtPoNumber.Text = pOrder.Grnumber.ShowGrnNumber();
                cmbVendor.SelectedValue = pOrder.Vendor_fk;
                cmbShipFrom.SelectedValue = pOrder.FromWarehouse_fk;
                cmbShipToStore.SelectedValue = pOrder.ToWarehouse_fk;
                TxtTotalPrice.Text = pOrder.GrnTotal.ToString();
                dpiOrderDate.SelectedDate = pOrder.GrnDate;
                dpiShipDate.SelectedDate = pOrder.ShipDate;
                dpiCancelDate.SelectedDate = pOrder.CancelDate;
                txtFright.Text = pOrder.Freight.ToString();
                txtDiscountPercent.Text = pOrder.DiscountPercent.ToString();
                lblDiscountPercent.Content = pOrder.Percent;
                txtDiscountDollers.Text = pOrder.DiscountDollers.ToString();
                txtInsurance.Text = pOrder.Insurance.ToString();
                txtCustomsDuty.Text = pOrder.CustomsDuty.ToString();
                txtHandling.Text = pOrder.Handling.ToString();
                txtForwarding.Text = pOrder.Forwarding.ToString();
                txtLandTransport.Text = pOrder.LandTransport.ToString();
                txtOthers.Text = pOrder.Others.ToString();
                lblTotalChargesDetails.Content = calculateMiscellaneous.getTotalString();
                lblTotalFreight.Content = calculateMiscellaneous.getTotalString();
                lblPoTotal.Content = "GIT Total";
                lblOrderDate.Content = "GRN Date ";
                if (pOrder.LastEditDate != null)
                    txtLastEdit.Text = pOrder.LastEditDate.Value.ToLongDateString();
                lblNumber.Content = "GRN # ";
                FillDataGrid(itemsList);

                ShowSideBar(Mode.Grn, 2);
            }
            else
            {

            }





            IsViewDetail = true;
            HidePanel();
            GrdNewPurchersOrder.Visibility = Visibility.Visible;
        }

        private void FillProductInformation(ProductMaster product)
        {
           
            selectedProductMaster = product;
            this.DataContext = product;
            lblSave.Content = "Edit";
            //txtStyleNumberProduct.Text = product.StyleNumber;
            //txtSkuProduct.Text = product.SKU;
            //txtUpcProduct.Text = product.UPC;
            //txtColorProduct.Text = product.Color;
            //txtSizeProduct.Text = product.Size;
            //txtMadeInProduct.Text = product.MadeIn;
            // txtAluProduct.Text = product.Id.ToString();

            // txtFobPriceProduct.Text = product.FobPrice.ToString();
            //txtCostProduct.Text = product.Cost.ToString();
            //txtWholesaleProduct.Text = product.WholesalePrice.ToString();
            //txtRetailPriceProduct.Text = product.RetailPrice.ToString();
            //txtReceiptPriceProduct.Text = product.ReceiptPrice.ToString();

            //lblInventoryProduct.Content = "---------------";
            lblSaleProduct.Content = "---------------";
            lblIncomeProduct.Content = "---------------";
            //lblMainWarehouseProduct.Content = "---------------";
            //lblStore1Product.Content = "---------------";
            //lblStore2Product.Content = "---------------";


            var addressDebug = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            imgProductMaster.Source = new BitmapImage(new Uri(addressDebug + "\\ProductImages\\" + selectedProductMaster.Image));

            GroupBoxInventory_OnMouseDown(null, null);
        }
        private void GrdProductInformation_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (lblSave.Content.ToString() == "Update")
            {
                SaveAndUpdateProductInformation();
            }
        }

        private void GroupBoxInventory_OnMouseDown(object sender, MouseButtonEventArgs e)
        {

            int InventoryCount = 0;
            int[] MainWarehouseCount = { 0, 0 };
            int[] Store1Count = { 0, 0 };
            int[] Store2Count = { 0, 0 };

            var Store1 = db.ProductInventoryWarehouses.Where(p => p.ProductMaster_fk == selectedProductMaster.Id && p.Warehouse_fk == 3).ToList();

            var Store2 = db.ProductInventoryWarehouses.Where(p => p.ProductMaster_fk == selectedProductMaster.Id && p.Warehouse_fk == 4).ToList();

            var Inventory = db.ProductInventoryWarehouses.Where(p => p.ProductMaster_fk == selectedProductMaster.Id).ToList();
            var MainWarehouse = db.ProductInventoryWarehouses.Where(p => p.ProductMaster_fk == selectedProductMaster.Id && p.Warehouse_fk == 2).ToList();

            foreach (var VARIABLE in Inventory)
            {
                InventoryCount = VARIABLE.Inventory.Value + VARIABLE.OnTheWayInventory.Value + InventoryCount;
            }

            foreach (var VARIABLE in MainWarehouse)
            {
                MainWarehouseCount[0] = VARIABLE.Inventory.Value + MainWarehouseCount[0];
                MainWarehouseCount[1] = VARIABLE.OnTheWayInventory.Value + MainWarehouseCount[1];
            }

            foreach (var VARIABLE in Store1)
            {
                Store1Count[0] = VARIABLE.Inventory.Value + Store1Count[0];
                Store1Count[1] = VARIABLE.OnTheWayInventory.Value + Store1Count[1];
            }

            foreach (var VARIABLE in Store2)
            {
                Store2Count[0] = VARIABLE.Inventory.Value + Store2Count[0];
                Store2Count[1] = VARIABLE.OnTheWayInventory.Value + Store2Count[1];
            }


            lblInventoryProduct.Content = InventoryCount;
            lblStore1Product.Content = (Store1Count[1] > 0) ? Store1Count[0] + "     ----- On the Way : " + Store1Count[1] : Store1Count[0].ToString();
            lblStore2Product.Content = (Store2Count[1] > 0) ? Store2Count[0] + "     ----- On the Way : " + Store2Count[1] : Store2Count[0].ToString();
            lblMainWarehouseProduct.Content = (MainWarehouseCount[1] > 0) ? MainWarehouseCount[0] + "     ----- On the Way : " + MainWarehouseCount[1] : MainWarehouseCount[0].ToString();
        }

        void FillDataGrid(List<Item> list, bool Pre = true)
        {
            if (Mode == Mode.PO)
            {
                DataGridItems.Columns[5].Header = "Previous Quantity";
                DataGridItems.Columns[6].Header = "Quantity ( PO )";
                foreach (var VARIABLE in list)
                {
                    if (Pre)
                        VARIABLE.PreviousQuantity = VARIABLE.PoQuantity;
                    VARIABLE.CurrentQuantity = VARIABLE.PoQuantity;
                    VARIABLE.TotalItemPrice = VARIABLE.PoItemsPrice;
                    VARIABLE.Price = VARIABLE.PoPrice;
                }
            }
            else if (Mode == Mode.Asn)
            {
                DataGridItems.Columns[5].Header = "Quantity ( PO )";
                DataGridItems.Columns[6].Header = "Quantity ( GIT )";

                foreach (var VARIABLE in list)
                {

                    VARIABLE.PreviousQuantity = VARIABLE.PoQuantity;
                    VARIABLE.CurrentQuantity = VARIABLE.AsnQuantity;
                    VARIABLE.TotalItemPrice = VARIABLE.AsnItemsPrice;
                    VARIABLE.Price = VARIABLE.AsnPrice;
                }
            }
            else if (Mode == Mode.Grn)
            {
                DataGridItems.Columns[5].Header = "Quantity ( GIT )";
                DataGridItems.Columns[6].Header = "Quantity ( GRN )";

                foreach (var VARIABLE in list)
                {

                    VARIABLE.PreviousQuantity = VARIABLE.AsnQuantity;
                    VARIABLE.CurrentQuantity = VARIABLE.GrnQuantity;
                    VARIABLE.TotalItemPrice = VARIABLE.AsnItemsPrice;
                    VARIABLE.Price = VARIABLE.AsnPrice;
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
            HidePanel();
            GrdNewPurchersOrder.Visibility = Visibility.Visible;

        }
        private void BtnPurchaseOrder_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            AddNewitemsList.Clear();
            RemoveItemsList.Clear();
            if (IsViewDetail)
            {
                if (Mode == Mode.PO)
                {

                    lvPurchase.ItemsSource = GivePurchaseOrdersList();
                    HidePanel();
                    GrdPurchesOrderList.Visibility = Visibility.Visible;
                    ShowSideBar(Mode.PO);
                    IsViewDetail = false;
                }
                else if (Mode == Mode.Asn)
                {
                    Mode = Mode.PO;
                    lblNewPo.Content = "New PO";
                    FillPerchaseOrderPage(SelectedPurchaseOrder);
                }
                else if (Mode == Mode.Grn)
                {
                    Mode = Mode.PO;
                    lblNewPo.Content = "New PO";
                    FillPerchaseOrderPage(SelectedPurchaseOrder);
                }
                else
                {

                }
            }
            else
            {
                Mode = Mode.PO;
                lblNewPo.Content = "New PO";
                lvPurchase.ItemsSource = GivePurchaseOrdersList();
                HidePanel();
                GrdPurchesOrderList.Visibility = Visibility.Visible;
                ShowSideBar(Mode.PO);

            }


        }


        private void BtnAsnVorchers_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            AddNewitemsList.Clear();
            RemoveItemsList.Clear();
            if (IsViewDetail)
            {

                switch (Mode)
                {
                    case Mode.Asn:
                        {
                            lvPurchase.ItemsSource = GivePurchaseOrdersList();
                            HidePanel();
                            GrdPurchesOrderList.Visibility = Visibility.Visible;
                            IsViewDetail = false;
                            ShowSideBar(Mode.Asn);

                            break;
                        }
                    case Mode.PO:
                        {
                            Mode = Mode.Asn;
                            lblNewPo.Content = "New GIT";
                            FillPerchaseOrderPage(SelectedPurchaseOrder);
                            break;
                        }
                    case Mode.Grn:
                        {
                            Mode = Mode.Asn;
                            lblNewPo.Content = "New GIT";
                            FillPerchaseOrderPage(SelectedPurchaseOrder);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("Error 534");
                            break;
                        }
                }
            }
            else
            {
                Mode = Mode.Asn;
                lblNewPo.Content = "New GIT";

                lvPurchase.ItemsSource = GivePurchaseOrdersList();
                HidePanel();
                GrdPurchesOrderList.Visibility = Visibility.Visible;
                ShowSideBar(Mode.Asn);
            }



        }

        private void BtnGrn_OnMouseDown(object sender, MouseButtonEventArgs e)
        {

            AddNewitemsList.Clear();
            RemoveItemsList.Clear();

            if (IsViewDetail)
            {
                if (Mode == Mode.Grn)
                {

                    lvPurchase.ItemsSource = GivePurchaseOrdersList();
                    HidePanel();
                    GrdPurchesOrderList.Visibility = Visibility.Visible;
                    IsViewDetail = false;
                    ShowSideBar(Mode.Grn);
                }
                else if (Mode == Mode.PO)
                {
                    Mode = Mode.Grn;
                    FillPerchaseOrderPage(SelectedPurchaseOrder);
                }
                else if (Mode == Mode.Asn)
                {
                    Mode = Mode.Grn;
                    FillPerchaseOrderPage(SelectedPurchaseOrder);
                }
                else
                {

                }

            }
            else
            {
                Mode = Mode.Grn;

                lvPurchase.ItemsSource = GivePurchaseOrdersList();
                HidePanel();
                GrdPurchesOrderList.Visibility = Visibility.Visible;
                ShowSideBar(Mode.Grn);

            }

        }
        private void BtnProduct_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Mode = Mode.productInformation;
            IsViewDetail = false;
            ModeAddItem = false;
            HidePanel();
            GrdProductList.Visibility = Visibility.Visible;
        }

        void ShowSideBar(Mode mode, int sub = 1)
        {
            switch (mode)
            {
                case Mode.PO:
                    if (sub == 2)
                    {
                        btnNewPurchaseOrder.Visibility = Visibility.Visible;
                        btnAddItem.Visibility = Visibility.Visible;
                        btnSave.Visibility = Visibility.Visible;
                        btnRemove.Visibility = Visibility.Visible;
                        btnDone.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        btnNewPurchaseOrder.Visibility = Visibility.Visible;
                        btnAddItem.Visibility = Visibility.Collapsed;
                        btnSave.Visibility = Visibility.Collapsed;
                        btnRemove.Visibility = Visibility.Collapsed;
                        btnDone.Visibility = Visibility.Collapsed;
                    }
                    break;
                case Mode.Asn:
                    if (sub == 2)
                    {
                        btnNewPurchaseOrder.Visibility = Visibility.Visible;
                        btnAddItem.Visibility = Visibility.Visible;
                        btnSave.Visibility = Visibility.Visible;
                        btnRemove.Visibility = Visibility.Visible;
                        btnDone.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        btnNewPurchaseOrder.Visibility = Visibility.Visible;
                        btnAddItem.Visibility = Visibility.Collapsed;
                        btnSave.Visibility = Visibility.Collapsed;
                        btnRemove.Visibility = Visibility.Collapsed;
                        btnDone.Visibility = Visibility.Collapsed;
                    }

                    break;
                case Mode.Grn:
                    if (sub == 2)
                    {
                        btnNewPurchaseOrder.Visibility = Visibility.Collapsed;
                        btnAddItem.Visibility = Visibility.Collapsed;
                        btnSave.Visibility = Visibility.Visible;
                        btnRemove.Visibility = Visibility.Collapsed;
                        btnDone.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        btnNewPurchaseOrder.Visibility = Visibility.Collapsed;
                        btnAddItem.Visibility = Visibility.Collapsed;
                        btnSave.Visibility = Visibility.Collapsed;
                        btnRemove.Visibility = Visibility.Collapsed;
                        btnDone.Visibility = Visibility.Collapsed;
                    }

                    break;
                case Mode.Sale:

                    break;
                case Mode.productInformation:
                    btnNewPurchaseOrder.Visibility = Visibility.Collapsed;
                    btnAddItem.Visibility = Visibility.Collapsed;
                    btnSave.Visibility = Visibility.Visible;
                    btnRemove.Visibility = Visibility.Collapsed;
                    btnDone.Visibility = Visibility.Collapsed;
                    break;
                case Mode.Nothong:

                    break;
            }
        }

        void HidePanel()
        {
            GrdNewPurchersOrder.Visibility = Visibility.Hidden;
            GrdSale.Visibility = Visibility.Hidden;
            GrdPurchesOrderList.Visibility = Visibility.Hidden;
            GrdProductList.Visibility = Visibility.Hidden;
            GrdTotalCharges.Visibility = Visibility.Hidden;
            GrdProductInformation.Visibility = Visibility.Hidden;


        }

        private List<PurchaseOrder> GivePurchaseOrdersList()
        {
            UnchangedAllEntities();
            switch (Mode)
            {
                case Mode.PO:
                    return db.PurchaseOrders.Include(p => p.Items).ThenInclude(p => p.ProductMaster).Include(p => p.Vendor).ToList();
                    break;
                case Mode.Asn:
                    return db.PurchaseOrders.Where(p => p.CreatedPO == true)
                         .Include(p => p.Items).ThenInclude(p => p.ProductMaster).Include(p => p.Vendor).ToList();
                    break;
                case Mode.Grn:
                    return db.PurchaseOrders.Where(p => p.CreatedAsn == true)
                         .Include(p => p.Items).ThenInclude(p => p.ProductMaster).Include(p => p.Vendor).ToList();
                    break;
            }

            return null;
        }
        private void BtnSave_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mode == Mode.productInformation)
            {
                SaveAndUpdateProductInformation(true);
            }
            else
            {
                SaveAndUpdate(false);

            }

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
            if (Mode == Mode.PO)
            {

                if (state == State.Save)
                {
                    Po.Id = db.PurchaseOrders.Max(p => p.Id) + 1;
                    Save = true;
                    state = State.Update;
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
                    {
                        Po.PoNumber = db.PurchaseOrders.Max(p => p.PoNumber) + 1;
                        Po.ApprovePoUser_fk = user.Id;
                        Po.CreatedPO = true;
                    }

                    vendorfk = Convert.ToInt32(cmbVendor.SelectedValue);
                    if (vendorfk != 0)
                        Po.Vendor_fk = vendorfk;
                    Po.OrderDate = dpiOrderDate.SelectedDate;
                    Po.ShipDate = dpiShipDate.SelectedDate;
                    Po.CancelDate = dpiCancelDate.SelectedDate;
                    Po.FromWarehouse_fk = Convert.ToInt32(cmbShipFrom.SelectedValue);
                    Po.ToWarehouse_fk = Convert.ToInt32(cmbShipToStore.SelectedValue);
                    Po.LastEditDate = DateTime.Now;
                    if (!string.IsNullOrWhiteSpace(TxtTotalPrice.Text))
                        Po.PoTotal = Convert.ToDecimal(TxtTotalPrice.Text);
                    if (ItemsCountInPO != 0)
                        Po.ItemsPoCount = ItemsCountInPO;
                    if (Save)
                    {
                        db.PurchaseOrders.Add(Po);
                        db.SaveChanges();
                        SelectedPurchaseOrder = Po;
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

                    if (RemoveItemsList.Count > 0 && !Save)
                    {
                        foreach (var VARIABLE in RemoveItemsList)
                        {
                            db.Items.Remove(VARIABLE);
                        }
                    }
                    db.SaveChanges();
                    itemsList.AddRange(AddNewitemsList);
                    AddNewitemsList.Clear();
                    RemoveItemsList.Clear();
                    myMessageQueue.Enqueue(message);
                }




            }

            else if (Mode == Mode.Asn)
            {
                if (state == State.Save)
                {
                    Po.Id = db.PurchaseOrders.Max(p => p.Id) + 1;
                    Save = true;
                    state = State.Update;
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
                    {
                        Po.Asnumber = db.PurchaseOrders.Max(p => p.Asnumber) + 1;
                        Po.ApproveAsnUser_fk = user.Id;
                        Po.CreatedAsn = true;
                    }

                    vendorfk = Convert.ToInt32(cmbVendor.SelectedValue);
                    if (vendorfk != 0)
                        Po.Vendor_fk = vendorfk;
                    Po.AsnDate = dpiOrderDate.SelectedDate;
                    Po.ShipDate = dpiShipDate.SelectedDate;
                    Po.CancelDate = dpiCancelDate.SelectedDate;
                    Po.FromWarehouse_fk = Convert.ToInt32(cmbShipFrom.SelectedValue);
                    Po.ToWarehouse_fk = Convert.ToInt32(cmbShipToStore.SelectedValue);
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

                    if (RemoveItemsList.Count > 0 && !Save)
                    {
                        foreach (var VARIABLE in RemoveItemsList)
                        {
                            db.Items.Remove(VARIABLE);
                        }
                    }
                    if (done)
                    {
                        var ree = db.ProductInventoryWarehouses.Max(p => p.Id);

                        foreach (Item VARIABLE in itemsList.Concat(AddNewitemsList))
                        {
                            ree++;
                            ProductInventoryWarehouse re = db.ProductInventoryWarehouses.SingleOrDefault(p =>
                                p.ProductMaster_fk == VARIABLE.ProductMaster_fk &&
                                p.Warehouse_fk == VARIABLE.PurchaseOrder.ToWarehouse_fk);

                            if (re != null)
                            {
                                re.OnTheWayInventory += VARIABLE.AsnQuantity;
                                db.ProductInventoryWarehouses.Update(re);
                            }
                            else
                            {
                                re = new ProductInventoryWarehouse();
                                re.Id = ree;
                                re.ProductMaster_fk = VARIABLE.ProductMaster_fk;
                                re.Warehouse_fk = VARIABLE.PurchaseOrder.ToWarehouse_fk;
                                re.OnTheWayInventory = VARIABLE.AsnQuantity;
                                db.ProductInventoryWarehouses.Add(re);
                            }
                            // VARIABLE.ProductMaster.OnTheWayInventory += VARIABLE.AsnQuantity;

                            if (Convert.ToInt16(cmbShipToStore.SelectedValue) != 2)
                            {
                                ProductInventoryWarehouse reee = db.ProductInventoryWarehouses.SingleOrDefault(p =>
                                    p.ProductMaster_fk == VARIABLE.ProductMaster_fk &&
                                    p.Warehouse_fk == VARIABLE.PurchaseOrder.FromWarehouse_fk);

                                reee.Inventory -= VARIABLE.AsnQuantity;


                            }


                        }
                    }

                    db.SaveChanges();
                    SelectedPurchaseOrder = Po;
                    itemsList.AddRange(AddNewitemsList);
                    AddNewitemsList.Clear();
                    RemoveItemsList.Clear();
                    myMessageQueue.Enqueue(message);

                }



            }
            else if (Mode == Mode.Grn)
            {

                Po = SelectedPurchaseOrder;

                if (Po.CreatedGrn == true)
                {
                    MessageBox.Show("You Can not change after Done");
                }
                else
                {
                    if (Po.Grnumber == 0 && done)
                    {
                        Po.Grnumber = db.PurchaseOrders.Max(p => p.Grnumber) + 1;
                        Po.ApproveGrnUser_fk = user.Id;
                        Po.CreatedGrn = true;
                    }

                    vendorfk = Convert.ToInt32(cmbVendor.SelectedValue);
                    Po.GrnDate = dpiOrderDate.SelectedDate;
                    Po.GrnTotal = Po.TotalCharges + Po.AsnTotal;
                    Po.LastEditDate = DateTime.Now;
                    CalculateTotalPriceItemsCount(Po.Items.ToList());
                    if (ItemsCountInPO != 0)
                        Po.ItemsGrnCount = ItemsCountInPO;
                    Po.LastEditDate = DateTime.Now;

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
                        ProductInventoryWarehouse ree;
                        foreach (Item VARIABLE in itemsList)
                        {
                            ree = db.ProductInventoryWarehouses.SingleOrDefault(p =>
                                 p.ProductMaster_fk == VARIABLE.ProductMaster_fk &&
                                 p.Warehouse_fk == VARIABLE.PurchaseOrder.ToWarehouse_fk);
                            if (ree != null)
                            {
                                ree.Inventory += VARIABLE.GrnQuantity;
                                ree.OnTheWayInventory -= VARIABLE.AsnQuantity;
                            }
                            VARIABLE.ProductMaster.Income += VARIABLE.GrnQuantity;
                            VARIABLE.ProductMaster.Inventory += VARIABLE.GrnQuantity;
                            VARIABLE.ProductMaster.OnTheWayInventory -= VARIABLE.AsnQuantity;

                        }
                    }


                    db.SaveChanges();
                    myMessageQueue.Enqueue(message);
                }


            }
            else
            {

            }
        }

        void SaveAndUpdateProductInformation(bool True=false)
        {
            List<TextBox> list = new List<TextBox>()
            {
                txtStyleNumberProduct,txtSkuProduct,txtUpcProduct,txtColorProduct,txtSizeProduct,txtMadeInProduct
                ,txtAluProduct,txtFobPriceProduct,txtCostProduct,txtWholesaleProduct,txtRetailPriceProduct,txtReceiptPriceProduct
            };

            if (!True)
            {
                foreach (var VARIABLE in list)
                {
                    VARIABLE.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
                    MakeReadOnly();
                }
            }
            else
            {
                if (lblSave.Content.ToString() == "Edit")
                {
                    lblSave.Content = "Update";
                    foreach (var VARIABLE in list)
                    {
                        VARIABLE.IsReadOnly = false;
                    }

                    txtAluProduct.IsReadOnly = true;
                }
                else if (lblSave.Content.ToString() == "Update")
                {
                    foreach (var VARIABLE in list)
                    {
                        VARIABLE.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                    }

                    db.Update(selectedProductMaster);
                    db.SaveChanges();
                    myMessageQueue.Enqueue("Product Updated");
                    MakeReadOnly();
                    lblSave.Content = "Edit";
                }
            }


            void MakeReadOnly()
            {
                foreach (var VARIABLE in list)
                {
                    VARIABLE.IsReadOnly = true;
                }
            }

        }

        #region LostFocusOfFright

        private void txtFrright_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtFright.Text))
            {
                calculateMiscellaneous.SetFright = txtFright.Text;
                lblTotalFreight.Content = calculateMiscellaneous.getTotalString();
                TotalCharges = calculateMiscellaneous.GetTotal();
            }

        }

        private void txtDiscountPercent_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtDiscountPercent.Text))
            {
                calculateMiscellaneous.TotalAsnPrice = SelectedPurchaseOrder.AsnTotal.Value;
                calculateMiscellaneous.SetDiscountPercent = txtDiscountPercent.Text;
                lblTotalFreight.Content = calculateMiscellaneous.getTotalString();
                lblDiscountPercent.Content = "   " + calculateMiscellaneous.Percent;
                txtDiscountPercent.Text = "- " + calculateMiscellaneous.DiscountPercent.ToString();
                TotalCharges = calculateMiscellaneous.GetTotal();
            }

        }

        private void txtDiscountDollers_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtDiscountDollers.Text))
            {
                calculateMiscellaneous.SetDiscountDollers = txtDiscountDollers.Text;
                lblTotalFreight.Content = calculateMiscellaneous.getTotalString();
                txtDiscountDollers.Text = "- " + calculateMiscellaneous.DiscountDollers.ToString();
                TotalCharges = calculateMiscellaneous.GetTotal();
            }

        }

        private void txtInsurance_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtInsurance.Text))
            {
                calculateMiscellaneous.SetInsurance = txtInsurance.Text;
                lblTotalFreight.Content = calculateMiscellaneous.getTotalString();
                TotalCharges = calculateMiscellaneous.GetTotal();
            }

        }

        private void txtCustomsDuty_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCustomsDuty.Text))
            {
                calculateMiscellaneous.SetCustomsDuty = txtCustomsDuty.Text;
                lblTotalFreight.Content = calculateMiscellaneous.getTotalString();
                TotalCharges = calculateMiscellaneous.GetTotal();
            }

        }

        private void txtHandling_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtHandling.Text))
            {
                calculateMiscellaneous.SetHandling = txtHandling.Text;
                lblTotalFreight.Content = calculateMiscellaneous.getTotalString();
                TotalCharges = calculateMiscellaneous.GetTotal();
            }

        }

        private void txtForwarding_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtForwarding.Text))
            {
                calculateMiscellaneous.SetForwarding = txtForwarding.Text;
                lblTotalFreight.Content = calculateMiscellaneous.getTotalString();
                TotalCharges = calculateMiscellaneous.GetTotal();
            }

        }

        private void txtLandTransport_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtLandTransport.Text))
            {
                calculateMiscellaneous.SetLandTransport = txtLandTransport.Text;
                lblTotalFreight.Content = calculateMiscellaneous.getTotalString();
                TotalCharges = calculateMiscellaneous.GetTotal();
            }

        }

        private void txtOthers_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtOthers.Text))
            {
                calculateMiscellaneous.SetOthers = txtOthers.Text;
                lblTotalFreight.Content = calculateMiscellaneous.getTotalString();
                TotalCharges = calculateMiscellaneous.GetTotal();
            }

        }

        #endregion

        #region SetNumericFrieght

        public bool SetNumeric(object sender, KeyEventArgs e, TextBox txt)
        {
            bool result = false;
            var tr = (sender as TextBox).Text.IndexOf('.');

            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key == Key.Back) || (e.Key == Key.Decimal) || (e.Key == Key.Tab))
            { result = false; }
            else if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key == Key.OemPeriod))
            { result = false; }
            else
            { result = true; }

            if (e.Key == Key.OemPeriod && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                result = true;
            }
            if (e.Key == Key.Decimal && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                result = true;
            }
            var count = txt.Text.Split('.');
            if (count.Count() > 1)
            {
                if (count[1].Count() > 3 && e.Key != Key.Tab)
                {
                    result = true;
                }
            }
            return result;
        }
        private void txtFright_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e, txtFright);
        }

        private void txtDiscountPercent_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e, txtDiscountPercent);
        }

        private void txtDiscountDollers_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e, txtDiscountDollers);
        }

        private void txtInsurance_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e, txtInsurance);
        }

        private void txtCustomsDuty_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e, txtCustomsDuty);
        }

        private void txtHandling_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e, txtHandling);
        }

        private void txtForwarding_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e, txtForwarding);
        }

        private void txtLandTransport_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e, txtLandTransport);
        }

        private void txtOthers_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e, txtOthers);
        }

        #endregion

        private void BtnTotalCharges_OnClick(object sender, RoutedEventArgs e)
        {

            if (Mode == Mode.Grn)
                GrdTotalCharges.Visibility = Visibility.Visible;
            else
            {
                MessageBox.Show("You can Import Charges when Grn Mode");
            }
        }

        private void BtnOkCharges_OnClick(object sender, RoutedEventArgs e)
        {
            decimal x = 0;
            SelectedPurchaseOrder.Freight = calculateMiscellaneous.Fright;
            SelectedPurchaseOrder.DiscountPercent = calculateMiscellaneous.DiscountPercent;
            SelectedPurchaseOrder.Percent = calculateMiscellaneous.Percent;
            SelectedPurchaseOrder.DiscountDollers = calculateMiscellaneous.DiscountDollers;
            SelectedPurchaseOrder.Insurance = calculateMiscellaneous.Insurance;
            SelectedPurchaseOrder.CustomsDuty = calculateMiscellaneous.CustomsDuty;
            SelectedPurchaseOrder.Handling = calculateMiscellaneous.Handling;
            SelectedPurchaseOrder.Forwarding = calculateMiscellaneous.Forwarding;
            SelectedPurchaseOrder.LandTransport = calculateMiscellaneous.LandTransport;
            SelectedPurchaseOrder.Others = calculateMiscellaneous.Others;
            SelectedPurchaseOrder.TotalCharges = calculateMiscellaneous.GetTotal();
            lblTotalChargesDetails.Content = lblTotalFreight.Content;
            foreach (var VARIABLE in SelectedPurchaseOrder.Items)
            {

                x = (SelectedPurchaseOrder.TotalCharges.Value / SelectedPurchaseOrder.AsnTotal.Value * VARIABLE.AsnPrice) +
                                VARIABLE.AsnPrice;
                VARIABLE.Cost = Math.Round(x, 2, MidpointRounding.ToEven);
            }
            FillDataGrid(SelectedPurchaseOrder.Items.ToList());
            TxtTotalPrice.Text = (SelectedPurchaseOrder.TotalCharges + SelectedPurchaseOrder.AsnTotal).ToString();
            GrdTotalCharges.Visibility = Visibility.Hidden;
        }

        public void UnchangedAllEntities()
        {
            var changedEntriesCopy = db.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Unchanged;
        }


    }
}
