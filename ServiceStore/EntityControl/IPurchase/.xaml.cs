using ServiceStore.Dao;
using ServiceStore.EntityControl.IPurchase;
using ServiceStore.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Controls;

namespace ServiceStore
{
    /// <summary>
    /// Логика взаимодействия для UserControlInicio.xaml
    /// </summary>
    public partial class UserControlInicio : UserControl
    {
        SqlConnection connection;
        PurchaseDao purchaseDao;
        public UserControlInicio(SqlConnection connection)
        {
            this.connection = connection;
            purchaseDao = new PurchaseDao(connection);
            InitializeComponent();
            GridData();
            if (DBConnection.id.Equals("Seller"))
            {
                DeleteBtn.Visibility = System.Windows.Visibility.Hidden;
                Status.Width = 120;
                Date.Width = 120;
                Customer.Width = 150;
                IMEI.Width = 150;
            }
        }

        public void GridData()
        {
            grdPurchase.ItemsSource = purchaseDao.SelectAllPurchses();
        }

        public void GridData(List<Purchase> purchases)
        {
            grdPurchase.ItemsSource = purchases;
        }

        private void UpdateBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string Id = (grdPurchase.SelectedItem as Purchase).C_Purchase;
            Purchase purchase = purchaseDao.SelectPurchaseById(Id);
            UpdatePurchase serviceForm = new UpdatePurchase(purchase, connection);
            serviceForm.ShowDialog();
            GridData();
        }

        private void DeleteBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string Id = (grdPurchase.SelectedItem as Purchase).C_Purchase;
            purchaseDao.DeletePurchase(Id);
            GridData();
        }

        private void Add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PurchaseForm purchaseForm = new PurchaseForm(connection);
            purchaseForm.ShowDialog();
            GridData();
       
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Purchase> purchase = purchaseDao.SelectAllPurchses();
            List<Purchase> input = new List<Purchase>();
            for (int i = 0; i < purchase.Count; i++)
            {
                if (purchase[i].C_Purchase.Contains(searchTextBox.Text))
                {
                    input.Add(purchase[i]);
                }
            }
            GridData(input);
        }

        private void SearchTextBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            searchTextBox.Clear();
        }
    }
}
