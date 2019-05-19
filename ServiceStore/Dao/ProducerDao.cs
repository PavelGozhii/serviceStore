using ServiceStore.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

namespace ServiceStore.Dao
{
    class ProducerDao
    {

        SqlConnection connection = null;
        private static readonly string SELECT_ALL_PRODUCER = "SELECT * FROM producer";
        private static readonly string SELECT_PRODUCER_BY_ID = "SELECT * FROM producer WHERE C_Producer = @ID;";
        private static readonly string INSERT_PRODUCER= "INSERT INTO producer(C_Producer, Director, Telephone, Email, Review, PhisicalAddress)" +
            " VALUES (@ID, @Director, @Telephone, @Email, @Review, @Address);";
        private static readonly string DELETE_PRODUCER = "DELETE FROM producer WHERE C_Producer = @ID;";
        private static readonly string UPDATE_PRODUCER = "UPDATE producer " +
            "SET C_Producer = @newID, Director = @Director, Telephone = @Telephone," +
            " Email = @Email, Review = @Review, PhisicalAddress = @Address WHERE C_Producer = @ID ;";

        public ProducerDao(SqlConnection connection)
        {
            this.connection = connection;
        }

        public List<Producer> SelectAllProducers()
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            SqlDataReader dataReader;
            List<Producer> producers = new List<Producer>();
            try
            {
                connection.Open();
                command = new SqlCommand(SELECT_ALL_PRODUCER, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string c_Customer = dataReader.GetValue(0).ToString();
                    string Director = dataReader.GetValue(1).ToString();
                    string Telephone = dataReader.GetValue(2).ToString();
                    string Email = dataReader.GetValue(3).ToString();
                    string Review = dataReader.GetValue(4).ToString();
                    string PhisicalAdd = dataReader.GetValue(5).ToString();
                    Producer producer = new Producer(c_Customer, Director, Telephone, Email, Review, PhisicalAdd);
                    producers.Add(producer);
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
            return producers;
        }

        public Producer SelectProducerById(string C_Producer)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            SqlDataReader dataReader;
            Producer producer = null;
            try
            {
                connection.Open();
                command = new SqlCommand(SELECT_PRODUCER_BY_ID, connection);
                command.Parameters.AddWithValue("@ID", C_Producer);
                dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    string c_Customer = dataReader.GetValue(0).ToString();
                    string Director = dataReader.GetValue(1).ToString();
                    string Telephone = dataReader.GetValue(2).ToString();
                    string Email = dataReader.GetValue(3).ToString();
                    string Review = dataReader.GetValue(4).ToString();
                    string PhisicalAdd = dataReader.GetValue(5).ToString();
                    producer = new Producer(c_Customer, Director, Telephone, Email, Review, PhisicalAdd);
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
            return producer;
        }

        public bool InsertProducer(Producer producer)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(INSERT_PRODUCER, connection);
                command.Parameters.AddWithValue("@ID", producer.C_Producer);
                command.Parameters.AddWithValue("@Director", producer.Director);
                command.Parameters.AddWithValue("@Telephone", producer.Telephone);
                command.Parameters.AddWithValue("@Email", producer.Email);
                command.Parameters.AddWithValue("@Review", producer.Review);
                command.Parameters.AddWithValue("@Address", producer.PhisicalAdress);
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

        public bool DeleteProducer(string C_Producer)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(DELETE_PRODUCER, connection);
                command.Parameters.AddWithValue("@ID", C_Producer);
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

        public bool UpdateProdcer(Producer producer, string C_Producer)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(UPDATE_PRODUCER, connection);
                command.Parameters.AddWithValue("@newID", producer.C_Producer);
                command.Parameters.AddWithValue("@Director", producer.Director);
                command.Parameters.AddWithValue("@Telephone", producer.Telephone);
                command.Parameters.AddWithValue("@Email", producer.Email);
                command.Parameters.AddWithValue("@Review", producer.Review);
                command.Parameters.AddWithValue("@Address", producer.PhisicalAdress);
                command.Parameters.AddWithValue("@ID", C_Producer);
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
