using System.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using prueba2y3.Models;

namespace prueba2y3.Services;

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

    public string GetAll()
    {
        DataTable table = new DataTable();

        using (var conn = new MySqlConnection(connStr))
        {
            try
            {
                Console.WriteLine("Connecting to MySQL via MySQL Connector...");
                conn.Open();

                string sql = "SELECT * FROM productos";
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
                return "Error al consultar la data";
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

                string sql = "SELECT * FROM productos WHERE Id = @id";
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
                return $"Error al consultar la data, ID {id}";
            }
        }

        var rawdata = JsonConvert.SerializeObject(table);
        return rawdata;
    }
    public string Update(Producto item)
    {
        DataTable table = new DataTable();
        using (var conn = new MySqlConnection(connStr))
        {
            try
            {
                Console.WriteLine("Connecting to MySQL via MySQL Connector...");
                conn.Open();

                string sql =
                    "UPDATE productos SET Nombre = @nombre, Descripcion = @descripcion, Precio = @precio, CantidadEnStock = @cantidadEnStock  WHERE Id = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.Parameters.AddWithValue("@nombre", item.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", item.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", item.Precio);
                    cmd.Parameters.AddWithValue("@cantidadEnStock", item.CantidadEnStock);

                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }

                Console.WriteLine("Data updated successfully.");
            }
            catch (Exception ex)
            {
                return "Error al actualizar el registro";
            }
        }

        var rawdata = JsonConvert.SerializeObject(table);
        return rawdata;
    }
    public string Create(Producto item)
    {
        DataTable table = new DataTable();
        using (var conn = new MySqlConnection(connStr))
        {
            try
            {
                Console.WriteLine("Connecting to MySQL via MySQL Connector...");
                conn.Open();

                string sql = "INSERT INTO productos VALUES(DEFAULT, @nombre, @descripcion, @precio, @cantidadEnStock)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", item.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", item.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", item.Precio);
                    cmd.Parameters.AddWithValue("@cantidadEnStock", item.CantidadEnStock);

                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }

                Console.WriteLine("Data inserted successfully.");
            }
            catch (Exception ex)
            {
                return "Error al crear el registro";
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

                string sql = "DELETE FROM productos WHERE Id = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", clienteid);

                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }

                return "registro eliminado";
            }
            catch (Exception ex)
            {
                return "Error al borrar el registro";
            }
        }
    }
}