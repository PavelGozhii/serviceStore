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

namespace ServiceStore.EntityControl.ITelephoneModel
{
    /// <summary>
    /// Логика взаимодействия для TelephoneModelControl.xaml
    /// </summary>
    public partial class TelephoneModelControl : UserControl
    {
        SqlConnection connection;
        TelephoneModelDao telephoneModelDao;

        public TelephoneModelControl(SqlConnection connection)
        {
            InitializeComponent();
            telephoneModelDao = new TelephoneModelDao(connection);
            this.connection = connection;
            DataGrid();

        }

        public void DataGrid()
        {
            grdTelephoneModel.ItemsSource = telephoneModelDao.selectAllTelephomeModel();
        }

        public void DataGrid(List<TelephoneModel> telephoneModels)
        {
            grdTelephoneModel.ItemsSource = telephoneModels;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            string id = (grdTelephoneModel.SelectedItem as TelephoneModel).C_TelephoneModel;
            telephoneModelDao.DeleteTelephoneModel(id);
            DataGrid();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            TelephoneModelForm telephoneModelForm = new TelephoneModelForm(connection);
            telephoneModelForm.ShowDialog();
            DataGrid();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            string id = (grdTelephoneModel.SelectedItem as TelephoneModel).C_TelephoneModel;
            TelephoneModel telephoneModel = telephoneModelDao.SelectTelephoneModelById(id);
            TelephoneModelForm telephoneModelForm = new TelephoneModelForm(connection, telephoneModel);
            telephoneModelForm.ShowDialog();
            DataGrid();
        }

        private void SearchTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            searchTextBox.Clear();
        }
    }
}
