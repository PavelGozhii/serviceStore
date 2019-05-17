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
    class DetailModelDao
    {
        SqlConnection connection = null;
        private static readonly string SELECT_ALL_DETAIL_MODEL = "SELECT * FROM DetailModel";
        private static readonly string SELECT_DETAIL_MODEL_BY_ID = "SELECT * FROM DetailModel WHERE C_DetailModel = @ID";
        private static readonly string INSERT_DETAIL_MODEL
            = "INSERT INTO DetailModel(C_DetailModel, Price, C_TelephoneModel, C_Detail) " +
            "VALUES (@ID, @Price, @TelephoneModel, @Detail);";
        private static readonly string DELETE_DETAIL_MODEL = "DELETE FROM DetailModel WHERE C_DetailModel = @ID ;";
        private static readonly string UPDATE_DETAIL_MDDEL = "UPDATE DetailModel " +
            "SET C_DetailModel = @newID, Price = @Price, C_TelephoneModel = @TelephoneModel, C_Detail = @Detail WHERE C_DetailModel = @ID;";

        public DetailModelDao(SqlConnection connection)
        {
            this.connection = connection;
        }

        public List<DetailModel> SelectAllDetailModel()
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            SqlDataReader dataReader;
            List<DetailModel> detailModels = new List<DetailModel>();
            try
            {
                connection.Open();
                command = new SqlCommand(SELECT_ALL_DETAIL_MODEL, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string C_DetailModel = dataReader.GetValue(0).ToString();
                    double Price = double.Parse(dataReader.GetValue(1).ToString());
                    string C_TelephoneModel = dataReader.GetValue(2).ToString();
                    string C_Detail = dataReader.GetValue(3).ToString();
                    DetailModel detailModel = new DetailModel(C_DetailModel, Price, C_TelephoneModel, C_Detail);
                    detailModels.Add(detailModel);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            } finally
            {
                DBConnection.Disconnect();
            }
            return detailModels;
        }

        public DetailModel SelectDetailModelById(string C_DetailModel)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            SqlDataReader dataReader;
            DetailModel detailModel = null;
            try
            {
                connection.Open();
                command = new SqlCommand(SELECT_DETAIL_MODEL_BY_ID, connection);
                command.Parameters.AddWithValue("@ID", C_DetailModel);
                dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    string c_DetailModel = dataReader.GetValue(0).ToString();
                    double Price = double.Parse(dataReader.GetValue(1).ToString());
                    string C_TelephoneModel = dataReader.GetValue(2).ToString();
                    string C_Detail = dataReader.GetValue(3).ToString();
                    detailModel = new DetailModel(c_DetailModel, Price, C_TelephoneModel, C_Detail);
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                DBConnection.Disconnect();
            }
            return detailModel;
        }

        public bool InsertDetailModel(DetailModel detailModel)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(INSERT_DETAIL_MODEL, connection);
                command.Parameters.AddWithValue("@ID", detailModel.C_Detail);
                command.Parameters.AddWithValue("@Price", detailModel.Price);
                command.Parameters.AddWithValue("@TelephoneModel", detailModel.C_TelephoneModel);
                command.Parameters.AddWithValue("@Detail", detailModel.C_Detail);
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

        public bool DeleteDetailModel(string C_DetailModel)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(DELETE_DETAIL_MODEL, connection);
                command.Parameters.AddWithValue("@ID", C_DetailModel);
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

        public bool UpdateDetailModel(DetailModel detailModel, string C_Customer)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(UPDATE_DETAIL_MDDEL, connection);
                command.Parameters.AddWithValue("@newID", detailModel.C_Detail);
                command.Parameters.AddWithValue("@Price", detailModel.Price);
                command.Parameters.AddWithValue("@TelephoneModel", detailModel.C_TelephoneModel);
                command.Parameters.AddWithValue("@Detail", detailModel.C_Detail);
                command.Parameters.AddWithValue("@ID", C_Customer);
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
