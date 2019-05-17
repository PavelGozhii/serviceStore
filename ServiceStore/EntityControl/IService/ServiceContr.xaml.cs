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

namespace ServiceStore.EntityControl.IService
{
    /// <summary>
    /// Логика взаимодействия для ServiceContr.xaml
    /// </summary>
    public partial class ServiceContr : UserControl
    {
        SqlConnection connection;
        ServiceDao serviceDao;
        public ServiceContr(SqlConnection connection)
        {
            this.connection = connection;
            serviceDao = new ServiceDao(connection);
            InitializeComponent();
            GridData();
            if (DBConnection.id.Equals("Seller"))
            {
                UpdateBtn.Visibility = Visibility.Hidden;
                DeleteBtn.Visibility = Visibility.Hidden;
                Add.Visibility = Visibility.Hidden;
                Name.Width= 180;
                Category.Width = 120;
                Price.Width = 120;
                Detail.Width = 150;
            }
        }

        public void GridData()
        {
            grdService.ItemsSource = serviceDao.SelectAllService();
        }

        public void GridData(List<Service> services)
        {
            grdService.ItemsSource = services;
        }
        

        public void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            string Id = (grdService.SelectedItem as Service).C_Service;
            serviceDao.DeleteService(Id);
            GridData();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            string Id = (grdService.SelectedItem as Service).C_Service;
            Service service = serviceDao.SelectServiceById(Id);
            ServiceForm serviceForm = new ServiceForm(service, connection);
            serviceForm.ShowDialog();
            GridData();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ServiceForm serviceForm = new ServiceForm(connection);
            serviceForm.ShowDialog();
            GridData();
        }

        private void Search_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            searchTextBox.Clear(); 
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Service> services = serviceDao.SelectAllService();
            List<Service> input = new List<Service>();
            for(int i = 0; i < services.Count; i++)
            {
                if (services[i].Name.Contains(searchTextBox.Text))
                {
                    input.Add(services[i]);
                }
            }
            GridData(input);
        }
    }
}
