using ServiceStore.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStore.Dao
{
    class DiscountDao
    {
        SqlConnection connection = null;
        private static readonly string SELECT_ALL_DISCOUNT = "SELECT * FROM discount";
        private static readonly string SELECT_DISCOUNT_BY_ID = 
            "SELECT * FROM discount WHERE C_Discount = @ID;";
        private static readonly string INSERT_DISCOUNT =
            "INSERT INTO discount(C_Discount, Size, Name, Starting, Ending) " +
            "VALUES (@ID, @Size, @Name, @Starting, @Ending);";
        private static readonly string DELETE_DISCOUNT =
            "DELETE FROM discount WHERE C_Discount = @ID;";
        private static readonly string UPDATE_DISCOUNT =
            "UPDATE discount " +
            "SET C_Discount = @newID, Size = @Size, Name = @Name, Starting = @Starting, Ending = @Ending " +
            "WHERE C_Discount = @ID;";

        public DiscountDao(SqlConnection connection)
        {
            this.connection = connection;
        }

        public List<Discount> SelectAllDiscounts()
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            SqlDataReader dataReader;
            List<Discount> discounts = new List<Discount>();
            try
            {
                connection.Open();
                command = new SqlCommand(SELECT_ALL_DISCOUNT, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string C_Discount = dataReader.GetValue(0).ToString();
                    int size = (int) dataReader.GetValue(1);
                    string Name = dataReader.GetValue(2).ToString();
                    string Starting = dataReader.GetValue(3).ToString();
                    DateTime dateTime = DateTime.Parse(Starting);
                    Starting = dateTime.ToShortDateString().ToString();
                    string Ending = dataReader.GetValue(4).ToString();
                    dateTime = DateTime.Parse(Ending);
                    Ending = dateTime.ToShortDateString().ToString();
                    Discount discount = new Discount(C_Discount, size, Name, Starting, Ending);
                    discounts.Add(discount);
                }
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                DBConnection.Disconnect();
            }
            return discounts;
        }

        public Discount SelectDiscountByCode(string C_Discount)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            SqlDataReader dataReader;
            Discount discount = null;
            try
            {
                connection.Open();
                command = new SqlCommand(SELECT_DISCOUNT_BY_ID, connection);
                command.Parameters.AddWithValue("@ID", C_Discount);
                dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    string c_Discount = dataReader.GetValue(0).ToString();
                    int size = (int)dataReader.GetValue(1);
                    string Name = dataReader.GetValue(2).ToString();
                    string Starting = dataReader.GetValue(3).ToString();
                    string Ending = dataReader.GetValue(4).ToString();
                    discount = new Discount(c_Discount, size, Name, Starting, Ending);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                DBConnection.Disconnect();
            }
            return discount;
        }

        public bool InsertDiscount(Discount discount)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(INSERT_DISCOUNT, connection);
                command.Parameters.AddWithValue("@ID", discount.C_Discount);
                command.Parameters.AddWithValue("@Size", discount.Size);
                command.Parameters.AddWithValue("@Name", discount.Name);
                command.Parameters.AddWithValue("@Starting", discount.Starting);
                command.Parameters.AddWithValue("@Ending", discount.Ending);
                command.ExecuteReader();
                return true;
            }catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                DBConnection.Disconnect();
            }
        }

        public bool DeleteDiscount(string C_Discount)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(DELETE_DISCOUNT, connection);
                command.Parameters.AddWithValue("@ID", C_Discount);
                command.ExecuteReader();
                return true;
            }catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                DBConnection.Disconnect();
            }
        }

        public bool UpdateDiscount(Discount discount, string C_Discount)
        {
            connection = DBConnection.Connect();
            SqlCommand command;
            try
            {
                connection.Open();
                command = new SqlCommand(UPDATE_DISCOUNT, connection);
                command.Parameters.AddWithValue("@newID", discount.C_Discount);
                command.Parameters.AddWithValue("@Size", discount.Size);
                command.Parameters.AddWithValue("@Name", discount.Name);
                command.Parameters.AddWithValue("@Starting", discount.Starting);
                command.Parameters.AddWithValue("@Ending", discount.Ending);
                command.Parameters.AddWithValue("@ID", C_Discount);
                command.ExecuteReader();
                return true;
            }catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                DBConnection.Disconnect();
            }
        }
    }
}
