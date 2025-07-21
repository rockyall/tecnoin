using System.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using prueba1.Models;

namespace prueba1.Services;

public class DALService
{
    public string connStr { get; set; }

    public DALService()
    {
        var server = "db.ricardoall.dev";
        var user = "tecnoin";
        var db = "prueba1";
        var pass = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");

        // MySQL Connector/NET connection string format
        connStr = $"Server={server};Database={db};User ID={user};Password={pass};SslMode=Preferred;";
    }

    public void Init(string connStr)
    {
        using (var conn = new MySqlConnection(connStr))
        {
            try
            {
                Console.WriteLine("Connecting to MySQL via MySQL Connector...");
                conn.Open();
                Console.WriteLine("Connection successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection error: " + ex.Message);
            }
        }

        Console.WriteLine("Done.");
    }

    public string GetAll()
    {
        DataTable table = new DataTable();

        using (var conn = new MySqlConnection(connStr))
        {
            try
            {
                Console.WriteLine("Connecting to MySQL via MySQL Connector...");
                conn.Open();

                string sql = "SELECT Id, Nombre, Email FROM clientes";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }

                Console.WriteLine("Data retrieved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        var rawdata = JsonConvert.SerializeObject(table);
        return rawdata;
    }
    
    public string GetByID(int id)
    {
        DataTable table = new DataTable();
        using (var conn = new MySqlConnection(connStr))
        {
            try
            {
                Console.WriteLine("Connecting to MySQL via MySQL Connector...");
                conn.Open();

                string sql = "SELECT Id, Nombre, Email FROM clientes WHERE Id = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }

                Console.WriteLine("Data retrieved getby successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        var rawdata = JsonConvert.SerializeObject(table);
        return rawdata;
    }

    public string Update(int id, ClienteViewModel cliente)
    {
        DataTable table = new DataTable();
        using (var conn = new MySqlConnection(connStr))
        {
            try
            {
                Console.WriteLine("Connecting to MySQL via MySQL Connector...");
                conn.Open();

                string sql = "UPDATE clientes SET Nombre = @nombre, Email = @email WHERE Id = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@email", cliente.Email);  
                    
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }

                Console.WriteLine("Data updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        var rawdata = JsonConvert.SerializeObject(table);
        return rawdata;
    }

    public object Create(ClienteViewModel cliente)
    {
        DataTable table = new DataTable();
        using (var conn = new MySqlConnection(connStr))
        {
            try
            {
                Console.WriteLine("Connecting to MySQL via MySQL Connector...");
                conn.Open();

                string sql = "INSERT INTO clientes VALUES(DEFAULT, @nombre, @email)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@email", cliente.Email);  
                    
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }

                Console.WriteLine("Data inserted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        var rawdata = JsonConvert.SerializeObject(table);
        return rawdata;
    }

    public string Delete(int clienteid)
    {
        DataTable table = new DataTable();
        using (var conn = new MySqlConnection(connStr))
        {
            try
            {
                Console.WriteLine("Connecting to MySQL via MySQL Connector...");
                conn.Open();

                string sql = "DELETE FROM clientes WHERE Id = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", clienteid);
                    
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }

                return "Cliente eliminado";
            }
            catch (Exception ex)
            {
                return "No se pudo borrar el cliente.";
            }
        }
    }
}