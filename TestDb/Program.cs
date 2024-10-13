using Microsoft.Data.SqlClient;

namespace TestDb
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string connectionString = "Server=Server;Database=NewsAggregatorDb;User Id=dnisko;Password=1q2w3e4r5t;";

            using var connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection Successful!");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Connection Failed!");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
