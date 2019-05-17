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

namespace ServiceStore.EntityControl.ITelephone
{
    /// <summary>
    /// Логика взаимодействия для TelephoneControl.xaml
    /// </summary>
    public partial class TelephoneControl : UserControl
    { 
        SqlConnection connection;
        TelephoneDao telephoneDao;
        public TelephoneControl(SqlConnection connection)
        {
            this.connection = connection;
            telephoneDao = new TelephoneDao(connection);
            InitializeComponent();
            DataGrid();
            if (DBConnection.id.Equals("Seller"))
            {
                DeleteBtn.Visibility = Visibility.Hidden;
                IMEI.Width = 240;
                Telephnemodel.Width = 240;
            }
        }

        public void DataGrid()
        {
            grdTelephone.ItemsSource = telephoneDao.SelectAllTelephone();
        }

        public void DataGrid(List<Telephone> telephones)
        {
            grdTelephone.ItemsSource = telephones; 
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            string id = (grdTelephone.SelectedItem as Telephone).IMEI;
            TelephoneForm telephoneForm = new TelephoneForm(connection, telephoneDao.SelectTelephoneById(id));
            telephoneForm.ShowDialog();
            DataGrid();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            string id = (grdTelephone.SelectedItem as Telephone).IMEI;
            telephoneDao.DeleteTelephone(id);
            DataGrid();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SearchTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            searchTextBox.Clear();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            TelephoneForm telephoneForm = new TelephoneForm(connection);
            telephoneForm.ShowDialog();
            DataGrid();
        }
    }
}
