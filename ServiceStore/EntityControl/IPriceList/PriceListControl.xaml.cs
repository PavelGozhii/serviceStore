using ServiceStore.Dao;
using ServiceStore.Model;
using ServiceStore.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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

namespace ServiceStore.EntityControl.IPriceList
{
    /// <summary>
    /// Логика взаимодействия для PriceListControl.xaml
    /// </summary>
    public partial class PriceListControl : UserControl
    {
        SqlConnection connection;
        PriceListDao priceListDao;
        List<PriceList> priceLists;
        public PriceListControl(SqlConnection connection)
        {            
            InitializeComponent();
            this.connection = connection;
            priceListDao = new PriceListDao(connection);
            priceLists = priceListDao.SelectPriceList();
            DataGrid();
        }

        private void DataGrid()
        {
            grdTelephoneList.ItemsSource = priceLists;
        }

        private void DataGrid(List<PriceList> priceLists)
        {
            grdTelephoneList.ItemsSource = priceLists;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<PriceList> input = new List<PriceList>();
            for (int i = 0; i < priceLists.Count; i++)
            {
                if (priceLists[i].NameService.Contains(searchTextBox.Text))
                {
                    input.Add(priceLists[i]);
                }
            }
            DataGrid(input);
        }

        private void SearchTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            searchTextBox.Clear();
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            PdfConvert.FullNotApp(grdTelephoneList, "PriceList");
        }
    }
}
