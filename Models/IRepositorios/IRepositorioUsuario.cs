namespace Sintronico.Models
{
    public interface IRepositorioUsuario : IRepositorio<Usuario>
    {
        public Usuario ObtenerUsuario(int id);
        IList<Usuario> ObtenerUsuarios();
        Usuario ObtenerUsuarioPorEmail(String email);
    }
}