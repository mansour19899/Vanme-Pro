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

namespace Vanme_Pro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<ItemTemp> GrdItemsTemp;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
           List< ItemTemp> db=new List<ItemTemp>()
           {
               new ItemTemp(){StyleNumber = "12.02.136876",price = 1.68m,PO_QYT = 0,Vendor = "Azir",Size = "12*12", Id=1},
               new ItemTemp(){StyleNumber = "12.02.136877",price = 2.68m,PO_QYT = 0,Vendor = "Azir",Size = "12*12", Id=2},
               new ItemTemp(){StyleNumber = "12.02.136878",price = 0.68m,PO_QYT = 0,Vendor = "Azir",Size = "12*12", Id=3},
               new ItemTemp(){StyleNumber = "12.02.136879",price = 5.68m,PO_QYT = 0,Vendor = "Azir",Size = "12*12", Id=4},
               new ItemTemp(){StyleNumber = "12.02.136871",price = 1.68m,PO_QYT = 0,Vendor = "Azir",Size = "12*12", Id=5},
               new ItemTemp(){StyleNumber = "12.02.136872",price = 1.68m,PO_QYT = 0,Vendor = "Azir",Size = "12*12", Id=6},
               new ItemTemp(){StyleNumber = "12.02.136886",price = 1.68m,PO_QYT = 0,Vendor = "Azir",Size = "12*12", Id=7},
               new ItemTemp(){StyleNumber = "12.02.136896",price = 1.68m,PO_QYT = 0,Vendor = "Azir",Size = "12*12", Id=8},
               new ItemTemp(){StyleNumber = "12.02.136846",price = 1.68m,PO_QYT = 0,Vendor = "Azir",Size = "12*12", Id=9},
               new ItemTemp(){StyleNumber = "12.02.136836",price = 1.68m,PO_QYT = 0,Vendor = "Azir",Size = "12*12", Id=10},
               new ItemTemp(){StyleNumber = "12.02.136826",price = 1.68m,PO_QYT = 0,Vendor = "Azir",Size = "12*12", Id=11},
           };
           GrdListItem.ItemsSource = db;
            GrdItemsTemp = GrdListItem.ItemsSource.Cast<ItemTemp>().ToList();
        }

        private void lvProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
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
