using MySql.Data.MySqlClient;

namespace Sintronico.Models;

public class RepositorioRepuesto : RepositorioBase, IRepositorioRepuesto
{
    public RepositorioRepuesto(IConfiguration configuration) : base(configuration) { }

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

    public Repuestos ObtenerRepuesto( int id)
    {
        Repuestos res = null;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @"Select IdRepuesto,Nombre,Tipo,Monto,Detalle,Imagen from Repuesto where IdRepuesto = @id;";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue($"@id",id);
                conexion.Open();
                var reader = com.ExecuteReader();
                while(reader.Read())
                {
                    res= new Repuestos{
                        IdRepuesto = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Tipo = reader.GetString(2),
                        Monto = reader.GetDouble(3),
                        Detalle = reader.GetString(4),
                        Imagen = reader.GetString(5)
                    };
                }
                conexion.Close();
            }
            return res;
        }
    }

    public int Alta( Repuestos r )
    {
        int res = -1;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @$"Insert into Repuesto (Nombre,Tipo,Monto,Detalle,Imagen)
                           Values (@{nameof(r.Nombre)},@{nameof(r.Tipo)},@{nameof(r.Monto)},@{nameof(r.Detalle)},@{nameof(r.Imagen)});
                           Select last_Insert_Id();";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue($"@{nameof(r.Nombre)}",r.Nombre);
                com.Parameters.AddWithValue($"@{nameof(r.Tipo)}",r.Tipo);
                com.Parameters.AddWithValue($"@{nameof(r.Monto)}",r.Monto);
                com.Parameters.AddWithValue($"@{nameof(r.Detalle)}",r.Detalle);
                com.Parameters.AddWithValue($"@{nameof(r.Imagen)}",r.Imagen);

                conexion.Open();
                res=Convert.ToInt32(com.ExecuteScalar());
                conexion.Close();
                r.IdRepuesto = res;
            }
            return res;
        }
    }

    public int Baja( int id )
    {
        var res = -1;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @$"Delete from Repuesto where IdRepuesto = @id;";
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

    public int Editar( Repuestos r )
    {
        var res = -1;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @$"Update Repuesto set Nombre=@Nombre,Tipo=@Tipo,Monto=@Monto,Detalle=@Detalle,Imagen=@Imagen where IdRepuesto = @id";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue($"@Nombre",r.Nombre);
                com.Parameters.AddWithValue($"@Tipo",r.Tipo);
                com.Parameters.AddWithValue($"@Monto",r.Monto);
                com.Parameters.AddWithValue($"@Detalle",r.Detalle);
                com.Parameters.AddWithValue($"@Imagen",r.Imagen);
                com.Parameters.AddWithValue($"@id",r.IdRepuesto);

                conexion.Open();
                res = com.ExecuteNonQuery();
                conexion.Close();
            }
            return res;
        }
    }
}