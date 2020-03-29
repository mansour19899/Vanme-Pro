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
        private dbContext db;
        private int StepAddItem = 1;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            PurchaseOrder PO=new PurchaseOrder(){Id = 1234};
            txtPoNumber.Text = "1234"; 
            db=new dbContext();

            itemsList = new List<Item>();

            var productlList= db.Products.ToList();
            lvProducts.ItemsSource = productlList;


           DataGridItems.ItemsSource =null;
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
            itemsList.Add(new Item(){Id  = StepAddItem,ProductMaster = wer,PoPrice = wer.Price.Value,PoQuantity = 0,PoTotalPrice = 0});
            //var rr=new Item();
            //var we = rr.ProductMaster.StyleNumber;
            DataGridItems.ItemsSource = null;
            DataGridItems.ItemsSource = itemsList;
            GrdProductList.Visibility = Visibility.Hidden;
            GrdNewPurchersOrder.Visibility = Visibility.Visible;
            StepAddItem++;
        }

        private void GrdListItem_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
           

            DataGridColumn col1 = e.Column;
            DataGridRow row1 = e.Row;
            Item t=e.Row.DataContext as Item;

            int row_index = ((DataGrid)sender).ItemContainerGenerator.IndexFromContainer(row1);
            int col_index = col1.DisplayIndex;


            string header = col1.Header as string;
            if (header.CompareTo("Price") == 0)
            {
                var Price = Convert.ToDecimal(((TextBox)e.EditingElement).Text);
                itemsList.FirstOrDefault(p => p.Id == t.Id).PoTotalPerPrice = Convert.ToDecimal(t.PoQuantity * Price);
                itemsList.FirstOrDefault(p => p.Id == t.Id).PoPrice = Price;
            }
            else
            {
                var QyT = Convert.ToInt32(((TextBox)e.EditingElement).Text);
                itemsList.FirstOrDefault(p => p.Id == t.Id).PoTotalPerPrice = Convert.ToDecimal(t.PoPrice * QyT);
                itemsList.FirstOrDefault(p => p.Id == t.Id).PoQuantity = QyT;
            }

            DataGridItems.ItemsSource = null;
            DataGridItems.ItemsSource=itemsList;

            decimal SumPrice = 0;

            foreach (Item item in itemsList)
            {
                SumPrice = item.PoTotalPerPrice + SumPrice;
            }

            TxtTotalPrice.Text = SumPrice.ToString();
            int x = 0;


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

        private void StackPanel_MouseDown_1(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
