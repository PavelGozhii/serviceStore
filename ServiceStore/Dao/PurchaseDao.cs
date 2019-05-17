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
    class PurchaseDao
    {
        SqlConnection connection = null;
        private static readonly string SELECT_ALL_PURCHASES = "SELECT * FROM purchase";
        private static readonly string SELECT_PURCHASE_BY_ID =
            "SELECT * FROM purchase WHERE C_Purchase = @ID";
        private static readonly string INSERT_PURCHASE =
            "INSERT INTO purchase (C_Purchase, Status, DatePuchase, C_Customer, IMEI) " +
            "VALUES (@ID, @Status, @Date, @Customer, @IMEI);";
        private static readonly string DELETE_PURCHASE =
            "DELETE FROM purchase WHERE C_Purchase = @ID";
        private static readonly string UPDATE_PURCHASE =
            "UPDATE purchase SET " +
            "C_Purchase = @newID, Status = @Status, DatePuchase = @Date," +
            " C_Customer = @Customer, IMEI = @IMEI " +
            "WHERE C_Purchase = @ID;";

        public PurchaseDao(SqlConnection connection)
        {
            this.connection = connection;
        }

        public List<Purchase> SelectAllPurchses()
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            SqlDataReader dataReader;
            List<Purchase> purchases = new List<Purchase>();
            try
            {
                connection.Open();
                command = new SqlCommand(SELECT_ALL_PURCHASES, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string C_Purchse = dataReader.GetValue(0).ToString();
                    string status = dataReader.GetValue(1).ToString();
                    string datePurchase = DateTime.Parse(dataReader.GetValue(2).ToString()).ToShortDateString();
                    string C_Customer = dataReader.GetValue(3).ToString();
                    string IMEI = dataReader.GetValue(4).ToString();
                    Purchase purchase = new Purchase(C_Purchse, status, datePurchase, C_Customer, IMEI);
                    purchases.Add(purchase);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                DBConnection.Disconnect();
            }
            return purchases;
        }

        public Purchase SelectPurchaseById(string C_Purchase)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            SqlDataReader dataReader;
            Purchase purchase = null;
            try
            {
                connection.Open();
                command = new SqlCommand(SELECT_PURCHASE_BY_ID, connection);
                command.Parameters.AddWithValue("@ID", C_Purchase);
                dataReader = command.ExecuteReader();
                if(dataReader.Read())
                {
                    string C_Purchse = dataReader.GetValue(0).ToString();
                    string status = dataReader.GetValue(1).ToString();
                    string datePurchase = dataReader.GetValue(2).ToString();
                    string C_Customer = dataReader.GetValue(3).ToString();
                    string IMEI = dataReader.GetValue(4).ToString();
                    purchase = new Purchase(C_Purchse, status, datePurchase, C_Customer, IMEI);
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                DBConnection.Disconnect();
            }
            return purchase;
        }

        public bool InsertPurchase(Purchase purchase)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(INSERT_PURCHASE, connection);
                command.Parameters.AddWithValue("@ID", purchase.C_Purchase);
                command.Parameters.AddWithValue("@Status", purchase.Status);
                command.Parameters.AddWithValue("@Date", purchase.DatePurchase);
                command.Parameters.AddWithValue("@Customer", purchase.C_Customer);
                command.Parameters.AddWithValue("@IMEI", purchase.IMEI);
                command.ExecuteReader();
                return true;
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
            finally
            {
                DBConnection.Disconnect();
            }
        }

        public bool DeletePurchase(string C_Purchase)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(DELETE_PURCHASE, connection);
                command.Parameters.AddWithValue("@ID", C_Purchase);
                command.ExecuteReader();
                return true;
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
            finally
            {
                DBConnection.Disconnect();
            }
        }

        public bool UpdatePurchse(Purchase purchase, string C_Purchase)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(UPDATE_PURCHASE, connection);
                command.Parameters.AddWithValue("@newID", purchase.C_Purchase);
                command.Parameters.AddWithValue("@Status", purchase.Status);
                command.Parameters.AddWithValue("@Date", purchase.DatePurchase);
                command.Parameters.AddWithValue("@Customer", purchase.C_Customer);
                command.Parameters.AddWithValue("@IMEI", purchase.IMEI);
                command.Parameters.AddWithValue("@ID", C_Purchase);
                command.ExecuteReader();
                return true;
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
            finally
            {
                DBConnection.Disconnect();
            }
        }
    }
}
