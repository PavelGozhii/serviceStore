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

namespace ServiceStore.EntityControl.ITrademark
{
    /// <summary>
    /// Логика взаимодействия для TrademarkControl.xaml
    /// </summary>
    public partial class TrademarkControl : UserControl
    {
        SqlConnection connection;
        TrademarkDao trademarkkDao;
        public TrademarkControl(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            trademarkkDao = new TrademarkDao(connection);
            DataGrid();
            if (DBConnection.id.Equals("Seller"))
            {
                UpdateBtn.Visibility = Visibility.Hidden;
                DeleteBtn.Visibility = Visibility.Hidden;
                Add.Visibility = Visibility.Hidden;
                Name.Width = 130;
                Review.Width = 280;
                Producer.Width = 230;
            }
        }

        public void DataGrid()
        {
            grdTrademark.ItemsSource = trademarkkDao.SelectAllTrademark();
        }

        public void DataGrid(List<Trademark> trademarks)
        {
            grdTrademark.ItemsSource = trademarks;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NewTrademarkForm newTrademark = new NewTrademarkForm(connection);
            newTrademark.ShowDialog();
            DataGrid();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            string Id = (grdTrademark.SelectedItem as Trademark).C_Trademark;
            Trademark trademark = trademarkkDao.SelectTrademarkById(Id);
            UpdateTrademarkForm updateTrademark = new UpdateTrademarkForm(connection, trademark);
            updateTrademark.ShowDialog();
            DataGrid();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            string Id = (grdTrademark.SelectedItem as Trademark).C_Trademark;
            trademarkkDao.DeleteTrademark(Id);
            DataGrid();
        }

        private void SearchTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            searchTextBox.Clear();
        }
    }
}
