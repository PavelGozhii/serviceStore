using ServiceStore.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ServiceStore.Dao
{
    class PurchaseServiceDao
    {

        SqlConnection connection = null;

        private readonly string INSERT_SERVICEPURCHASE = "INSERT INTO ServicePurchase(C_Purchase, C_Service) VALUES(@Purchase , @Service);";
        private readonly string SELECT_SERVICE = "SELECT * FROM ServicePurchase";
        private readonly string SELECT_SERVICE_BY_PURCHASE = "SELECT * FROM ServicePurchase WHERE C_Purchase = @ID";

        public PurchaseServiceDao(SqlConnection connection)
        {
            this.connection = connection;
        }
        
        public bool InsertPurchaseService(PurchaseService purchaseService)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(INSERT_SERVICEPURCHASE, connection);
                command.Parameters.AddWithValue("@Purchase", purchaseService.Purchase);
                command.Parameters.AddWithValue("@Service", purchaseService.Service);
                command.ExecuteReader();
                return true;
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return false;
        }
        
        public List<PurchaseService> SelectAllServicePurchase()
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            SqlDataReader dataReader;
            List<PurchaseService> purchaseServices = new List<PurchaseService>();
            try
            {
                connection.Open();
                command = new SqlCommand(SELECT_SERVICE, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string C_Purchase = dataReader.GetValue(0).ToString();
                    string C_Service = dataReader.GetValue(1).ToString();
                    PurchaseService purchaseService = new PurchaseService(C_Purchase,C_Service);
                    purchaseServices.Add(purchaseService);
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection.Close();
            }

            return purchaseServices;
        }

        public PurchaseService SelectServicePurchasById(string Id)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            SqlDataReader dataReader;
            PurchaseService purchaseService = null;
            try
            {
                connection.Open();
                command = new SqlCommand(SELECT_SERVICE_BY_PURCHASE, connection);
                command.Parameters.AddWithValue("@ID", Id);
                dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    string C_Purchase = dataReader.GetString(0);
                    string C_Service = dataReader.GetString(1);
                    purchaseService = new PurchaseService(C_Purchase, C_Service);
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return purchaseService;
        }


    }
}
