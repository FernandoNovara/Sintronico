using MySql.Data.MySqlClient;

namespace Sintronico.Models;

public class RepositorioDetallePresupuesto
{

    String ConnectionString = "Server=localhost;User=root;Password=;Database=bicicleteria;SslMode=none";

    public RepositorioDetallePresupuesto(){ }

    public IList<DetallePresupuesto> ObtenerDetallePresupuestos()
    {
        var res = new List<DetallePresupuesto>();
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @"Select IdDetalle,IdRepuesto,IdPresupuesto,Total,Cantidad from Detalle_Presupuesto;";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                conexion.Open();
                var reader = com.ExecuteReader();
                while(reader.Read())
                {
                    var d = new DetallePresupuesto{
                        IdDetalle = reader.GetInt32(0),
                        IdRepuesto = reader.GetInt32(1),
                        IdPresupuesto = reader.GetInt32(2),
                        Total = reader.GetDouble(3),
                        Cantidad = reader.GetInt32(4)
                    };
                    res.Add(d);
                }
                conexion.Close();
            }
            return res;
        }
    }
}