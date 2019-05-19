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
    /// Логика взаимодействия для UpdateDetailForm.xaml
    /// </summary>
    public partial class UpdateDetailForm : Window
    {
        SqlConnection connection;
        DetailDao detailDao;

        public UpdateDetailForm(SqlConnection connection, Detail detail)
        {
            this.connection = connection;
            detailDao = new DetailDao(connection);
            InitializeComponent();
            DetailTextBox.Text = detail.C_Detail;
            TypeTextBox.Text = detail.Type;
            LastIdTextBox.Text = detail.C_Detail;
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            string LastId = LastIdTextBox.Text;
            string C_Detail = DetailTextBox.Text;
            string Type = TypeTextBox.Text;
            Detail detail = new Detail(C_Detail, Type);
            detailDao.UpdateDetail(detail, LastId);
            Close();
        }
    }
}
