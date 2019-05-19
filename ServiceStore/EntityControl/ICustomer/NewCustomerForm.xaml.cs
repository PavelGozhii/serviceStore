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
    /// Логика взаимодействия для NewCustomerForm.xaml
    /// </summary>
    public partial class NewCustomerForm : Window
    {
        CustomerDao customerDao;  
        public NewCustomerForm(SqlConnection connection)
        {
            InitializeComponent();
            customerDao = new CustomerDao(connection);
            int[] days = new int[31];
            int[] months = new int[12];
            int[] years = new int[60];
            for(int i = 0; i < 31; i++)
            {
                days[i] = i+1;
            }
            for(int i = 0; i < 12; i++)
            {
                months[i] = i + 1;
            }
            for(int i = 0; i < 60; i++)
            {
                years[i] = i + 1960;
            }
            ComboBoxDay.ItemsSource = days;
            ComboBoxMonth.ItemsSource = months;
            ComboBoxYear.ItemsSource = years;
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            string c_customer = CustomerTextBox.Text;
            string FullName = FullNameTextBox.Text;
            string Date = ComboBoxYear.Text + "-" + ComboBoxMonth.Text + "-" + ComboBoxDay.Text;
            string Address = AddressTextBOx.Text;
            Customer customer = new Customer(c_customer, FullName, Date, Address);
            customerDao.InsertCustomer(customer);
            this.Close();
        }
    }
}
