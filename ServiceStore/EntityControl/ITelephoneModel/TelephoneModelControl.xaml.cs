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
            if (DBConnection.id.Equals("Seller"))
            {
                UpdateBtn.Visibility = Visibility.Hidden;
                DeleteBtn.Visibility = Visibility.Hidden;
                Add.Visibility = Visibility.Hidden;
                Name.Width = 170;
                Category.Width = 90;
                OS.Width = 80;
                Memory.Width = 70;
                Processor.Width = 170;
                Trademark.Width = 110;
            }

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
            List<TelephoneModel> telephoneModels = telephoneModelDao.selectAllTelephomeModel();
            List<TelephoneModel> input = new List<TelephoneModel>();
            for (int i = 0; i < telephoneModels.Count; i++)
            {
                if (telephoneModels[i].Name.Contains(searchTextBox.Text))
                {
                    input.Add(telephoneModels[i]);
                }
            }
            DataGrid(input);
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
