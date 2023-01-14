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

    public Presupuesto ObtenerPresupuesto( int id)
    {
        Presupuesto res = null;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @"Select IdPresupuesto,IdBicicleta,IdUsuario,FechaInicio,FechaEntrega,Monto,Estado from Presupuesto where IdPresupuesto = @id;";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue($"@id",id);
                conexion.Open();
                var reader = com.ExecuteReader();
                while(reader.Read())
                {
                    res= new Presupuesto{
                        IdPresupuesto = reader.GetInt32(0),
                        IdBicicleta = reader.GetInt32(1),
                        IdUsuario = reader.GetInt32(2),
                        FechaInicio = reader.GetDateTime(3),
                        FechaEntrega = reader.GetDateTime(4),
                        Monto = reader.GetDouble(5),
                        Estado = reader.GetString(6)
                    };
                }
                conexion.Close();
            }
            return res;
        }
    }

    public int Alta( Presupuesto p )
    {
        int res = -1;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = $@"Insert into Presupuesto (IdBicicleta,IdUsuario,FechaInicio,FechaEntrega,Monto,Estado)
                           Values (@{nameof(p.IdBicicleta)},@{nameof(p.IdUsuario)},@{nameof(p.FechaInicio)},@{nameof(p.FechaEntrega)},@{nameof(p.Monto)},@{nameof(p.Estado)});
                           Select last_Insert_Id();";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue($"@{nameof(p.IdBicicleta)}",p.IdBicicleta);
                com.Parameters.AddWithValue($"@{nameof(p.IdUsuario)}",p.IdUsuario);
                com.Parameters.AddWithValue($"@{nameof(p.FechaInicio)}",p.FechaInicio);
                com.Parameters.AddWithValue($"@{nameof(p.FechaEntrega)}",p.FechaEntrega);
                com.Parameters.AddWithValue($"@{nameof(p.Monto)}",p.Monto);
                com.Parameters.AddWithValue($"@{nameof(p.Estado)}",p.Estado);

                conexion.Open();
                res=Convert.ToInt32(com.ExecuteScalar());
                conexion.Close();
                p.IdPresupuesto = res;
            }
            return res;
        }
    }

    public int Baja( int id )
    {
        var res = -1;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @$"Delete from Presupuesto where IdPresupuesto = @id;";
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

    public int Editar( Presupuesto p )
    {
        var res = -1;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @$"Update Presupuesto set IdBicicleta=@IdBicicleta,IdUsuario=@IdUsuario,FechaInicio=@FechaInicio,FechaEntrega=@FechaEntrega,Monto=@Monto,Estado=@Estado where IdPresupuesto = @id";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue($"@IdBicicleta",p.IdBicicleta);
                com.Parameters.AddWithValue($"@IdUsuario",p.IdUsuario);
                com.Parameters.AddWithValue($"@FechaInicio",p.FechaInicio);
                com.Parameters.AddWithValue($"@FechaEntrega",p.FechaEntrega);
                com.Parameters.AddWithValue($"@Monto",p.Monto);
                com.Parameters.AddWithValue($"@Estado",p.Estado);
                com.Parameters.AddWithValue($"@id",p.IdPresupuesto);

                conexion.Open();
                res = com.ExecuteNonQuery();
                conexion.Close();
            }
            return res;
        }
    }


}