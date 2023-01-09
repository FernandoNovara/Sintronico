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
}