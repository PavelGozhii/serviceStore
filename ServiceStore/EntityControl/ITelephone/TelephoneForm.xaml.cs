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

namespace ServiceStore.EntityControl.ITelephone
{
    /// <summary>
    /// Логика взаимодействия для TelephoneForm.xaml
    /// </summary>
    public partial class TelephoneForm : Window
    {
        SqlConnection connection;
        TelephoneDao telephoneDao;
        TelephoneModelDao telephoneModelDao;
        public TelephoneForm(SqlConnection connection)
        {
            this.connection = connection;
            telephoneDao = new TelephoneDao(connection);
            telephoneModelDao = new TelephoneModelDao(connection);
            InitializeComponent();
            List<TelephoneModel> telephoneModels = telephoneModelDao.selectAllTelephomeModel();
            string[] telephoneModelNames = new string[telephoneModels.Count];
            for(int i = 0; i < telephoneModels.Count; i++)
            {
                telephoneModelNames[i] = telephoneModels[i].C_TelephoneModel;
            }
            TelephoneModelComboBox.ItemsSource = telephoneModelNames;
        }

        public TelephoneForm(SqlConnection connection, Telephone telephone)
        {
            this.connection = connection;
            telephoneDao = new TelephoneDao(connection);
            telephoneModelDao = new TelephoneModelDao(connection);
            InitializeComponent();
            List<TelephoneModel> telephoneModels = telephoneModelDao.selectAllTelephomeModel();
            string[] telephoneModelNames = new string[telephoneModels.Count];
            for (int i = 0; i < telephoneModels.Count; i++)
            {
                telephoneModelNames[i] = telephoneModels[i].C_TelephoneModel;
            }
            TelephoneModelComboBox.ItemsSource = telephoneModelNames;
            TelephoneTextBox.Text = telephone.IMEI;
            LastIdTextBox.Text = telephone.IMEI;
            TelephoneModelComboBox.Text = telephone.C_TelephoneModel;
            ActionBtn.Content = "Update";
            
        }

        private void ActionBtn_Click(object sender, RoutedEventArgs e)
        {
            string IMEI = TelephoneTextBox.Text;
            string telephoneModel = TelephoneModelComboBox.Text;
            Telephone telephone = new Telephone(IMEI, telephoneModel);
            if (ActionBtn.Content.ToString().Equals("Create"))
            {
                telephoneDao.InsertTelephone(telephone);
            }else if (ActionBtn.Content.ToString().Equals("Update"))
            {
                string LastId = LastIdTextBox.Text;
                telephoneDao.UpdateTelephone(telephone, LastId);
            }
            Close();
        }
    }
}
