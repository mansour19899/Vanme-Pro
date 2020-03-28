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
        private dbContext db;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            PurchaseOrder PO=new PurchaseOrder(){Id = 1234};
            txtPoNumber.Text = "1234"; 
            db=new dbContext();

            var productlList= db.Products.ToList();
            lvProducts.ItemsSource = productlList;


           GrdListItem.ItemsSource =null;
          // GrdItemsTemp = GrdListItem.ItemsSource.Cast<ItemTemp>().ToList();
        }

        private void lvProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Hi");
        }

        private void GrdListItem_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
           

            DataGridColumn col1 = e.Column;
            DataGridRow row1 = e.Row;
            ItemTemp t=e.Row.DataContext as ItemTemp;

            int row_index = ((DataGrid)sender).ItemContainerGenerator.IndexFromContainer(row1);
            int col_index = col1.DisplayIndex;


            string header = col1.Header as string;
            if (header.CompareTo("Price") == 0)
            {
                var Price = Convert.ToDecimal(((TextBox)e.EditingElement).Text);
                GrdItemsTemp.FirstOrDefault(p => p.Id == t.Id).Totalprice = Convert.ToDecimal(t.PO_QYT * Price);
                GrdItemsTemp.FirstOrDefault(p => p.Id == t.Id).price = Price;
            }
            else
            {
                var QyT = Convert.ToInt32(((TextBox)e.EditingElement).Text);
                GrdItemsTemp.FirstOrDefault(p => p.Id == t.Id).Totalprice = Convert.ToDecimal(t.price * QyT);
                GrdItemsTemp.FirstOrDefault(p => p.Id == t.Id).PO_QYT = QyT;
            }
           
           
            GrdListItem.ItemsSource=null;
            GrdListItem.ItemsSource = GrdItemsTemp;
            
            int x = 0;


        }

        private void GrdListItem_Sorting(object sender, DataGridSortingEventArgs e)
        {
             //GrdItemsTemp = GrdListItem.ItemsSource.Cast<ItemTemp>().ToList();
        }
    }
}
