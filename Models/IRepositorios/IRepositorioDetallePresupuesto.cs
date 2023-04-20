namespace Sintronico.Models
{
    public interface IRepositorioDetallePresupuestos : IRepositorio<DetallePresupuesto>
    {
        IList<DetallePresupuesto> ObtenerDetallePresupuestos(int id);
        DetallePresupuesto ObtenerDetallePresupuesto(int id);
    }
}