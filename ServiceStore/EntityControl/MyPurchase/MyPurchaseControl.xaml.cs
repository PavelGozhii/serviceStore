using ServiceStore.Dao;
using ServiceStore.EntityControl.ITrademark;
using ServiceStore.Model;
using ServiceStore.Services;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ServiceStore.EntityControl.MyPurchase
{
    /// <summary>
    /// Логика взаимодействия для MyPurchaseControl.xaml
    /// </summary>
    public partial class MyPurchaseControl : UserControl
    {
        SqlConnection connection;
        PurchaseInfoDao purchaseInfoDao;
        List<PurchaseInfo> purchaseInfos;

        public MyPurchaseControl(SqlConnection connection)
        {
            this.connection = connection;
            purchaseInfoDao = new PurchaseInfoDao(connection);
            purchaseInfos = purchaseInfoDao.SelectPurchaseInfo();
            InitializeComponent();
            DataGrid();
        }

        private void DataGrid()
        {
            grdPurchase.ItemsSource = purchaseInfos;
        }

        private void DataGrid(List<PurchaseInfo> purchaseInfos)
        {
            grdPurchase.ItemsSource = purchaseInfos;
        }

        private void SearchTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            searchTextBox.Clear();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<PurchaseInfo> input = new List<PurchaseInfo>();
            for (int i = 0; i < purchaseInfos.Count; i++)
            {
                if (purchaseInfos[i].TelephoneModel.Contains(searchTextBox.Text))
                {
                    input.Add(purchaseInfos[i]);
                }
            }
            DataGrid(input);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NewTrademarkForm newTrademark = new NewTrademarkForm(connection);
            newTrademark.ShowDialog();
            DataGrid();
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            PdfConvert.FullNotApp(grdPurchase, "Purschase list");
        }
    }
}
