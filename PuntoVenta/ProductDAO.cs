using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoVenta
{
    public class ProductDAO : BaseDAO
    {
        public  static DataTable GetAllProducts()
        {
            using (var connection = new ProductDAO().GetConnection())
            {
                connection.Open();
                string query = "SELECT ProductID, ProductName, UnitPrice FROM Products";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable products = new DataTable();
                adapter.Fill(products);
                return products;
            }
        }

        public  static decimal GetProductPrice(int productId)
        {
            using (var connection = new ProductDAO().GetConnection())
            {
                connection.Open();
                string query = "SELECT UnitPrice FROM Products WHERE ProductID = @productId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@productId", productId);
                object result = command.ExecuteScalar();
                return result != null ? Convert.ToDecimal(result) : 0;
            }
        }

        public  static void AddNewProduct(int productId, string productName, decimal unitPrice)
        {
            using (var connection = new ProductDAO().GetConnection())
            {
                connection.Open();
                string query = "INSERT INTO Products (ProductID, ProductName, UnitPrice) VALUES (@id, @name, @price)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", productId);
                command.Parameters.AddWithValue("@name", productName);
                command.Parameters.AddWithValue("@price", unitPrice);
                command.ExecuteNonQuery();
            }
        }
    }
}
