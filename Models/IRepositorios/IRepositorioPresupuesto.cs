namespace Sintronico.Models
{
    public interface IRepositorioPresupuesto: IRepositorio<Presupuesto>
    {
        IList<Presupuesto> ObtenerPresupuestos();
        Presupuesto ObtenerPresupuesto( int id);
    }
}