using MySql.Data.MySqlClient;

namespace Sintronico.Models;

public class RepositorioBicicleta
{

    String ConnectionString = "Server=localhost;User=root;Password=;Database=bicicleteria;SslMode=none";

    public RepositorioBicicleta(){ }

    public IList<Bicicleta> ObtenerBicicletas()
    {
        var res = new List<Bicicleta>();
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @"Select IdBicicleta,IdPropietario,Marca,Color,NumeroSerie,Tipo,Imagen from Bicicleta;";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                conexion.Open();
                var reader = com.ExecuteReader();
                while(reader.Read())
                {
                    var b = new Bicicleta{
                        IdBicicleta = reader.GetInt32(0),
                        IdPropietario = reader.GetInt32(1),
                        Marca = reader.GetString(2),
                        Color = reader.GetString(3),
                        NumeroSerie = reader.GetString(4),
                        Tipo = reader.GetString(5),
                        Imagen = reader.GetString(6)
                    };
                    res.Add(b);
                }
                conexion.Close();
            }
            return res;
        }
    }

    public Bicicleta ObtenerBicicleta(int id)
    {
        Bicicleta res = null;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @"Select IdBicicleta,IdPropietario,Marca,Color,NumeroSerie,Tipo,Imagen from Bicicleta where IdBicicleta = @id;";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue($"@id",id);
                conexion.Open();
                var reader = com.ExecuteReader();
                while(reader.Read())
                {
                    res = new Bicicleta{
                        IdBicicleta = reader.GetInt32(0),
                        IdPropietario = reader.GetInt32(1),
                        Marca = reader.GetString(2),
                        Color = reader.GetString(3),
                        NumeroSerie = reader.GetString(4),
                        Tipo = reader.GetString(5),
                        Imagen = reader.GetString(6)
                    };
                }
                conexion.Close();
            }
            return res;
        }
    }

    public int Alta(Bicicleta b)
    {
        var res = -1;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = $@"insert into bicicleta (IdPropietario,Marca,Color,NumeroSerie,Tipo,Imagen) 
                           Values (@{nameof(b.IdPropietario)},@{nameof(b.Marca)},@{nameof(b.Color)},@{nameof(b.NumeroSerie)},@{nameof(b.Tipo)},@{nameof(b.Imagen)});
                           Select last_Insert_Id();";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue($"@IdPropietario",b.IdPropietario);
                com.Parameters.AddWithValue($"@Marca",b.Marca);
                com.Parameters.AddWithValue($"@Color",b.Color);
                com.Parameters.AddWithValue($"@NumeroSerie",b.NumeroSerie);
                com.Parameters.AddWithValue($"@Tipo",b.Tipo);
                com.Parameters.AddWithValue($"@Imagen",b.Imagen);

                conexion.Open();
                res=Convert.ToInt32(com.ExecuteScalar());
                conexion.Close();
                b.IdBicicleta = res;
            }
            return res;
        }
    }

    public int Baja(int id)
    {
        var res = -1;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @"Delete from Bicicleta where IdBicicleta = @id;";
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

    public int Editar(Bicicleta b)
    {
        var res = -1;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @"Update bicicleta set IdPropietario = @IdPropietario,Marca = @Marca,Color = @Color,NumeroSerie = @NumeroSerie,Tipo = @Tipo,Imagen = @Imagen where IdBicicleta = @id;";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue($"@IdPropietario",b.IdPropietario);
                com.Parameters.AddWithValue($"@Marca",b.Marca);
                com.Parameters.AddWithValue($"@Color",b.Color);
                com.Parameters.AddWithValue($"@NumeroSerie",b.NumeroSerie);
                com.Parameters.AddWithValue($"@Tipo",b.Tipo);
                com.Parameters.AddWithValue($"@Imagen",b.Imagen);
                com.Parameters.AddWithValue($"@id",b.IdBicicleta);

                conexion.Open();
                res = com.ExecuteNonQuery();
                conexion.Close();
            }
            return res;
        }
    }
}