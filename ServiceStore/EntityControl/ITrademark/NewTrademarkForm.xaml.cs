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

namespace ServiceStore.EntityControl.ITrademark
{
    /// <summary>
    /// Логика взаимодействия для NewTrademarkForm.xaml
    /// </summary>
    public partial class NewTrademarkForm : Window
    {
        SqlConnection connection;
        TrademarkDao trademarkDao;
        ProducerDao producerDao;
        public NewTrademarkForm(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            trademarkDao = new TrademarkDao(connection);
            producerDao = new ProducerDao(connection);
            List<Producer> producers = producerDao.SelectAllProducers();
            string[] producerSource = new string[producers.Count];
            for(int i = 0; i < producers.Count; i++)
            {
                producerSource[i] = producers[i].C_Producer;
            }
            ProducerComboBox.ItemsSource = producerSource;
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            string C_Trademark = TradeMarkTextBox.Text;
            string Name = NameTextBox.Text;
            string Review = ReviewTextBox.Text;
            string Producer = ProducerComboBox.Text;
            Trademark trademark = new Trademark(C_Trademark, Name, Review, Producer);
            trademarkDao.InsertTrademark(trademark);
            Close();
        }
    }
}
