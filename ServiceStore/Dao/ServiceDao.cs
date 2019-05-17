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
    class ServiceDao
    {
        SqlConnection connection = null;
        private static readonly string SELECT_ALL_Services = "SELECT * FROM service";
        private static readonly string SELECT_SERVICE_BY_ID =
            "SELECT * FROM service WHERE C_Service = @ID;";
        private static readonly string SELECT_SERVICE_BY_NAME =
            "SELECT * FROM service WHERE Name LIKE @Name%";
        private static readonly string INSERT_SERVICE =
            "INSERT INTO service(C_Service, Name, Category, Price, Detail) " +
            "VALUES (@ID, @Name, @Category, @Price, @Detail);";
        private static readonly string DELETE_SERVICE =
            "DELETE FROM service WHERE C_Service = @ID;";
        private static readonly string UPDATE_SERVICE =
            "UPDATE service " +
            "SET C_Service = @newID, Name = @Name, Category = @Category, Price = @Price, Detail = @Detail " +
            "WHERE C_Service = @ID";

        public ServiceDao(SqlConnection connection)
        {
            this.connection = connection;
        }

        public List<Service> SelectAllService()
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            SqlDataReader dataReader;
            List<Service> services = new List<Service>();
            try
            {
                connection.Open();
                command = new SqlCommand(SELECT_ALL_Services, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string C_service = dataReader.GetValue(0).ToString();
                    string Name = dataReader.GetValue(1).ToString();
                    string Category = dataReader.GetValue(2).ToString();
                    double Price = double.Parse(dataReader.GetValue(3).ToString());
                    string Detail = dataReader.GetValue(4).ToString();
                    Service service = new Service(C_service, Name, Category, Price, Detail);
                    services.Add(service);
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
            return services;
        }

        public Service SelectServiceById(string C_Service)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            SqlDataReader dataReader;
            Service service = null;
            try
            {
                connection.Open();
                command = new SqlCommand(SELECT_SERVICE_BY_ID, connection);
                command.Parameters.AddWithValue("@ID", C_Service);
                dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    string C_service = dataReader.GetValue(0).ToString();
                    string Name = dataReader.GetValue(1).ToString();
                    string Category = dataReader.GetValue(2).ToString();
                    double Price = double.Parse(dataReader.GetValue(3).ToString()); 
                    string Detail = dataReader.GetValue(4).ToString();
                    service = new Service(C_Service, Name, Category, Price, Detail);
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
            return service;
        }

        public bool InsertService(Service service)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(INSERT_SERVICE, connection);
                command.Parameters.AddWithValue("@ID", service.C_Service);
                command.Parameters.AddWithValue("@Name", service.Name);
                command.Parameters.AddWithValue("@Category", service.Category);
                command.Parameters.AddWithValue("@Price", service.Price);
                command.Parameters.AddWithValue("@Detail", service.Detail);
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

        public bool DeleteService(string C_Service)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(DELETE_SERVICE, connection);
                command.Parameters.AddWithValue("@ID", C_Service);
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

        public bool updateService(Service service, string C_Service)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(UPDATE_SERVICE, connection);
                command.Parameters.AddWithValue("@newID", service.C_Service);
                command.Parameters.AddWithValue("@Name", service.Name);
                command.Parameters.AddWithValue("@Category", service.Category);
                command.Parameters.AddWithValue("@Price", service.Price);
                command.Parameters.AddWithValue("@Detail", service.Detail);
                command.Parameters.AddWithValue("@ID", C_Service);
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
