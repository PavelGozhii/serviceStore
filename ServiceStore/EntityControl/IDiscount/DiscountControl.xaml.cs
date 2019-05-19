using ServiceStore.Dao;
using ServiceStore.Model;
using ServiceStore.Services;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ServiceStore.EntityControl.IDiscount
{
    /// <summary>
    /// Логика взаимодействия для DiscountControl.xaml
    /// </summary>
    public partial class DiscountControl : UserControl
    {
        SqlConnection connection;
        DiscountDao discountDao;

        public DiscountControl(SqlConnection connection)
        {
            InitializeComponent();
            discountDao = new DiscountDao(connection);
            this.connection = connection;
            GridData();
            if (DBConnection.id.Equals("Seller"))
            {
                deleteBtn.Visibility = Visibility.Hidden;
                Name.Width = 220;
                Size.Width = 90;
                Starting.Width = 100;
                Ending.Width = 100;              
            }
            if (DBConnection.id.Equals("Customer"))
            {
                deleteBtn.Visibility = Visibility.Hidden;
                UpdateBtn.Visibility = Visibility.Hidden;
                Add.Visibility = Visibility.Hidden;
                Name.Width = 250;
                Size.Width = 120;
                Starting.Width = 120;
                Ending.Width = 120;
                Report.Visibility = Visibility.Visible;
            }


        }

        public void GridData()
        {
            grdDiscount.ItemsSource = discountDao.SelectAllDiscounts();
        }

        public void GridData(List<Discount> discounts)
        {
            grdDiscount.ItemsSource = discounts;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            string id = (grdDiscount.SelectedItem as Discount).C_Discount;
            discountDao.DeleteDiscount(id);
            GridData();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            string id = (grdDiscount.SelectedItem as Discount).C_Discount;
            NewDiscountForm discountForm = new NewDiscountForm(connection, discountDao.SelectDiscountByCode(id));
            discountForm.ShowDialog();
            GridData();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Discount> discounts = discountDao.SelectAllDiscounts();
            List<Discount> input = new List<Discount>();
            for (int i = 0; i < discounts.Count; i++)
            {
                if (discounts[i].Name.Contains(searchTextBox.Text))
                {
                    input.Add(discounts[i]);
                }
            }
            GridData(input);
        }

        private void SearchTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            searchTextBox.Clear();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NewDiscountForm discountForm = new NewDiscountForm(connection);
            discountForm.ShowDialog();
            GridData();
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            PdfConvert.FullNotApp(grdDiscount, "DiscontList");
        }
    }
}
