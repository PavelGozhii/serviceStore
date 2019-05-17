using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ServiceStore.Dao
{
    class DBConnection
    {
       public static string id { get; set; }
       public static string password { get; set; }
       public static SqlConnection Instance { get; set; }


       public DBConnection(string id, string password)
       {
            DBConnection.id = id;
            DBConnection.password = password;         
       }


        public static SqlConnection Connect()
        {
            string connectionString = string.Format(@"Data Source=DESKTOP-C4CO08G; Initial Catalog = servicestore;User id={0};Password={1};",id, password);
            SqlConnection cnn = new SqlConnection(connectionString);
            try {                                                
            cnn.Open();          
                Console.WriteLine("ConnectionOpen!");
                cnn.Close();
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return cnn;
        }

        public static SqlConnection Connect(string id, string password)
        {
            string connectionString = string.Format(@"Data Source=DESKTOP-C4CO08G; Initial Catalog = servicestore;User id={0};Password={1};", id, password);
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("ConnectionOpen!");
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return connection;
        }

        public static bool Disconnect()
        {
            try
            {
                Instance.Close();
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }

    }
}
