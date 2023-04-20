using MySql.Data.MySqlClient;

namespace Sintronico.Models;

public class RepositorioPropietario : RepositorioBase, IRepositorioPropietario
{
    public RepositorioPropietario(IConfiguration configuration) : base(configuration) { }

    public IList<Propietario> ObtenerPropietarios()
    {
        var res = new List<Propietario>();
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @"Select IdPropietario,Nombre,Apellido,Dni,Telefono,Direccion,Email,Avatar,Clave from Propietario;";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                conexion.Open();
                var reader = com.ExecuteReader();
                while(reader.Read())
                {
                    var p = new Propietario{
                        IdPropietario = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        Dni = reader.GetString(3),
                        Telefono = reader.GetString(4),
                        Direccion = reader.GetString(5),
                        Email = reader.GetString(6),
                        Avatar = reader.GetString(7),
                        Clave = reader.GetString(8)
                    };
                    res.Add(p);
                }
                conexion.Close();
            }
            return res;
        }
    }

    public Propietario ObtenerPropietario(int id)
    {
        var res = new Propietario();
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @"Select IdPropietario,Nombre,Apellido,Dni,Telefono,Direccion,Email,Avatar,Clave from Propietario where IdPropietario = @id ;";
            using (MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue($"@id",id);

                conexion.Open();
                var reader = com.ExecuteReader();
                while(reader.Read())
                {
                    res = new Propietario
                    {
                        IdPropietario = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        Dni = reader.GetString(3),
                        Telefono = reader.GetString(4),
                        Direccion = reader.GetString(5),
                        Email = reader.GetString(6),
                        Avatar = reader.GetString(7),
                        Clave = reader.GetString(8)
                    };
                    
                }
                conexion.Close();
            }
            return res;
        }
    }

    public int Alta(Propietario p)
    {
        var res = -1;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @$"insert into propietario (Nombre,Apellido,Dni,Telefono,Direccion,Email,Avatar,Clave)
                           Values (@{nameof(p.Nombre)},@{nameof(p.Apellido)},@{nameof(p.Dni)},@{nameof(p.Telefono)},@{nameof(p.Direccion)},@{nameof(p.Email)},@{nameof(p.Avatar)},@{nameof(p.Clave)});
                           Select last_Insert_Id();";
            using(MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue($"@{nameof(p.Nombre)}",p.Nombre);
                com.Parameters.AddWithValue($"@{nameof(p.Apellido)}",p.Apellido);
                com.Parameters.AddWithValue($"@{nameof(p.Dni)}",p.Dni);
                com.Parameters.AddWithValue($"@{nameof(p.Telefono)}",p.Telefono);
                com.Parameters.AddWithValue($"@{nameof(p.Direccion)}",p.Direccion);
                com.Parameters.AddWithValue($"@{nameof(p.Email)}",p.Email);
                com.Parameters.AddWithValue($"@{nameof(p.Avatar)}",p.Avatar);
                com.Parameters.AddWithValue($"@{nameof(p.Clave)}",p.Clave);

                conexion.Open();
                res=Convert.ToInt32(com.ExecuteScalar());
                conexion.Close();
                p.IdPropietario = res;
            }
            return res;
        }
    }

    public int Baja(int id)
    {
        var res = -1;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @$"Delete from propietario where IdPropietario = @id;";
            using(MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue($"@id",id);

                conexion.Open();
                res = com.ExecuteNonQuery();
                conexion.Close();
            }
            return res;
        }
    }

    public int Editar(Propietario p)
    {
        var res = -1;
        using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
        {
            String sql = @$"Update propietario set Nombre=@Nombre,Apellido=@Apellido,Dni=@Dni,Telefono=@Telefono,Direccion=@Direccion,Email=@Email,Avatar=@Avatar,Clave=@Clave where idPropietario = @id ";
            using(MySqlCommand com = new MySqlCommand(sql,conexion))
            {
                com.Parameters.AddWithValue("@Nombre",p.Nombre);
                com.Parameters.AddWithValue("@Apellido",p.Apellido);
                com.Parameters.AddWithValue("@Dni",p.Dni);
                com.Parameters.AddWithValue("@Telefono",p.Telefono);
                com.Parameters.AddWithValue("@Direccion",p.Direccion);
                com.Parameters.AddWithValue("@Email",p.Email);
                com.Parameters.AddWithValue("@Avatar",p.Avatar);
                com.Parameters.AddWithValue("@Clave",p.Clave);
                com.Parameters.AddWithValue("@id",p.IdPropietario);

                conexion.Open();
                res=com.ExecuteNonQuery();
                conexion.Close();
            }
            return res;
        }
    }
}