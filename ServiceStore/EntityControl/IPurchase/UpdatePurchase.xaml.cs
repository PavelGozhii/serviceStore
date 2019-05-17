using ServiceStore.Dao;
using ServiceStore.EntityControl.ICustomer;
using ServiceStore.EntityControl.ITelephone;
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

namespace ServiceStore.EntityControl.IPurchase
{
    /// <summary>
    /// Логика взаимодействия для UpdatePurchase.xaml
    /// </summary>
    public partial class UpdatePurchase : Window
    {

        PurchaseDao purchaseDao;
        CustomerDao customerDao;
        TelephoneDao telephoneDao;
        SqlConnection connection;

        public UpdatePurchase(Purchase purchase, SqlConnection connection)
        {

            InitializeComponent();
            purchaseDao = new PurchaseDao(connection);
            customerDao = new CustomerDao(connection);
            telephoneDao = new TelephoneDao(connection);
            this.connection = connection;
            InitializeComponent();
            List<Customer> customers = customerDao.SelectAllCustomers();
            List<Telephone> telephones = telephoneDao.SelectAllTelephone();
            string[] nameCustomers = new string[customers.Count];
            string[] telephoneIMEIs = new string[telephones.Count];
            for (int i = 0; i < customers.Count; i++)
            {
                nameCustomers[i] = customers[i].C_Customer;
            }
            for (int i = 0; i < telephones.Count; i++)
            {
                telephoneIMEIs[i] = telephones[i].IMEI;
            }
            ComboBoxIMEI.ItemsSource = telephoneIMEIs;
            ComboBoxCustomer.ItemsSource = nameCustomers;
            TextBoxPurchase.Text = purchase.C_Purchase;
            ComboBoxCustomer.Text = purchase.C_Customer;    
            ComboBoxIMEI.Text = purchase.IMEI;
        }

        private void CreateIMEI_Click(object sender, RoutedEventArgs e)
        {
            TelephoneForm telephoneForm = new TelephoneForm(connection);
            telephoneForm.ShowDialog();
            List<Telephone> telephones = telephoneDao.SelectAllTelephone();
            string[] telephoneIMEIs = new string[telephones.Count];
            for (int i = 0; i < telephones.Count; i++)
            {
                telephoneIMEIs[i] = telephones[i].IMEI;
            }
            ComboBoxIMEI.ItemsSource = telephoneIMEIs;
        }

        private void CreateCustomer_Click(object sender, RoutedEventArgs e)
        {
            NewCustomerForm newCustomerForm = new NewCustomerForm(connection);
            newCustomerForm.ShowDialog();
            List<Customer> customers = customerDao.SelectAllCustomers();
            string[] nameCustomers = new string[customers.Count];
            for (int i = 0; i < customers.Count; i++)
            {
                nameCustomers[i] = customers[i].FullName;
            }
            ComboBoxCustomer.ItemsSource = nameCustomers;
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            string C_Purchase = TextBoxPurchase.Text;
            string Customer = ComboBoxCustomer.Text;
            List<Customer> customers = customerDao.SelectAllCustomers();
            string IMEI = ComboBoxIMEI.Text;
            string Status = "In proccess";
            string Year = DateTime.Today.Year.ToString();
            string Month = DateTime.Today.Month.ToString();
            string Day = DateTime.Today.Day.ToString();
            string FullDate = Year + "-" + Month + "-" + Day;
            Purchase purchase = new Purchase(C_Purchase, Status, FullDate, Customer, IMEI);
            purchaseDao.UpdatePurchse(purchase, C_Purchase);
            Close();
        }
    }
}
