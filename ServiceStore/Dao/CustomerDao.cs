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
    class CustomerDao
    {
        SqlConnection connection = null;
        private static readonly string SELECT_ALL_CUSTOMERS = "SELECT * FROM customer";
        private static readonly string SELECT_CUSTOMER_BY_C_CUSTOMER = "SELECT * FROM customer WHERE C_CUSTOMER = @ID;";
        private static readonly string INSERT_CUSTOMER = "INSERT INTO customer(C_Customer, FullName, DateOfBirth, Address)" +
            " VALUES (@ID,@Name,@Birth,@Address);";
        private static readonly string DELETE_CUSTOMER_BY_C_CUSTOMER = "DELETE FROM customer WHERE C_Customer = @ID;";
        private static readonly string UPDATE_CUSTOMER = "UPDATE customer " +
            "SET C_Customer = @newID, FullName = @Name, DateOfBirth = @Birth, Address = @Address WHERE C_Customer = @ID ;";

        public CustomerDao(SqlConnection connection)
        {
            this.connection = connection;
        } 

        public List<Customer> SelectAllCustomers()
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            SqlDataReader dataReader;
            List<Customer> customers = new List<Customer>();
            try
            {
                connection.Open();
                command = new SqlCommand(SELECT_ALL_CUSTOMERS, connection);               
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string c_Customer = dataReader.GetValue(0).ToString();
                    string fullName = dataReader.GetValue(1).ToString();
                    string dataOfBirth = dataReader.GetValue(2).ToString();
                    DateTime dateTime = DateTime.Parse(dataOfBirth);
                    dataOfBirth = dateTime.ToShortDateString().ToString();                       
                    string address = dataReader.GetValue(3).ToString();
                    Customer customer = new Customer(c_Customer, fullName, dataOfBirth, address);
                    customers.Add(customer);
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                DBConnection.Disconnect();
            }
            return customers;
        }

        public Customer SelectCustomerByCode(string C_Customer)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            SqlDataReader dataReader;
            Customer customer = null;
            try
            {
                connection.Open();
                command = new SqlCommand(SELECT_CUSTOMER_BY_C_CUSTOMER, connection);
                command.Parameters.AddWithValue("@ID", C_Customer);
                dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    string c_Customer = dataReader.GetValue(0).ToString();
                    string fullName = dataReader.GetValue(1).ToString();
                    string dataOfBirth = dataReader.GetValue(2).ToString();
                    string address = dataReader.GetValue(3).ToString();
                    customer = new Customer(c_Customer, fullName, dataOfBirth, address);
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                DBConnection.Disconnect();
            }
            return customer; 
        } 

        public bool InsertCustomer(Customer customer)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(INSERT_CUSTOMER, connection);
                command.Parameters.AddWithValue("@ID", customer.C_Customer);
                command.Parameters.AddWithValue("@Name", customer.FullName);
                command.Parameters.AddWithValue("@Birth", customer.DateOfBirth);
                command.Parameters.AddWithValue("@Address", customer.Address);
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

        public bool DeleteCustomer(string C_Customer)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(DELETE_CUSTOMER_BY_C_CUSTOMER, connection);
                command.Parameters.AddWithValue("@ID", C_Customer);
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

        public bool UpdateCustomer(Customer customer, string C_Customer)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(UPDATE_CUSTOMER, connection);
                command.Parameters.AddWithValue("@newID", customer.C_Customer);
                command.Parameters.AddWithValue("@Name", customer.FullName);
                command.Parameters.AddWithValue("@Birth", customer.DateOfBirth);
                command.Parameters.AddWithValue("@Address", customer.Address);
                command.Parameters.AddWithValue("@ID", C_Customer);
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
       
    }
}
