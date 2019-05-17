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

namespace ServiceStore.EntityControl.IDiscount
{
    /// <summary>
    /// Логика взаимодействия для NewDiscountForm.xaml
    /// </summary>
    public partial class NewDiscountForm : Window
    {
        SqlConnection connection;
        DiscountDao discountDao;
 
        public NewDiscountForm(SqlConnection connection)
        {
            this.connection = connection;
            discountDao = new DiscountDao(connection);
            InitializeComponent();
            int[] days = new int[31];
            int[] months = new int[12];
            int[] years = new int[60];
            for (int i = 0; i < 31; i++)
            {
                days[i] = i + 1;
            }
            for (int i = 0; i < 12; i++)
            {
                months[i] = i + 1;
            }
            for (int i = 0; i < 60; i++)
            {
                years[i] = i + 1960;
            }
            ComboBoxDayStarting.ItemsSource = days;
            ComboBoxMonthStarting.ItemsSource = months;
            ComboBoxYearStarting.ItemsSource = years;
            ComboBoxDayEnding.ItemsSource = days;
            ComboBoxMonthEnding.ItemsSource = months;
            ComboBoxYearEnding.ItemsSource = years;
            CreateBtn.Content = "Create";
            CreateBtn.ToolTip = "Create discount";
        }

        public NewDiscountForm(SqlConnection connection, Discount discount)
        {
            this.connection = connection;
            discountDao = new DiscountDao(connection);
            InitializeComponent();
            int[] days = new int[31];
            int[] months = new int[12];
            int[] years = new int[60];
            for (int i = 0; i < 31; i++)
            {
                days[i] = i + 1;
            }
            for (int i = 0; i < 12; i++)
            {
                months[i] = i + 1;
            }
            for (int i = 0; i < 60; i++)
            {
                years[i] = i + 1960;
            }
            ComboBoxDayStarting.ItemsSource = days;
            ComboBoxMonthStarting.ItemsSource = months;
            ComboBoxYearStarting.ItemsSource = years;
            ComboBoxDayEnding.ItemsSource = days;
            ComboBoxMonthEnding.ItemsSource = months;
            ComboBoxYearEnding.ItemsSource = years;
            CreateBtn.Content = "Update";
            CreateBtn.ToolTip = "Update discount";
            NameTextBox.Text = discount.Name;
            DiscountTextBox.Text = discount.C_Discount;
            DiscountTextBox.Visibility = Visibility.Hidden;
            DiscountTextBlock.Visibility = Visibility.Hidden;   
            SizeTextBox.Text = discount.C_Discount;
            DateTime dateTimeStarting = DateTime.Parse(discount.Starting);
            DateTime dateTimeEnding = DateTime.Parse(discount.Ending);
            ComboBoxDayStarting.Text = dateTimeStarting.Day.ToString();
            ComboBoxMonthStarting.Text = dateTimeStarting.Month.ToString();
            ComboBoxYearStarting.Text = dateTimeStarting.Year.ToString();
            ComboBoxDayEnding.Text = dateTimeEnding.Day.ToString();
            ComboBoxMonthEnding.Text = dateTimeEnding.Month.ToString();
            ComboBoxYearEnding.Text = dateTimeEnding.Year.ToString();
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            string C_Discount = DiscountTextBox.Text.ToString();
            int Size = int.Parse(SizeTextBox.Text.ToString());
            string Name = NameTextBox.Text.ToString();
            int dayStarting = int.Parse(ComboBoxDayStarting.Text.ToString());
            int monthStarting = int.Parse(ComboBoxMonthStarting.Text.ToString());
            int yearStarting = int.Parse(ComboBoxYearStarting.Text.ToString());
            int dayEnding = int.Parse(ComboBoxDayEnding.Text.ToString());
            int monthEnding = int.Parse(ComboBoxMonthEnding.Text.ToString());
            int yearEnding = int.Parse(ComboBoxYearEnding.Text.ToString());
            string strDateStarting = dayStarting + "-" + monthStarting + "-" + yearStarting;
            string strDateEnding = dayEnding + "-" + monthEnding + "-" + yearEnding;
            Discount discount = new Discount(C_Discount, Size, Name, strDateStarting, strDateEnding);
            if (CreateBtn.Content.ToString().Equals("Create"))
            {
                discountDao.InsertDiscount(discount);
            }else if (CreateBtn.Content.ToString().Equals("Update"))
            {
                discountDao.UpdateDiscount(discount, C_Discount);
            }
            Close();
        }
    }
}
