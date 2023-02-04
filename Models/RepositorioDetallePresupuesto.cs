using MySql.Data.MySqlClient;

namespace Sintronico.Models;

public class RepositorioDetallePresupuesto
{

    String ConnectionString = "Server=localhost;User=root;Password=;Database=bicicleteria;SslMode=none";

    public RepositorioDetallePresupuesto(){ }

    public IList<DetallePresupuesto> ObtenerDetallePresupuestos(int id)
    {
        var res = new List<DetallePresupuesto>();
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @"Select IdDetalle,IdPresupuesto,Total,Cantidad,DetallePresupuesto.IdRepuesto,repuesto.Monto
                        from DetallePresupuesto
                        Join Repuesto on DetallePresupuesto.IdRepuesto = Repuesto.IdRepuesto 
                        where DetallePresupuesto.IdPresupuesto = @id;";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue(@"id",id);

                conexion.Open();
                var reader = com.ExecuteReader();
                while(reader.Read())
                {
                    var d = new DetallePresupuesto{
                        IdDetalle = reader.GetInt32(0),
                        IdPresupuesto = reader.GetInt32(1),
                        Total = reader.GetDouble(2),
                        Cantidad = reader.GetInt32(3),
                        Repuestos = new Repuestos{
                            IdRepuesto = reader.GetInt32(4),
                            Monto = reader.GetDouble(5)
                        }
                    };
                    res.Add(d);
                }
                conexion.Close();
            }
            return res;
        }

    }

    public DetallePresupuesto ObtenerDetallePresupuesto(int id)
    {
        DetallePresupuesto res = null;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @"Select IdDetalle,IdPresupuesto,Total,Cantidad,DetallePresupuesto.IdRepuesto,repuesto.Monto
                        from DetallePresupuesto
                        Join Repuesto on DetallePresupuesto.IdRepuesto = Repuesto.IdRepuesto 
                        where DetallePresupuesto.IdDetalle = @id;";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue(@"id",id);

                conexion.Open();
                var reader = com.ExecuteReader();
                while(reader.Read())
                {
                    res = new DetallePresupuesto{
                        IdDetalle = reader.GetInt32(0),
                        IdPresupuesto = reader.GetInt32(1),
                        Total = reader.GetDouble(2),
                        Cantidad = reader.GetInt32(3),
                        Repuestos = new Repuestos{
                            IdRepuesto = reader.GetInt32(4),
                            Monto = reader.GetDouble(5)
                        }
                    };
                }
                conexion.Close();
            }
            return res;
        }
    }

    public int Alta(DetallePresupuesto detalle)
    {
        var res = -1;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @$"Insert into DetallePresupuesto (IdRepuesto,IdPresupuesto,Total,Cantidad) Values (@IdRepuesto,@IdPresupuesto,@Total,@Cantidad);
                          Select last_Insert_Id();";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue(@"IdRepuesto",detalle.IdRepuesto);
                com.Parameters.AddWithValue(@"IdPresupuesto",detalle.IdPresupuesto);
                com.Parameters.AddWithValue(@"Total",detalle.Total);
                com.Parameters.AddWithValue(@"Cantidad",detalle.Cantidad);

                conexion.Open();
                res=Convert.ToInt32(com.ExecuteScalar());
                conexion.Close();
                detalle.IdDetalle = res;
            }
            return res;
        }
    }

    public int Baja(int id)
    {
        var res = -1;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @$"Delete from DetallePresupuesto where IdDetalle = @id;";
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

    public int Editar(DetallePresupuesto detalle)
    {
        var res = -1;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @$"Update DetallePresupuesto set IdRepuesto=@IdRepuesto,IdPresupuesto=@IdPresupuesto,Total=@Total,Cantidad=@Cantidad where IdDetalle = @id;";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue($"@IdRepuesto",detalle.IdRepuesto);
                com.Parameters.AddWithValue($"@IdPresupuesto",detalle.IdPresupuesto);
                com.Parameters.AddWithValue($"@Total",detalle.Total);
                com.Parameters.AddWithValue($"@Cantidad",detalle.Cantidad);
                com.Parameters.AddWithValue($"@id",detalle.IdDetalle);

                conexion.Open();
                res=com.ExecuteNonQuery();
                conexion.Close();
            }
            return res;
        }
    }
}