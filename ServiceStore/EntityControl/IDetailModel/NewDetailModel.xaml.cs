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

namespace ServiceStore.EntityControl.IDetailModel
{
    /// <summary>
    /// Логика взаимодействия для NewDetailModel.xaml
    /// </summary>
    public partial class NewDetailModel : Window
    {
        SqlConnection connection;
        DetailModelDao detailModelDao;
        TelephoneModelDao telephoneModelDao;
        DetailDao detailDao;

        public NewDetailModel(SqlConnection connection)
        {
            this.connection = connection;
            detailModelDao = new DetailModelDao(connection);
            telephoneModelDao = new TelephoneModelDao(connection);
            detailDao = new DetailDao(connection);
            InitializeComponent();
            List<TelephoneModel> telephoneModels = telephoneModelDao.selectAllTelephomeModel();
            List<Detail> details = detailDao.SelectAllDetails();
            string[] telephoneModelsName = new string[telephoneModels.Count];
            string[] detailsString = new string[details.Count];
            for(int i = 0; i < telephoneModels.Count; i++)
            {
                telephoneModelsName[i] = telephoneModels[i].C_TelephoneModel;
            }
            for (int i = 0; i < details.Count; i++)
            {
                detailsString[i] = details[i].C_Detail;
            }
            DetailComboBox.ItemsSource = detailsString;
            TelephoneModelComboBox.ItemsSource = telephoneModelsName;
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            string C_DetailModel = DetailModelTextBox.Text;
            double Price = double.Parse(PriceTextBox.Text);
            string C_TelephoneModel = DetailComboBox.Text;
            string C_Detail = DetailComboBox.Text;
            DetailModel detailModel = new DetailModel(C_DetailModel, Price, C_TelephoneModel, C_Detail);
            detailModelDao.InsertDetailModel(detailModel);
            Close();
        }
    }
}
