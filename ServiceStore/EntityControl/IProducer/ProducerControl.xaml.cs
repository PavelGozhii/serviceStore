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

namespace ServiceStore.EntityControl.IProducer
{
    /// <summary>
    /// Логика взаимодействия для ProducerControl.xaml
    /// </summary>
    public partial class ProducerControl : UserControl
    {
        SqlConnection connection;
        ProducerDao producerDao;
        public ProducerControl(SqlConnection connection)
        {
            this.connection = connection;
            InitializeComponent();
            producerDao = new ProducerDao(connection);
            DataGrid();
            if (DBConnection.id.Equals("Seller"))
            {
                UpdateBtn.Visibility = Visibility.Hidden;
                DeleteBtn.Visibility = Visibility.Hidden;
                Add.Visibility = Visibility.Hidden;
                Producer.Width = 130;
                Director.Width = 110;
                Telephone.Width = 130;
                Email.Width = 140;
                Review.Width = 80;
                PhisicalAddress.Width = 150;
            }
        }

        public void DataGrid()
        {
            grdProducer.ItemsSource = producerDao.SelectAllProducers();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            string Id = (grdProducer.SelectedItem as Producer).C_Producer;
            UpdateProducer updateProducer = new UpdateProducer(connection, producerDao.SelectProducerById(Id));
            updateProducer.ShowDialog();
            DataGrid();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            string Id = (grdProducer.SelectedItem as Producer).C_Producer;
            producerDao.DeleteProducer(Id);
            DataGrid();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddNewProducer newProducer = new AddNewProducer(connection);
            newProducer.ShowDialog();
            DataGrid();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
