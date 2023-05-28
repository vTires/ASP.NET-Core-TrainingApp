using ASP.NETCore_TrainingApp.Models;
using Google.Apis.Sheets.v4;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASP.NETCore_TrainingApp.Services
{
    public class CloudDatabaseService
    {
        public static MySqlConnection DBConnection = new MySqlConnection();
        private readonly SheetsService _sheetsService;

        public string ipAdress;
        public string connectionName;
        public string databaseName;
        public string username;
        public string password;

        public CloudDatabaseService(string ipAddress, string connectionName, string databaseName, string username, string password)
        {
            this.ipAdress = ipAddress;
            this.connectionName = connectionName;
            this.databaseName = databaseName;
            this.username = username;
            this.password = password;
        }

        public MySqlConnectionStringBuilder NewMysqlTCPConnectionString()
        {
            var connectionString = new MySqlConnectionStringBuilder()
            {
                Server = ipAdress, 
                UserID = username,  
                Password = password, 
                Database = databaseName,
                SslMode = MySqlSslMode.Disabled,
            };
            connectionString.Pooling = true;
            return connectionString;

        }

        public List<CloudDatabaseModel> DatabaseItems { get; private set; }

        public CloudDatabaseService(SheetsService sheetsService)
        {
            _sheetsService = sheetsService;
            DatabaseItems = new List<CloudDatabaseModel>();
        }

        public void ConnectDB()
        {
            if (DBConnection.State == System.Data.ConnectionState.Closed)
            {
                  DBConnection.ConnectionString = NewMysqlTCPConnectionString().ToString();

                try
                {
                    DBConnection.Open();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        public async Task CloseConnection()
        {
            if (DBConnection.State == System.Data.ConnectionState.Open)
                DBConnection.Close();
        }

       

        //public async Task<List<CloudDatabaseModel>> GetData()
        //{
        //    using (var connection = new MySqlConnection(_connectionString))
        //    {
        //        var command = new MySqlCommand("SELECT * FROM TableName", connection);
        //        connection.Open();
        //        using (var reader = command.ExecuteReader())
        //        {
        //            var data = new List<CloudDatabaseModel>();
        //            while (reader.Read())
        //            {
        //                var item = new CloudDatabaseModel
        //                {
        //                    Id = Convert.ToInt32(reader["Id"]),
        //                    Name = reader["Name"].ToString()
        //                };
        //                data.Add(item);
        //            }

        //            return data;
        //        }
        //    }
        //}

        //public async Task InsertData(CloudDatabaseModel model)
        //{
        //    using (MySqlConnection connection = new MySqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync();

        //        string sql = "INSERT INTO table_name (Name) VALUES (@name)";
        //        using (MySqlCommand command = new MySqlCommand(sql, connection))
        //        {
        //            command.Parameters.AddWithValue("@name", model.Name);
        //            await command.ExecuteNonQueryAsync();
        //        }
        //    }
        //}
    }

}
