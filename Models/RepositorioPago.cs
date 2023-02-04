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

    public Pago ObtenerPago(int id)
    {
        Pago res = null;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @$"Select IdPago,IdPresupuesto,FechaEmision,Monto from Pago where IdPago = @id;";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue($"@id",id);

                conexion.Open();
                var reader = com.ExecuteReader();
                while(reader.Read())
                {
                    res = new Pago{
                        IdPago = reader.GetInt32(0),
                        IdPresupuesto = reader.GetInt32(1),
                        FechaEmision = reader.GetDateTime(2),
                        Monto = reader.GetDouble(3)
                    };
                }
                conexion.Close();
            }
            return res;
        }
    }

        public int Alta(Pago pago)
    {
        var res = -1;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @$"Insert into Pago (IdPresupuesto,FechaEmision,Monto) Values (@IdPresupuesto,@FechaEmision,@Monto); 
                           Select last_Insert_Id();";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue($"@IdPresupuesto",pago.IdPresupuesto);
                com.Parameters.AddWithValue($"@FechaEmision",pago.FechaEmision);
                com.Parameters.AddWithValue($"@Monto",pago.Monto);
                com.Parameters.AddWithValue($"@id",pago.IdPago);

                conexion.Open();
                res=Convert.ToInt32(com.ExecuteScalar());
                conexion.Close();
                pago.IdPago = res;
            }
            return res;
        }
    }

    public int Baja(int id)
    {
        var res = -1;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @$"Delete from Pago where IdPago = @id;";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue($"@id",id);

                conexion.Open();
                res = com.ExecuteNonQuery();
                conexion.Close();
            }
            return res;
        }
    }

        public int Editar(Pago pago)
    {
        var res = -1;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @$"Update Pago set IdPresupuesto = @IdPresupuesto,FechaEmision = @FechaEmision,Monto = @Monto where IdPago = @id;";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue($"@IdPresupuesto",pago.IdPresupuesto);
                com.Parameters.AddWithValue($"@FechaEmision",pago.FechaEmision);
                com.Parameters.AddWithValue($"@Monto",pago.Monto);
                com.Parameters.AddWithValue($"@id",pago.IdPago);

                conexion.Open();
                res = com.ExecuteNonQuery();
                conexion.Close();
            }
            return res;
        }
    }
}