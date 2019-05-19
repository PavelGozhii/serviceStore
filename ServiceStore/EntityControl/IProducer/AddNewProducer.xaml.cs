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

namespace ServiceStore.EntityControl.IProducer
{
    /// <summary>
    /// Логика взаимодействия для AddNewProducer.xaml
    /// </summary>
    public partial class AddNewProducer : Window
    {
        SqlConnection connection;
        ProducerDao producerDao;

        public AddNewProducer(SqlConnection connection)
        {
            InitializeComponent();
            producerDao = new ProducerDao(connection);
            this.connection = connection;
            InitializeComponent();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

            string C_Producer = ProducerTextBox.Text;
            string Director = DirectorTextBox.Text;
            string Telephone = TelephoneTextBox.Text;
            string Email = EmailTextBox.Text;
            string Review = ReviewTextBox.Text;
            string Address = AddressTextBox.Text;
            Producer producer = new Producer(C_Producer, Director, Telephone, Email, Review, Address);
            producerDao.InsertProducer(producer);
            Close();
        }
    }
}
