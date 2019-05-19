using ServiceStore.Dao;
using ServiceStore.EntityControl.ICustomer;
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

namespace ServiceStore.EntityControl
{
    /// <summary>
    /// Логика взаимодействия для NewRapairControl.xaml
    /// </summary>
    public partial class NewRapairControl : UserControl
    {
        SqlConnection connection;
        CustomerDao customerDao;
        TelephoneDao telephoneDao;
        PurchaseDao purchaseDao;
        TelephoneModelDao telephoneModelDao;
        PurchaseInfoDao purchaseTelephoneInfo;
        PurchaseServiceDao purchaseServiceDao;
        ServiceDao serviceDao;
        public NewRapairControl(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            purchaseServiceDao = new PurchaseServiceDao(connection);
            purchaseTelephoneInfo = new PurchaseInfoDao(connection);
            customerDao = new CustomerDao(connection);
            telephoneDao = new TelephoneDao(connection);
            telephoneModelDao = new TelephoneModelDao(connection);
            purchaseDao = new PurchaseDao(connection);
            serviceDao = new ServiceDao(connection);
            List<TelephoneModel> telephoneModels = telephoneModelDao.selectAllTelephomeModel();
            List<Customer> customers = customerDao.SelectAllCustomers();
            List<Service> services = serviceDao.SelectAllService();
            string[] login = new string[customers.Count];
            string[] telephones = new string[telephoneModels.Count];
            string[] servicesStr = new string[services.Count];
            for(int i = 0; i < telephoneModels.Count; i++)
            {
                telephones[i] = telephoneModels[i].C_TelephoneModel;
            }
            for(int i = 0; i < customers.Count; i++)
            {
                login[i] = customers[i].C_Customer;
            }
            for(int i = 0; i < services.Count; i++)
            {
                servicesStr[i] = services[i].C_Service;
            }
            ComboBoxCustomer.ItemsSource = login;
            TelephoneModelComboBox.ItemsSource = telephones;
            ComboBoxService.ItemsSource = servicesStr;
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            string C_Purchase = TextBoxPurchase.Text;
            string Customer = ComboBoxCustomer.Text;
            string IMEI = TextBoxIMEI.Text;
            string Status = "In proccess";
            string TelephoneModel = TelephoneModelComboBox.Text;
            string Year = DateTime.Today.Year.ToString();
            string Month = DateTime.Today.Month.ToString();
            string Day = DateTime.Today.Day.ToString();
            string FullDate = Year + "-" + Month + "-" + Day;
            string Service = ComboBoxService.Text;          
            Purchase purchase = new Purchase(C_Purchase, Status, FullDate, Customer, IMEI);
            Telephone telephone = new Telephone(IMEI, TelephoneModel);
            PurchaseService purchaseService = new PurchaseService(C_Purchase, Service);
            if (telephoneDao.InsertTelephone(telephone))
            {
                if (purchaseDao.InsertPurchase(purchase))
                {
                    purchaseServiceDao.InsertPurchaseService(purchaseService);
                }
            }
            MessageBox.Show("Created purchase"); 
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
    }
}
