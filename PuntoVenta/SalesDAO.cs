using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoVenta
{
    public class SalesDAO : BaseDAO
    {
        public void AddSale(int customerId, int employeeId, DataTable saleDetails)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string saleQuery = "INSERT INTO Sales (CustomerID, EmployeeID, SaleDate, Total) VALUES (@CustomerID, @EmployeeID, NOW(), 0)";
                        MySqlCommand saleCommand = new MySqlCommand(saleQuery, connection, transaction);
                        saleCommand.Parameters.AddWithValue("@CustomerID", customerId);
                        saleCommand.Parameters.AddWithValue("@EmployeeID", employeeId);
                        saleCommand.ExecuteNonQuery();

                        int saleId = (int)saleCommand.LastInsertedId;

                        string detailQuery = "INSERT INTO SaleDetails (SaleID, ProductID, Quantity, UnitPrice, Subtotal) VALUES (@SaleID, @ProductID, @Quantity, @UnitPrice, @Subtotal)";
                        MySqlCommand detailCommand = new MySqlCommand(detailQuery, connection, transaction);

                        foreach (DataRow row in saleDetails.Rows)
                        {
                            detailCommand.Parameters.Clear();
                            detailCommand.Parameters.AddWithValue("@SaleID", saleId);
                            detailCommand.Parameters.AddWithValue("@ProductID", row["ProductID"]);
                            detailCommand.Parameters.AddWithValue("@Quantity", row["Quantity"]);
                            detailCommand.Parameters.AddWithValue("@UnitPrice", row["UnitPrice"]);
                            detailCommand.Parameters.AddWithValue("@Subtotal", row["Subtotal"]);
                            detailCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public DataTable GetSalesByMonth()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT MONTH(SaleDate) AS Month, SUM(Total) AS TotalSales FROM Sales GROUP BY MONTH(SaleDate)";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable salesData = new DataTable();
                adapter.Fill(salesData);
                return salesData;
            }
        }
    }
}
