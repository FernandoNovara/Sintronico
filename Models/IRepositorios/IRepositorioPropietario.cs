namespace Sintronico.Models
{
    public interface IRepositorioPropietario : IRepositorio<Propietario>
    {
        IList<Propietario> ObtenerPropietarios();

        Propietario ObtenerPropietario(int id);
    }
}