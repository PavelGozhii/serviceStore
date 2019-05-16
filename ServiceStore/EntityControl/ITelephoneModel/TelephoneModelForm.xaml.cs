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

namespace ServiceStore.EntityControl.ITelephoneModel
{
    /// <summary>
    /// Логика взаимодействия для TelephoneModelForm.xaml
    /// </summary>
    public partial class TelephoneModelForm : Window
    {
        SqlConnection connection;
        TelephoneModelDao telephoneModelDao;
        TrademarkDao trademarkDao;

        public TelephoneModelForm(SqlConnection connection)
        {
            this.connection = connection;
            telephoneModelDao = new TelephoneModelDao(connection);
            trademarkDao = new TrademarkDao(connection);
            InitializeComponent();
            ActionBtn.Content = "Create";
            List<Trademark> trademarks = trademarkDao.SelectAllTrademark();
            string[] trademarkNames = new string[trademarks.Count];
            for (int i = 0; i < trademarks.Count; i++) {
                trademarkNames[i] = trademarks[i].C_Trademark;
            }
            TrademarkComboBox.ItemsSource = trademarkNames;
        }
        
        public TelephoneModelForm(SqlConnection connection, TelephoneModel telephoneModel) {
            this.connection = connection;
            telephoneModelDao = new TelephoneModelDao(connection);
            trademarkDao = new TrademarkDao(connection);
            InitializeComponent();
            List<Trademark> trademarks = trademarkDao.SelectAllTrademark();
            string[] trademarkNames = new string[trademarks.Count];
            for (int i = 0; i < trademarks.Count; i++)
            {
                trademarkNames[i] = trademarks[i].C_Trademark;
            }
            TrademarkComboBox.ItemsSource = trademarkNames;
            ActionBtn.Content = "Update";
            TelephoneModelTextBox.Visibility = Visibility.Hidden;
            TelephoneModelTextBlock.Visibility = Visibility.Hidden;
            TelephoneModelTextBox.Text = telephoneModel.C_TelephoneModel;
            NameTextBox.Text = telephoneModel.Name;
            CategoryTextBox.Text = telephoneModel.Category;
            OperatingSystemTextBox.Text = telephoneModel.OperatingSystem;
            MemorySizeTextBox.Text = telephoneModel.MemorySize.ToString();
            ProccessorTextBox.Text = telephoneModel.Processor;
            TrademarkComboBox.Text = telephoneModel.C_Trademark;
        }

        private void ActionBtn_Click(object sender, RoutedEventArgs e)
        {
            string C_TelephoneModel = TelephoneModelTextBox.Text;
            string Name = NameTextBox.Text;
            string Category = CategoryTextBox.Text;
            string OperatingSystem = OperatingSystemTextBox.Text;
            int MemorySize = int.Parse(MemorySizeTextBox.Text.ToString());
            string Processor = ProccessorTextBox.Text;
            string Trademark = TrademarkComboBox.Text;
            TelephoneModel telephoneModel = new TelephoneModel(C_TelephoneModel, Name, Category, OperatingSystem, MemorySize, Processor, Trademark);
            if (ActionBtn.Content.ToString().Equals("Create")){
                telephoneModelDao.InsertTelephoneModel(telephoneModel);
            }else if (ActionBtn.Content.ToString().Equals("Update")){
                telephoneModelDao.UpdateTelephoneModel(telephoneModel, C_TelephoneModel);
            }
            Close();
        }
    }
}
