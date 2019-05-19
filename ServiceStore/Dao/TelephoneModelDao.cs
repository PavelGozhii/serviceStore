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
    class TelephoneModelDao
    {
        SqlConnection connection = null;
        private static readonly string SELECT_ALL_TELEPHONE_MODEL = "SELECT * FROM TelephoneModel";
        private static readonly string SELECT_TELEPHONE_MODEL_BY_ID = "SELECT * FROM TelephoneModel WHERE C_TelephoneModel = @ID;";
        private static readonly string INSERT_TELEPHONE_MODEL = 
            "INSERT INTO TelephoneModel(C_TelephoneModel, Name, Category, OperatingSystem, MemorySize, Processor, C_Trademark)" +
            " VALUES (@ID, @Name, @Category, @OS, @Memory, @Processor, @Trademark);";
        private static readonly string DELETE_TELEPHONE_MODEL = "DELETE FROM TelephoneModel WHERE C_TelephoneModel = @ID;";
        private static readonly string UPDATE_TELEPHONE_MODEL = "UPDATE TelephoneModel " +
            "SET C_TelephoneModel = @newID, Name = @Name, Category = @Category, OperatingSystem = @OS, MemorySize = @Memory, Processor = @Processor, C_Trademark = @Trademark WHERE C_TelephoneModel = @ID ;";

        public TelephoneModelDao(SqlConnection connection)
        {
            this.connection = connection;
        }

        public List<TelephoneModel> selectAllTelephomeModel()
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            SqlDataReader dataReader;
            List<TelephoneModel> telephoneModels = new List<TelephoneModel>();
            try
            {
                connection.Open();
                command = new SqlCommand(SELECT_ALL_TELEPHONE_MODEL, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string C_TelephoneModel = dataReader.GetValue(0).ToString();
                    string Name = dataReader.GetValue(1).ToString();
                    string Category = dataReader.GetValue(2).ToString();
                    string OS = dataReader.GetValue(3).ToString();
                    int MemorySize = (int)dataReader.GetValue(4);
                    string Processor = dataReader.GetValue(5).ToString();
                    string C_Trademark = dataReader.GetValue(6).ToString();
                    TelephoneModel telephoneModel = new TelephoneModel(C_TelephoneModel, Name, Category, OS, MemorySize, Processor, C_Trademark);
                    telephoneModels.Add(telephoneModel);
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                DBConnection.Disconnect();
            }
            return telephoneModels;
        }

        public TelephoneModel SelectTelephoneModelById(string C_TelephoneModel)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            SqlDataReader dataReader;
            TelephoneModel telephoneModel = null;
            try
            {
                connection.Open();
                command = new SqlCommand(SELECT_TELEPHONE_MODEL_BY_ID, connection);
                command.Parameters.AddWithValue("@ID", C_TelephoneModel);
                dataReader = command.ExecuteReader();
                if (dataReader.Read()){
                    string c_TelephoneModel = dataReader.GetValue(0).ToString();
                    string Name = dataReader.GetValue(1).ToString();
                    string Category = dataReader.GetValue(2).ToString();
                    string OS = dataReader.GetValue(3).ToString();
                    int MemorySize = (int)dataReader.GetValue(4);
                    string Processor = dataReader.GetValue(5).ToString();
                    string C_Trademark = dataReader.GetValue(6).ToString();
                    telephoneModel = new TelephoneModel(c_TelephoneModel, Name, Category, OS, MemorySize, Processor, C_Trademark);
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                DBConnection.Disconnect();
            }
            return telephoneModel;
        }

        public bool InsertTelephoneModel(TelephoneModel telephoneModel)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(INSERT_TELEPHONE_MODEL, connection);
                command.Parameters.AddWithValue("@ID", telephoneModel.C_TelephoneModel);
                command.Parameters.AddWithValue("@Name", telephoneModel.Name);
                command.Parameters.AddWithValue("@Category", telephoneModel.Category);
                command.Parameters.AddWithValue("@OS", telephoneModel.OperatingSystem);
                command.Parameters.AddWithValue("@Memory", telephoneModel.MemorySize);
                command.Parameters.AddWithValue("@Processor", telephoneModel.Processor);
                command.Parameters.AddWithValue("@Trademark", telephoneModel.C_Trademark);
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

        public bool DeleteTelephoneModel(string C_TelephoneModel)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(DELETE_TELEPHONE_MODEL, connection);
                command.Parameters.AddWithValue("@ID", C_TelephoneModel);
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

        public bool UpdateTelephoneModel(TelephoneModel telephoneModel, string C_TelephoneModel)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(UPDATE_TELEPHONE_MODEL, connection);
                command.Parameters.AddWithValue("@newID", telephoneModel.C_TelephoneModel);
                command.Parameters.AddWithValue("@Name", telephoneModel.Name);
                command.Parameters.AddWithValue("@Category", telephoneModel.Category);
                command.Parameters.AddWithValue("@OS", telephoneModel.OperatingSystem);
                command.Parameters.AddWithValue("@Memory", telephoneModel.MemorySize);
                command.Parameters.AddWithValue("@Processor", telephoneModel.Processor);
                command.Parameters.AddWithValue("@Trademark", telephoneModel.C_Trademark);
                command.Parameters.AddWithValue("@ID", C_TelephoneModel);
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




    }
}
