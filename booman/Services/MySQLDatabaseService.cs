using System;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace booman.Service
{
    public class MySQLDatabaseService
    {
        public MySqlConnection connection;
        public string server;
        public string database;
        public string uid;
        public string password;

        public MySQLDatabaseService(string server, string database, string uid, string password)
        {
            this.server = server;
            this.database = database;
            this.uid = uid;
            this.password = password;
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            connection = new MySqlConnection(connectionString);
        }

        public DataTable GetTableData(string tableName)
        {
            DataTable dataTable = new DataTable();

            string query = $"SELECT * FROM {tableName}";

            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            connection.Close();

            return dataTable;
        }
    }
}