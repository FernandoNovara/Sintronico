using MySql.Data.MySqlClient;

namespace Sintronico.Models;

public class RepositorioPresupuesto
{

    String ConnectionString = "Server=localhost;User=root;Password=;Database=bicicleteria;SslMode=none";

    public RepositorioPresupuesto(){ }

    public IList<Presupuesto> ObtenerPresupuestos()
    {
        var res = new List<Presupuesto>();
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @"Select IdPresupuesto,IdBicicleta,IdUsuario,FechaInicio,FechaEntrega,Monto,Estado from Presupuesto;";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                conexion.Open();
                var reader = com.ExecuteReader();
                while(reader.Read())
                {
                    var p = new Presupuesto{
                        IdPresupuesto = reader.GetInt32(0),
                        IdBicicleta = reader.GetInt32(1),
                        IdUsuario = reader.GetInt32(2),
                        FechaInicio = reader.GetDateTime(3),
                        FechaEntrega = reader.GetDateTime(4),
                        Monto = reader.GetDouble(5),
                        Estado = reader.GetString(6)
                    };
                    res.Add(p);
                }
                conexion.Close();
            }
            return res;
        }
    }


}