using ServiceStore.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

namespace ServiceStore.Dao
{
    class DetailDao
    {
        SqlConnection connection = null;
        private static readonly string SELECT_ALL_DETAIL = "SELECT * FROM detail";
        private static readonly string SELECT_DETAIL_BY_C_DETAIL= "SELECT * FROM detail WHERE C_Detail = @ID;";
        private static readonly string INSERT_DETAIL = "INSERT INTO detail(C_Detail, Type) VALUES (@ID, @Type);";
        private static readonly string DELETE_DETAIL_BY_C_DETAIL = "DELETE FROM detail WHERE C_Detail = @ID;";
        private static readonly string UPDATE_DETAIL = "UPDATE detail " +
            "SET C_Detail = @newID, Type = @Type WHERE C_Detail = @ID";

        public DetailDao(SqlConnection connection)
        {
            this.connection = connection;
        }


        public List<Detail> SelectAllDetails()
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            SqlDataReader dataReader;
            List<Detail> details = new List<Detail>();
            try
            {
                connection.Open();
                command = new SqlCommand(SELECT_ALL_DETAIL, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string C_Detail  = dataReader.GetValue(0).ToString();
                    string Type = dataReader.GetValue(1).ToString();
                    Detail detail = new Detail(C_Detail, Type);
                    details.Add(detail);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return details;
        }

        public Detail SelectDetailById(string C_Detail)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            SqlDataReader dataReader;
            Detail detail = null;
            try
            {
                connection.Open();
                command = new SqlCommand(SELECT_DETAIL_BY_C_DETAIL, connection);
                command.Parameters.AddWithValue("@ID", C_Detail);
                dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    string C_detail = dataReader.GetValue(0).ToString();
                    string type = dataReader.GetValue(1).ToString();
                    detail = new Detail(C_detail, type);
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                DBConnection.Disconnect();
            }
            return detail;
        }

        public bool InsertDetail(Detail detail)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(INSERT_DETAIL, connection);
                command.Parameters.AddWithValue("@ID", detail.C_Detail);
                command.Parameters.AddWithValue("@Type", detail.Type);
                command.ExecuteReader();
                return false;
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

        public bool DeleteDetail(string C_Detail)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(DELETE_DETAIL_BY_C_DETAIL, connection);
                command.Parameters.AddWithValue("@ID", C_Detail);
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

        public bool UpdateDetail(Detail detail, string C_Detail)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(UPDATE_DETAIL, connection);
                command.Parameters.AddWithValue("@newID", detail.C_Detail);
                command.Parameters.AddWithValue("@Type", detail.Type);
                command.Parameters.AddWithValue("@ID", C_Detail);
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
