using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string connectionString = @"Data Source=DESKTOP-C4CO08G; Initial Catalog = servicestore;User id=Admin;Password=Admin;";
            //     "Data Source=DESKTOP-C4CO08G;Initial Catalog=servicestore;User Id=Admin;Password=Admin;";

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

        public static bool Disconnect()
        {
            try
            {
                Instance.Close();
                return true;
            }catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }


    }
}
