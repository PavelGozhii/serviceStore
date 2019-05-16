using ServiceStore.Dao;
using ServiceStore.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ServiceStore.EntityControl.IDetail
{
    /// <summary>
    /// Логика взаимодействия для DetailControl.xaml
    /// </summary>
    public partial class DetailControl : UserControl
    {
        SqlConnection connection;
        DetailDao detailDao;
        public DetailControl(SqlConnection connection)
        {
            this.connection = connection;
            detailDao = new DetailDao(connection);
            InitializeComponent();
            DataGrid();
        }

        public void DataGrid()
        {
            grdDetail.ItemsSource = detailDao.SelectAllDetails();
        }

        public void DataGrid(List<Detail> details)
        {
            grdDetail.ItemsSource = details;
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            string Id = (grdDetail.SelectedItem as Detail).C_Detail;
            Detail detail = detailDao.SelectDetailById(Id);
            UpdateDetailForm detailForm = new UpdateDetailForm(connection, detail);
            detailForm.ShowDialog();
            DataGrid();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            string Id = (grdDetail.SelectedItem as Detail).C_Detail;
            detailDao.DeleteDetail(Id);
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
            NewDetailForm detailForm = new NewDetailForm(connection);
            detailForm.ShowDialog();
            DataGrid();
        }
    }
}
