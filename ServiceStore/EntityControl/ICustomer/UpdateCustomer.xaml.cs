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
using System.Windows.Shapes;

namespace ServiceStore.EntityControl.ICustomer
{
    /// <summary>
    /// Логика взаимодействия для UpdateCustomer.xaml
    /// </summary>
    public partial class UpdateCustomer : Window
    {
        SqlConnection connection;
        CustomerDao customerDao;
        public UpdateCustomer(SqlConnection connection, Customer customer)
        {
            InitializeComponent();
            this.connection = connection;
            customerDao = new CustomerDao(connection);
            LastName.Text = customer.C_Customer;
            CustomerTextBox.Text = customer.C_Customer;
            FullNameTextBox.Text = customer.FullName;
            DateTime dateTime = DateTime.Parse(customer.DateOfBirth);
            AddressTextBOx.Text = customer.Address;
            int[] days = new int[31];
            int[] months = new int[12];
            int[] years = new int[60];
            for (int i = 0; i < 31; i++)
            {
                days[i] = i + 1;
            }
            for (int i = 0; i < 12; i++)
            {
                months[i] = i + 1;
            }
            for (int i = 0; i < 60; i++)
            {
                years[i] = i + 1960;
            }
            ComboBoxDay.ItemsSource = days;
            ComboBoxMonth.ItemsSource = months;
            ComboBoxYear.ItemsSource = years;
            ComboBoxDay.Text = dateTime.Day.ToString();
            ComboBoxMonth.Text = dateTime.Month.ToString();
            ComboBoxYear.Text = dateTime.Year.ToString();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            string LastCode = LastName.Text;
            string C_Customer = CustomerTextBox.Text;
            string FullName = FullNameTextBox.Text;
            string c_customer = CustomerTextBox.Text;
            string Date = ComboBoxYear.Text + "-" + ComboBoxMonth.Text + "-" + ComboBoxDay.Text;
            string Address = AddressTextBOx.Text;
            Customer customer = new Customer(C_Customer, FullName, Date, Address);
            customerDao.UpdateCustomer(customer, LastCode);
            Close();
        }
    }
}
