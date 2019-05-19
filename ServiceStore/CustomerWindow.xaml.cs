using ServiceStore.Dao;
using ServiceStore.EntityControl.IDiscount;
using ServiceStore.EntityControl.ITelephone;
using ServiceStore.EntityControl.IPriceList;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ServiceStore.EntityControl;
using ServiceStore.EntityControl.ITelephoneList;
using ServiceStore.EntityControl.MyPurchase;

namespace ServiceStore
{
    /// <summary>
    /// Логика взаимодействия для CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        SqlConnection connection = DBConnection.Connect();
        public CustomerWindow(SqlConnection connection)
        {
            this.connection = connection;
            InitializeComponent();
        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);
            DBConnection.id = "Customer";
            DBConnection.password = "Customer";
            switch (index)
            {
                case 0:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new MyPurchaseControl(connection));
                    break;
                case 1:                   
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new PriceListControl(connection));
                    break;
                case 2:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new DiscountControl(connection));
                    break;
                case 3:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new NewRapairControl(connection));
                    break;
                case 4:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new TelephoneListControl(connection));
                    break;
                default:
                    break;
            }
        }

        private void MoveCursorMenu(int index)
        {
            TrainsitionigContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, (10 + (50 * index)), 0, 0);
        }

        private void FacebookBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/profile.php?id=100007208143771");
        }

        private void LinkedinBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.linkedin.com/in/гожий-павло-569754160/");
        }

        private void GithubBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/PavelGozhii");
        }
    }
}