using System.Data.SqlClient;

namespace SalesSystem.DAL
{
    public static class DbHelper
    {
        // Chuỗi kết nối chuẩn trực tiếp vào LocalDB của máy bạn
        public static string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SaleDB;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}