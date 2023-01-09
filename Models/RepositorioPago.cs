using MySql.Data.MySqlClient;

namespace Sintronico.Models;

public class RepositorioPago
{

    String ConnectionString = "Server=localhost;User=root;Password=;Database=bicicleteria;SslMode=none";

    public RepositorioPago(){ }

    public IList<Pago> ObtenerPagos()
    {
        var res = new List<Pago>();
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @"Select IdPago,IdPresupuesto,FechaEmision,Monto from Pago;";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                conexion.Open();
                var reader = com.ExecuteReader();
                while(reader.Read())
                {
                    var p = new Pago{
                        IdPago = reader.GetInt32(0),
                        IdPresupuesto = reader.GetInt32(1),
                        FechaEmision = reader.GetDateTime(2),
                        Monto = reader.GetDouble(3)
                    };
                    res.Add(p);
                }
                conexion.Close();
            }
            return res;
        }
    }
}