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
    class TelephoneDao
    {
        SqlConnection connection = null;
        private static readonly string SELECT_ALL_TELEPHONES = "SELECT * FROM telephone";
        private static readonly string SELECT_TELEPHONE_BY_ID
            = "SELECT * FROM telephone WHERE IMEI = @ID";
        private static readonly string INSERT_TELEPHONE =
            "INSERT INTO telephone(IMEI, C_TelephoneModel) " +
            "VALUES (@ID, @TelephoneModel);";
        private static readonly string DELETE_TELEPHONE = "DELETE FROM telephone WHERE IMEI = @ID;";
        private static readonly string UPDATE_TELEPHONE = "UPDATE telephone " +
            "SET IMEI = @newID, C_TelephoneModel = @TelephoneModel WHERE IMEI = @ID;";

        public TelephoneDao(SqlConnection connection)
        {
            this.connection = connection;
        }

        public List<Telephone> SelectAllTelephone()
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            SqlDataReader dataReader;
            List<Telephone> telephones = new List<Telephone>();
            try
            {
                connection.Open();
                command = new SqlCommand(SELECT_ALL_TELEPHONES, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string IMEI = dataReader.GetValue(0).ToString();
                    string C_TelephoneModel = dataReader.GetValue(1).ToString();
                    Telephone telephone = new Telephone(IMEI, C_TelephoneModel);
                    telephones.Add(telephone);
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                DBConnection.Disconnect();
            }
            return telephones;
        }

        public Telephone SelectTelephoneById(string IMEI)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            SqlDataReader dataReader;
            Telephone telephone = null;
            try
            {
                connection.Open();
                command = new SqlCommand(SELECT_TELEPHONE_BY_ID, connection);
                command.Parameters.AddWithValue("@ID", IMEI);
                dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    string IMEi = dataReader.GetValue(0).ToString();
                    string C_TelephoneModel = dataReader.GetValue(1).ToString();
                    telephone = new Telephone(IMEi, C_TelephoneModel);
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                DBConnection.Disconnect();
            }
            return telephone;
        }

        public bool InsertTelephone(Telephone telephone)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(INSERT_TELEPHONE, connection);
                command.Parameters.AddWithValue("@ID", telephone.IMEI);
                command.Parameters.AddWithValue("@TelephoneModel", telephone.C_TelephoneModel);
                command.ExecuteReader();
                return true;
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            finally
            {
                DBConnection.Disconnect();
            }
        }


        public bool DeleteTelephone(string IMEI)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(DELETE_TELEPHONE, connection);
                command.Parameters.AddWithValue("@ID", IMEI);
                command.ExecuteReader();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            finally
            {
                DBConnection.Disconnect();
            }
        }

        public bool UpdateTelephone(Telephone telephone, string C_Telephone)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(UPDATE_TELEPHONE, connection);
                command.Parameters.AddWithValue("@newID", telephone.IMEI);
                command.Parameters.AddWithValue("@TelephoneModel", telephone.C_TelephoneModel);
                command.Parameters.AddWithValue("@ID", C_Telephone);
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
