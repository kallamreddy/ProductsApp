using ProductsApp.Models;
using System.Data.SqlClient;

namespace ProductsApp.Services
{
    public class ProductService
    {
        private static string db_source = "prdserver.database.windows.net";
        private static string db_user = "prdadminuser";
        private static string db_password = "Bannu@08642";
        private static string db_database = "productsdb";

        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;

            return new SqlConnection(_builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            SqlConnection sqlConnection = GetConnection();
            List<Product> products = new List<Product>();
            string statement = "SELECT ProductId,ProductName,Quantity from Products";
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(statement, sqlConnection);

            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
                while (sqlDataReader.Read())
                {
                    Product product = new Product();
                    product.ProductId=sqlDataReader.GetInt32(0);
                    product.ProductName=sqlDataReader.GetString(1);
                    product.Quantity=sqlDataReader.GetInt32(2);
                    products.Add(product);
                }
            }

            sqlConnection.Close();
            return products;
        }
    }
}
