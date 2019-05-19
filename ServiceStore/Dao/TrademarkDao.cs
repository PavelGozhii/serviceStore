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
    class TrademarkDao
    {
        SqlConnection connection = null;
        private static readonly string SELECT_ALL_TRADEMARK = "SELECT * FROM trademark";
        private static readonly string SELECT_TRADEMARK_BY_ID = "SELECT * FROM trademark WHERE C_Trademark = @ID;";
        private static readonly string INSERT_TRADEMARK = 
            "INSERT INTO trademark(C_Trademark, Name, Review, C_Producer) VALUES (@ID, @Name, @Review, @Producer);";
        private static readonly string DELETE_TRADEMARK = "DELETE FROM Trademark WHERE C_Trademark = @ID;";
        private static readonly string UPDATE_DETAIL = "UPDATE trademark " +
            "SET C_Trademark = @newID, Name = @Name, Review = @Review, C_Producer = @Producer WHERE C_Trademark = @ID";

        public TrademarkDao(SqlConnection connection)
        {
            this.connection = connection;
        }

        public List<Trademark> SelectAllTrademark()
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            SqlDataReader dataReader;
            List<Trademark> trademarks = new List<Trademark>();
            try
            {
                connection.Open();
                command = new SqlCommand(SELECT_ALL_TRADEMARK, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string C_Trademark = dataReader.GetValue(0).ToString();
                    string Name = dataReader.GetValue(1).ToString();
                    string Review = dataReader.GetValue(2).ToString();
                    string C_Prudcer = dataReader.GetValue(3).ToString();
                    Trademark trademark = new Trademark(C_Trademark, Name, Review, C_Prudcer);
                    trademarks.Add(trademark);
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                DBConnection.Disconnect();
            }
            return trademarks;
        }

        public Trademark SelectTrademarkById(string C_Trademark)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            SqlDataReader dataReader;
            Trademark trademark = null;
            try
            {
                connection.Open();
                command = new SqlCommand(SELECT_TRADEMARK_BY_ID, connection);
                command.Parameters.AddWithValue("@ID", C_Trademark);
                dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    string c_Trademark = dataReader.GetValue(0).ToString();
                    string Name = dataReader.GetValue(1).ToString();
                    string Review = dataReader.GetValue(2).ToString();
                    string C_Prudcer = dataReader.GetValue(3).ToString();
                    trademark = new Trademark(c_Trademark, Name, Review, C_Prudcer);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                DBConnection.Disconnect();
            }
            return trademark;
        }

        public bool InsertTrademark(Trademark trademark)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(INSERT_TRADEMARK, connection);
                command.Parameters.AddWithValue("@ID", trademark.C_Trademark);
                command.Parameters.AddWithValue("@Name", trademark.Name);
                command.Parameters.AddWithValue("@Review", trademark.Review);
                command.Parameters.AddWithValue("@Producer", trademark.C_Producer);
                command.ExecuteReader();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
            finally
            {
                DBConnection.Disconnect();
            }
        }

        public bool DeleteTrademark(string C_Trademark)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(DELETE_TRADEMARK, connection);
                command.Parameters.AddWithValue("@ID", C_Trademark);
                command.ExecuteReader();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
            finally
            {
                DBConnection.Disconnect();
            }
        }

        public bool updateTrademark(Trademark trademark, string C_Trademark)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(UPDATE_DETAIL, connection);
                command.Parameters.AddWithValue("@newID", trademark.C_Trademark);
                command.Parameters.AddWithValue("@Name", trademark.Name);
                command.Parameters.AddWithValue("@Review", trademark.Review);
                command.Parameters.AddWithValue("@Producer", trademark.C_Producer);
                command.Parameters.AddWithValue("@ID", C_Trademark);
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
