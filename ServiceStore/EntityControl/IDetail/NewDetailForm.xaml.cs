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

namespace ServiceStore.EntityControl.IDetail
{
    /// <summary>
    /// Логика взаимодействия для NewDetailForm.xaml
    /// </summary>
    public partial class NewDetailForm : Window
    {
        SqlConnection connection;
        DetailDao detailDao;

        public NewDetailForm(SqlConnection connection)
        {
            this.connection = connection;
            detailDao = new DetailDao(connection);
            InitializeComponent();
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            string C_Detail = DetailTextBox.Text;
            string Type = TypeTextBox.Text;
            Detail detail = new Detail(C_Detail, Type);
            detailDao.InsertDetail(detail);
            Close();
        }
    }
}
