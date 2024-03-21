using System;
using System.Data;
using System.Windows.Documents;
using MySql.Data.MySqlClient;

namespace booman.Services
{
    public class MySQLDatabaseService
    {
        // Data fields
        private MySqlConnection connection;
        private string server = "localhost";
        private string database = "booman";
        private string uid = "root";


        private string password = "12345678";


  
        public MySQLDatabaseService()
        {
            ConnectToDatabase();
        }

        // Methods
        private void ConnectToDatabase()
        {
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
        public void InsertRoom(string roomNumber, string quality,string bedType, decimal price)
        {
            connection.Open();
            string query = "INSERT INTO `room` (`room_num`, `quality_class`, `bed_type_class`, `price`, `status`) VALUES (@RoomNumber, @Quality, @BedType, @Price, @Status)";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@RoomNumber", roomNumber);
            command.Parameters.AddWithValue("@Quality", quality);
            command.Parameters.AddWithValue("@BedType", bedType);
            command.Parameters.AddWithValue("@Price", price);
            command.Parameters.AddWithValue("@Status", "empty");
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void DeleteRoom(string roomNumber)
        {
            connection.Open();
            string query = "DELETE FROM room WHERE room_num = @RoomNumber";
            MySqlCommand command = connection.CreateCommand();  
            command.CommandText = query;
            command.Parameters.AddWithValue("@RoomNumber", roomNumber);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void UpdateRoom(string roomNumber, string quality,string bedType, decimal price, string status)
        {
            connection.Open();
            string query = "UPDATE room SET quality_class = @Quality, bed_type_class = @BedType, price = @Price, status = @Status WHERE room_num = @RoomNumber";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@Quality", quality);
            command.Parameters.AddWithValue("@Price", price);
            command.Parameters.AddWithValue("@BedType", bedType);
            command.Parameters.AddWithValue("@Status", status);
            command.Parameters.AddWithValue("@RoomNumber", roomNumber);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void InsertService(string idService, string nameService, decimal price,string unitService,string noteService)
        {
            connection.Open();
            string query = "INSERT INTO service(id,name,price,unit,note) VALUES (@ServiceId, @ServiceName,@Price,@Unit,@Note)";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@ServiceId", idService);
            command.Parameters.AddWithValue("@ServiceName", nameService);
            command.Parameters.AddWithValue("@Price", price);
            command.Parameters.AddWithValue("@Unit", unitService);
            command.Parameters.AddWithValue("@Note", noteService);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void UpdateService(string idService, string nameService, decimal price, string unitService, string noteService)
        {
            connection.Open();
            string query = "UPDATE service SET name = @ServiceName, price = @Price, unit = @Unit, note = @Note WHERE id = @ServiceId";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@ServiceName", nameService);
            command.Parameters.AddWithValue("@Price", price);
            command.Parameters.AddWithValue("@Unit", unitService);
            command.Parameters.AddWithValue("@Note", noteService);
            command.Parameters.AddWithValue("@ServiceId", idService);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void DeleteService(string idService)
        {
            connection.Open();
            string query = "DELETE FROM service WHERE id = @ServiceId";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@ServiceId", idService);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public DataTable FilterRoom(string quality, string status)
        {
            connection.Open();
            DataTable dataTable = new DataTable();
            string query = "SELECT * FROM room WHERE status = @status AND quality_class = @quality";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("status", status);
            command.Parameters.AddWithValue("quality", quality);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            adapter.Fill(dataTable);
            return dataTable;
        }
        public DataTable SearchRoom(string room_num)
        {
            connection.Open();
            DataTable dataTable = new DataTable();
            string query = "SELECT * FROM room WHERE room_num = @RoomNum";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("RoomNum", room_num);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            adapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable SearchService(string name)
        {
            connection.Open();
            DataTable dataTable = new DataTable();
            string query = "SELECT * FROM service WHERE name = @ServiceName";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@ServiceName", name);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            adapter.Fill(dataTable);
            return dataTable;
        }



        // Account management
        public void InsertAccount(string phone, string email, string password, string fullName, string role)
        {
            connection.Open();
            string query = "INSERT INTO account(phone, email, password, full_name, role) VALUES (@Phone, @Email, @Password, @FullName, @Role)";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@Phone", phone);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@FullName", fullName);
            command.Parameters.AddWithValue("@Role", role);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void UpdateAccount(string phone, string email, string password, string fullName, string role)
        {
            connection.Open();
            string query = "UPDATE account SET password = @Password, email = @Email, full_name = @FullName, role = @Role WHERE phone = @Phone";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@Phone", phone);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@FullName", fullName);
            command.Parameters.AddWithValue("@Role", role);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void DeleteAccount(string phone)
        {
            connection.Open();
            string query = "DELETE FROM account WHERE phone = @Phone";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@Phone", phone);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}