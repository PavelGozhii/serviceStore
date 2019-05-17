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

namespace ServiceStore.EntityControl.IDetailModel
{
    /// <summary>
    /// Логика взаимодействия для DatailModelControl.xaml
    /// </summary>
    public partial class DatailModelControl : UserControl
    {
        SqlConnection connection;
        DetailModelDao detailModelDao;

        public DatailModelControl(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            detailModelDao = new DetailModelDao(connection);
            DataGrid();
            if (DBConnection.id.Equals("Seller"))
            {
                updateBtn.Visibility = Visibility.Hidden;
                deleteBtn.Visibility = Visibility.Hidden;
                Add.Visibility = Visibility.Hidden;
                DetailModel.Width = 220;
                Price.Width = 80;
                TelephoneModel.Width = 140;
                Detail.Width = 130;
            }
        }

        public void DataGrid()
        {
            grdDetailModel.ItemsSource = detailModelDao.SelectAllDetailModel();
        }

        public void DataGrid(List<DetailModel> detailModels)
        {
            grdDetailModel.ItemsSource = detailModels;
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            string Id = (grdDetailModel.SelectedItem as DetailModel).C_DetailModel;
            DetailModel detailModel = detailModelDao.SelectDetailModelById(Id);
            UpdateDetailModel updateDetailModel = new UpdateDetailModel(connection, detailModel);
            updateDetailModel.ShowDialog();
            DataGrid();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            string ID = (grdDetailModel.SelectedItem as DetailModel).C_DetailModel;
            detailModelDao.DeleteDetailModel(ID);
            DataGrid();
        }

        private void SearchTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            searchTextBox.Clear();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NewDetailModel newDetailModel = new NewDetailModel(connection);
            newDetailModel.ShowDialog();
            DataGrid();
        }
    }
}
