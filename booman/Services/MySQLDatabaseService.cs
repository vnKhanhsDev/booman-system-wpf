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
<<<<<<< HEAD
        private string password = "304082";
=======
        private string password = "12345678";
>>>>>>> 8775baca743f037391eefe03f86e8cc45a514f72

        // Constructors
        public MySQLDatabaseService()
        {
            ConnectToDatabase();
        }

        // Properties
        public MySqlConnection Connection { get { return connection; } }

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
        public void InsertRoom(string roomNumber, string quality, string bedType, decimal price)
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
        public void UpdateRoom(string roomNumber, string quality, string bedType, decimal price, string status)
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
        public void InsertService(string idService, string nameService, decimal price, string unitService, string noteService)
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
        public DataTable GetRoomEmpty()
        {
            DataTable dt = new DataTable();
            connection.Open();
            string query = "SELECT * FROM room WHERE status = @Status";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("Status", "empty");
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            adapter.Fill(dt);
            connection.Close();
            return dt;
        }
        public void ChangeRoom(string roomNumCurent, string roomNumChange, string status)
        {
            connection.Open();
            string query = "UPDATE booked_rooms SET room_num = @RoomNumChange WHERE  room_num = @RoomNumCurent";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("RoomNumChange", roomNumChange);
            command.Parameters.AddWithValue("RoomNumCurent", roomNumCurent);
            command.ExecuteNonQuery();
            connection.Close();

            connection.Open();
            string query_2 = "UPDATE room SET status = @Status WHERE room_num = @RoomNumChange";
            MySqlCommand comand_2 = connection.CreateCommand();
            comand_2.CommandText = query_2;
            comand_2.Parameters.AddWithValue("Status", status);
            comand_2.Parameters.AddWithValue("RoomNumChange", roomNumChange);
            comand_2.ExecuteNonQuery();
            connection.Close();

            connection.Open();
            string query_3 = "UPDATE room SET status = @Status WHERE room_num = @RoomNumCurent";
            MySqlCommand comand_3 = connection.CreateCommand();
            comand_3.CommandText = query_3;
            comand_3.Parameters.AddWithValue("Status", "empty");
            comand_3.Parameters.AddWithValue("RoomNumCurent", roomNumCurent);
            comand_3.ExecuteNonQuery();
            connection.Close();

            connection.Open();
            string query_4 = "UPDATE room_services SET room_id = @RoomNumChange WHERE  room_id = @RoomNumCurent";
            MySqlCommand command_4 = connection.CreateCommand();
            command_4.CommandText = query_4;
            command_4.Parameters.AddWithValue("RoomNumChange", roomNumChange);
            command_4.Parameters.AddWithValue("RoomNumCurent", roomNumCurent);
            command_4.ExecuteNonQuery();
            connection.Close();
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
        public bool InsertAccount(string phone, string email, string password, string fullName, string role)
        {
            try
            {
                connection.Open();
                // Kiểm tra xem tài khoản đã tồn tại hay chưa
                string checkQuery = "SELECT COUNT(*) FROM account WHERE email = @Email OR phone = @Phone";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@Email", email);
                checkCommand.Parameters.AddWithValue("@Phone", phone);
                int existingAccountsCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (existingAccountsCount > 0)
                {
                    // Tài khoản đã tồn tại, không thể thêm vào
                    return false;
                }

                // Tài khoản không tồn tại, tiến hành thêm vào cơ sở dữ liệu
                string insertQuery = "INSERT INTO account(phone, email, password, full_name, role) VALUES (@Phone, @Email, @Password, @FullName, @Role)";
                MySqlCommand command = new MySqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@Phone", phone);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@FullName", fullName);
                command.Parameters.AddWithValue("@Role", role);
                command.ExecuteNonQuery();

                return true; // Thêm tài khoản thành công
            }
            catch (Exception ex)
            {
                // Xử lý nếu có lỗi xảy ra
                Console.WriteLine("Lỗi khi thêm tài khoản: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
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