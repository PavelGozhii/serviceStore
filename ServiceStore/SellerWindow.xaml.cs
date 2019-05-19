using ServiceStore.Dao;
using ServiceStore.EntityControl.ICustomer;
using ServiceStore.EntityControl.IDetail;
using ServiceStore.EntityControl.IDetailModel;
using ServiceStore.EntityControl.IDiscount;
using ServiceStore.EntityControl.IProducer;
using ServiceStore.EntityControl.IService;
using ServiceStore.EntityControl.ITelephone;
using ServiceStore.EntityControl.ITelephoneModel;
using ServiceStore.EntityControl.ITrademark;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ServiceStore
{
    /// <summary>
    /// Логика взаимодействия для SellerWindow.xaml
    /// </summary>
    public partial class SellerWindow : Window
    {
        SqlConnection connection = DBConnection.Connect();
        public SellerWindow(SqlConnection connection)
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
            DBConnection.id = "Seller";
            DBConnection.password = "Seller";
            switch (index)
            {
                case 0:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new ServiceContr(connection));
                    break;
                case 1:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new UserControlInicio(connection));
                    break;
                case 2:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new DiscountControl(connection));
                    break;
                case 3:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new TelephoneControl(connection));
                    break;
                case 4:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new TelephoneModelControl(connection));
                    break;
                case 5:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new DetailControl(connection));
                    break;
                case 6:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new DatailModelControl(connection));
                    break;
                case 7:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new TrademarkControl(connection));
                    break;
                case 8:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new ProducerControl(connection));
                    break;
                case 9:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new CustomerControl(connection));
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

