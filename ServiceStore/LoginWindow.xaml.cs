using ServiceStore.Dao;
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

namespace ServiceStore
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            string id = LoginTextBox.Text;
            string Password = PasswordTextBox.Password;
            DBConnection dbconnection = new DBConnection(id, Password);
            SqlConnection connection = DBConnection.Connect(id, Password);
            DBConnection.id = id;
            DBConnection.password = Password;
            {
                if (DBConnection.id.Equals("Admin") && DBConnection.password.Equals("Admin"))
                {
                    Hide();
                    MainWindow mainWindow = new MainWindow(connection);
                    mainWindow.ShowDialog();
                    PasswordTextBox.Clear();
                    Show();
                }
                else if (DBConnection.id.Equals("Seller") && DBConnection.password.Equals("Seller"))
                {
                    Hide();
                    SellerWindow sellerWindow = new SellerWindow(connection);
                    sellerWindow.ShowDialog();
                    PasswordTextBox.Clear();
                    Show();
                }
                else if (DBConnection.id.Equals("Customer") && DBConnection.password.Equals("Customer"))
                {
                    Hide();
                    CustomerWindow customerWindow = new CustomerWindow(connection);
                    customerWindow.ShowDialog();
                    PasswordTextBox.Clear();
                    Show();
                }
                else
                {
                    MessageBox.Show("Incorrect login or password");
                }
            }
        }
    }
}
