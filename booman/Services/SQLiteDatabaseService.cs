using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Documents;

namespace booman.Services
{
    public class SQLiteDatabaseService
    {
        // Data fields
        private SQLiteConnection _connection;
        private string _databaseName = "../booman.db";

        // Constructors
        public SQLiteDatabaseService()
        {
            ConnectToDatabase();
        }

        // Methods
        private void ConnectToDatabase()
        {
            _connection = new SQLiteConnection($"Data Source={_databaseName};Version=3;");
            _connection.Open();
        }

        public DataTable GetDataTable(string tableName)
        {
            DataTable dataTbl = new DataTable();

            using (SQLiteCommand cmd = new SQLiteCommand($"SELECT * FROM {tableName}", _connection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    dataTbl.Load(reader);
                }
            }

            return dataTbl;
        }
    }
}
