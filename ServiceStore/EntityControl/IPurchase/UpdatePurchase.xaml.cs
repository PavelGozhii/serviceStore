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

namespace ServiceStore.EntityControl.IPurchase
{
    /// <summary>
    /// Логика взаимодействия для UpdatePurchase.xaml
    /// </summary>
    public partial class UpdatePurchase : Window
    {
        public UpdatePurchase(Purchase purchase, SqlConnection connection)
        {
            InitializeComponent();
        }
    }
}
