using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoVenta
{
    public class CustomerDAO : BaseDAO
    {
        public  static DataTable GetAllCustomers()
        {
            using (var connection = new CustomerDAO().GetConnection())
            {
                connection.Open();
                string query = "SELECT CustomerID, CompanyName, ContactName, Address FROM Customers";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable customers = new DataTable();
                adapter.Fill(customers);
                return customers;
            }
        }

        public static void AddCustomer(string companyName, string contactName, string address)
        {
            using (var connection = new CustomerDAO().GetConnection())
            {
                connection.Open();

                // Aquí no incluimos CustomerID porque se genera automáticamente
                string query = "INSERT INTO Customers (CompanyName, ContactName, Address) VALUES (@CompanyName, @ContactName, @Address)";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@CompanyName", companyName);
                command.Parameters.AddWithValue("@ContactName", contactName);
                command.Parameters.AddWithValue("@Address", address);

                // Ejecutamos la consulta de inserción
                command.ExecuteNonQuery();

            }
        }
            public static void UpdateCustomer(string customerId, string companyName, string contactName, string address)
        {
            using (var connection = new CustomerDAO().GetConnection())
            {
                connection.Open();
                string query = "UPDATE Customers SET CompanyName = @CompanyName, ContactName = @ContactName, Address = @Address WHERE CustomerID = @CustomerID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerID", customerId);
                command.Parameters.AddWithValue("@CompanyName", companyName);
                command.Parameters.AddWithValue("@ContactName", contactName);
                command.Parameters.AddWithValue("@Address", address);
                command.ExecuteNonQuery();
            }
        }

        public  static void DeleteCustomer(string customerId)
        {
            using (var connection = new CustomerDAO().GetConnection())
            {
                connection.Open();
                string query = "DELETE FROM Customers WHERE CustomerID = @CustomerID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerID", customerId);
                command.ExecuteNonQuery();
            }
        }
    }
}
