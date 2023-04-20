namespace Sintronico.Models
{
    public interface IRepositorioBicicleta : IRepositorio<Bicicleta>
    {
        IList<Bicicleta> ObtenerBicicletas();
        Bicicleta ObtenerBicicleta(int id);
    }
}