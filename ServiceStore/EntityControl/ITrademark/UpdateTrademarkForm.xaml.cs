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
    /// Логика взаимодействия для UpdateTrademarkForm.xaml
    /// </summary>
    public partial class UpdateTrademarkForm : Window
    {
        SqlConnection connection;
        TrademarkDao trademarkDao;
        ProducerDao producerDao;

        public UpdateTrademarkForm(SqlConnection connection, Trademark trademark)
        {
            InitializeComponent();
            this.connection = connection;
            trademarkDao = new TrademarkDao(connection);
            producerDao = new ProducerDao(connection);
            LastId.Text = trademark.C_Trademark;
            TradeMarkTextBox.Text = trademark.C_Trademark;
            NameTextBox.Text = trademark.Name;
            ReviewTextBox.Text = trademark.Review;
            List<Producer> producers = producerDao.SelectAllProducers();
            string[] producerSource = new string[producers.Count];
            for (int i = 0; i < producers.Count; i++)
            {
                producerSource[i] = producers[i].C_Producer;
            }
            ProducerComboBox.ItemsSource = producerSource;
            ProducerComboBox.Text = trademark.C_Producer;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            string LastId = TradeMarkTextBox.Text;
            string C_Trademark = TradeMarkTextBox.Text;
            string Name = NameTextBox.Text;
            string Review = ReviewTextBox.Text;
            string Producer = ProducerComboBox.Text;
            Trademark trademark = new Trademark(C_Trademark, Name, Review, Producer);
            trademarkDao.updateTrademark(trademark, LastId);
            Close();
        }
    }
}
