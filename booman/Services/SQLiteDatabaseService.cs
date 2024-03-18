using System;
using System.Data.SQLite;

namespace booman.Services
{
    public class SQLiteDatabaseService
    {
        // Data fields
        private SQLiteConnection _connection;
        private string _databaseName = "..\\booman.db";

        // Constructors
        public SQLiteDatabaseService()
        {
            ConnectToDatabase();
        }

        // Methods
        private void ConnectToDatabase()
        {
            _connection = new SQLiteConnection($"Data Source={_databaseName}; Version3;");
            _connection.Open();
        }
    }
}
