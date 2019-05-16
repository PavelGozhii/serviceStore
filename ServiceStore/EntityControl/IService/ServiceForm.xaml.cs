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

namespace ServiceStore.EntityControl.IService
{
    /// <summary>
    /// Логика взаимодействия для ServiceForm.xaml
    /// </summary>
    public partial class ServiceForm : Window
    {
        ServiceDao serviceDao;
        DetailDao detailDao;
        public ServiceForm(SqlConnection connection)
        {
            InitializeComponent();
            serviceDao = new ServiceDao(connection);
            detailDao = new DetailDao(connection);
            List<Detail> details = detailDao.SelectAllDetails();
            string[] detailsName = new string[details.Count];
            for (int i = 0; i < details.Count; i++)
            {
                detailsName[i] = details[i].Type;
            }
            DetailComboBox.ItemsSource = detailsName;
            updateBtn.Content = "Create";
        }

        public ServiceForm(Service service, SqlConnection connection)
        {
            InitializeComponent();
            serviceDao = new ServiceDao(connection);
            detailDao = new DetailDao(connection);
            C_ServiceTextBox.Text = service.C_Service;
            C_ServiceTextBox.Visibility = Visibility.Hidden;
            C_Service.Visibility = Visibility.Hidden;
            NameTextBox.Text = service.Name;
            CategoryTextBox.Text = service.Category;
            PriceTextBox.Text = service.Price.ToString();
            DetailComboBox.SelectedItem = service.Detail;
            List<Detail> details = detailDao.SelectAllDetails();
            string[] detailsName = new string[details.Count];
            for(int i = 0; i < details.Count; i++)
            {
                detailsName[i] = details[i].Type;
            }
            DetailComboBox.ItemsSource = detailsName;
            updateBtn.Content = "Update";
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            string C_Service = C_ServiceTextBox.Text;
            string Name = NameTextBox.Text;
            string Category = CategoryTextBox.Text;
            double Price = double.Parse(PriceTextBox.Text);
            string Detail = DetailComboBox.SelectedItem.ToString();
            Service service = new Service(C_Service, Name, Category, Price, Detail);
            if (updateBtn.Content.Equals("Update"))
            {
                serviceDao.updateService(service, C_Service);
            }else if (updateBtn.Content.Equals("Create"))
            {
                serviceDao.InsertService(service);
            }
            Close();
        }
    }
}
