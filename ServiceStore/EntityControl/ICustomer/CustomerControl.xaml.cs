using ServiceStore.Dao;
using ServiceStore.Model;
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

namespace ServiceStore.EntityControl.ICustomer
{
    /// <summary>
    /// Логика взаимодействия для CustomerControl.xaml
    /// </summary>
    public partial class CustomerControl : UserControl
    {
        SqlConnection connection;
        CustomerDao customerDao;
        List<Customer> customers;

        public CustomerControl(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            customerDao = new CustomerDao(connection);
            customers = customerDao.SelectAllCustomers();

            if (DBConnection.id.Equals("Seller"))
            {
                DeleteBtn.Visibility = Visibility.Hidden;
                Customer.Width = 130;
                FullName.Width = 130;
                Date.Width = 130;
                Address.Width = 130;
            }
            DataGrid();
        }

        public void DataGrid()
        {
            grdCustomer.ItemsSource = customers;
        }

        public void DataGrid(List<Customer> customers)
        {
            grdCustomer.ItemsSource = customers;
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            string Id = (grdCustomer.SelectedItem as Customer).C_Customer;
            UpdateCustomer updateCustomer = new UpdateCustomer(connection, customerDao.SelectCustomerByCode(Id));
            updateCustomer.ShowDialog();
            customers = customerDao.SelectAllCustomers();
            DataGrid();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Customer> input = new List<Customer>();
            for (int i = 0; i < customers.Count; i++)
            {
                if (customers[i].FullName.Contains(searchTextBox.Text))
                {
                    input.Add(customers[i]);
                }
            }
            DataGrid(input);
        }

        private void SearchTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            searchTextBox.Clear();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NewCustomerForm newCustomer = new NewCustomerForm(connection);
            newCustomer.ShowDialog();
            customers = customerDao.SelectAllCustomers();
            DataGrid();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            string Id = (grdCustomer.SelectedItem as Customer).C_Customer;
            customerDao.DeleteCustomer(Id);
            customers = customerDao.SelectAllCustomers();
            DataGrid();
        }
    }
}
