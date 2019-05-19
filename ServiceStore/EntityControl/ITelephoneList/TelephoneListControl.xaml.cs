using ServiceStore.Dao;
using ServiceStore.Model;
using ServiceStore.Services;
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

namespace ServiceStore.EntityControl.ITelephoneList
{
    /// <summary>
    /// Логика взаимодействия для TelephoneListControl.xaml
    /// </summary>
    public partial class TelephoneListControl : UserControl
    {
        SqlConnection connection;
        TelephoneInfoDao telephoneInfoDao;
        List<TelephoneInfo> telephoneInfos;

        public TelephoneListControl(SqlConnection connection)
        {            
            InitializeComponent();            
            this.connection = connection;
            telephoneInfoDao = new TelephoneInfoDao(connection);
            telephoneInfos = telephoneInfoDao.SelectAllTelephoneInfo();
            DataGrid();
        }

        private void DataGrid()
        {
            grdTelephoneInfo.ItemsSource = telephoneInfos;
        }

        private void DataGrid(List<TelephoneInfo> telephoneInfos)
        {
            grdTelephoneInfo.ItemsSource = telephoneInfos;
        }

        private void SearchTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            searchTextBox.Clear();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<TelephoneInfo> input = new List<TelephoneInfo>();
            for (int i = 0; i < telephoneInfos.Count; i++)
            {
                if (telephoneInfos[i].Name.Contains(searchTextBox.Text))
                {
                    input.Add(telephoneInfos[i]);
                }
            }
            DataGrid(input);
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            PdfConvert.FullNotApp(grdTelephoneInfo, "TelephoneList"); 
        }
    }
}
