using MySql.Data.MySqlClient;

namespace TorresyTorres.Database
{
    public class Conection
    {
        public MySqlConnection GetSqlConnection()
        {
            string connectionString = "server=localhost;user=root;database=prueba;port=3306;password=";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
