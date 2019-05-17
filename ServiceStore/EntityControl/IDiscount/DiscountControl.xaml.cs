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
    }
}
