using MySql.Data.MySqlClient;

namespace Sintronico.Models;

public class RepositorioUsuario
{

    String ConnectionString = "Server=localhost;User=root;Password=;Database=bicicleteria;SslMode=none";

    public RepositorioUsuario(){ }

    public IList<Usuario> ObtenerUsuarios()
    {
        var res = new List<Usuario>();
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @"Select IdUsuario,Nombre,Apellido,Email,Clave,Avatar,Rol from Usuario;";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                conexion.Open();
                var reader = com.ExecuteReader();
                while(reader.Read())
                {
                    var u = new Usuario{
                        IdUsuario = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        Email = reader.GetString(3),
                        Clave = reader.GetString(4),
                        Avatar = reader.GetString(5),
                        Rol = reader.GetInt32(6)
                    };
                    res.Add(u);
                }
                conexion.Close();
            }
            return res;
        }
    }
}