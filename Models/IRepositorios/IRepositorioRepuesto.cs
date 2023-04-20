namespace Sintronico.Models
{
    public interface IRepositorioRepuesto : IRepositorio<Repuestos>
    {
        IList<Repuestos> ObtenerRepuestos();
        Repuestos ObtenerRepuesto(int id);
    }
}