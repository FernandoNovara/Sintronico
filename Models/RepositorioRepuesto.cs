using MySql.Data.MySqlClient;

namespace Sintronico.Models;

public class RepositorioRepuesto
{

    String ConnectionString = "Server=localhost;User=root;Password=;Database=bicicleteria;SslMode=none";

    public RepositorioRepuesto(){ }

    public IList<Repuestos> ObtenerRepuestos()
    {
        var res = new List<Repuestos>();
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @"Select IdRepuesto,Nombre,Tipo,Monto,Detalle,Imagen from Repuesto;";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                conexion.Open();
                var reader = com.ExecuteReader();
                while(reader.Read())
                {
                    var r = new Repuestos{
                        IdRepuesto = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Tipo = reader.GetString(2),
                        Monto = reader.GetDouble(3),
                        Detalle = reader.GetString(4),
                        Imagen = reader.GetString(5)
                    };
                    res.Add(r);
                }
                conexion.Close();
            }
            return res;
        }
    }
}