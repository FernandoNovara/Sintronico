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

    public Usuario ObtenerUsuario(int id)
    {
        Usuario res = null;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @"Select IdUsuario,Nombre,Apellido,Email,Clave,Avatar,Rol from Usuario where IdUsuario = @id;";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue(@"id",id);

                conexion.Open();
                var reader = com.ExecuteReader();
                while(reader.Read())
                {
                    res = new Usuario{
                        IdUsuario = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        Email = reader.GetString(3),
                        Clave = reader.GetString(4),
                        Avatar = reader.GetString(5),
                        Rol = reader.GetInt32(6)
                    };
                }
                conexion.Close();
            }
            return res;
        }
    }

    public int Alta(Usuario usuario)
    {
        var res = -1;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @"insert into Usuario (Nombre,Apellido,Email,Clave,Avatar,Rol) values (@Nombre,@Apellido,@Email,@Clave,@Avatar,@Rol);
                          Select last_insert_Id();";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue(@"Nombre",usuario.Nombre);
                com.Parameters.AddWithValue(@"Apellido",usuario.Apellido);
                com.Parameters.AddWithValue(@"Email",usuario.Email);
                com.Parameters.AddWithValue(@"Clave",usuario.Clave);
                com.Parameters.AddWithValue(@"Avatar",usuario.Avatar);
                com.Parameters.AddWithValue(@"Rol",usuario.Rol);

                conexion.Open();
                res = com.ExecuteNonQuery();
                conexion.Close();
            }
            return res;
        }
    }

    public int Baja(int id)
    {
        var res = -1;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @"Delete from Usuario where IdUsuario = @id;";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue(@"id",id);

                conexion.Open();
                res = com.ExecuteNonQuery();
                conexion.Close();
            }
            return res;
        }
    }

    public int Editar(Usuario usuario)
    {
        var res = -1;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @"Update Usuario set Nombre = @Nombre,Apellido = @Apellido,Email = @Email,Clave = @Clave,Avatar = @Avatar,Rol = @Rol where IdUsuario = @id;";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue(@"id",usuario.IdUsuario);
                com.Parameters.AddWithValue(@"Nombre",usuario.Nombre);
                com.Parameters.AddWithValue(@"Apellido",usuario.Apellido);
                com.Parameters.AddWithValue(@"Email",usuario.Email);
                com.Parameters.AddWithValue(@"Clave",usuario.Clave);
                com.Parameters.AddWithValue(@"Avatar",usuario.Avatar);
                com.Parameters.AddWithValue(@"Rol",usuario.Rol);

                conexion.Open();
                res = com.ExecuteNonQuery();
                conexion.Close();
            }
            return res;
        }
    }
}