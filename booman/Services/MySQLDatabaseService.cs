using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Documents;
using booman.Models;
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
        private string password = "khanh1907";

        // Constructor
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

        public InfoRoomMap InfoRoomInRoomMap(string room_num)
        {
            connection.Open();
            string query = "SELECT * FROM booked_rooms WHERE room_num = @RoomNumber";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("RoomNUmber", room_num);
            MySqlDataReader reader = command.ExecuteReader();
            string bookedID = "";
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    bookedID = reader.GetString("booking_id");
                }
            }
            connection.Close();
            connection.Open();
            InfoRoomMap newInfo = new InfoRoomMap();
            string query_2 = "SELECT * FROM booking WHERE id = @ID";
            MySqlCommand command_2 = connection.CreateCommand();
            command_2.CommandText = query_2;
            command_2.Parameters.AddWithValue("ID", bookedID);
            MySqlDataReader reader_2 = command_2.ExecuteReader();
            string customerID = "";
            if (reader_2.HasRows)
            {
                while (reader_2.Read())
                {
                    newInfo.Checkin_date = reader_2[3].ToString();
                    newInfo.Stay_duration = (int)reader_2[4];
                    newInfo.Act_checkin_time = reader_2[6].ToString();
                    customerID = reader_2[1].ToString();
                }
            }
            connection.Close();
            connection.Open();
            string query_3 = "SELECT * FROM customer WHERE id = @ID";
            MySqlCommand command_3 = connection.CreateCommand();
            command_3.CommandText = query_3;
            command_3.Parameters.AddWithValue("ID", customerID);
            MySqlDataReader reader_3 = command_3.ExecuteReader();
            if (reader_3.HasRows)
            {
                while (reader_3.Read())
                {
                    newInfo.Name = reader_3[1].ToString();
                    newInfo.Phone = reader_3[2].ToString();
                    newInfo.Email = reader_3[3].ToString();
                }
            }
            connection.Close();
            return newInfo;
        }
        public List<RoomService> GetRoomSevice(string room_num)
        {
            DataTable dataTable = new DataTable();
            List<RoomService> listService = new List<RoomService>();
            connection.Open();
            string query = "SELECT * FROM room_services WHERE room_id = @RoomNumber";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("RoomNUmber", room_num);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            adapter.Fill(dataTable);
            connection.Close();
            foreach (DataRow row in dataTable.Rows)
            {
                RoomService roomService = new RoomService();
                string service_id = row["service_id"].ToString();
                int service_quantity = (int)row["quantity"];
                roomService.Id = service_id;
                roomService.Quantity = service_quantity;
                connection.Open();
                string query_2 = "SELECT * FROM service WHERE id = @ID";
                MySqlCommand command_2 = connection.CreateCommand();
                command_2.CommandText = query_2;
                command_2.Parameters.AddWithValue("ID", service_id);
                MySqlDataReader reader = command_2.ExecuteReader();
                string name;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        name = reader.GetString("name");
                        roomService.Name = name;
                    }
                }
                listService.Add(roomService);
                connection.Close();
            }


            return listService;
        }
    }
}