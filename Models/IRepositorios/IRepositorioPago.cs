namespace Sintronico.Models
{
    public interface IRepositorioPago : IRepositorio<Pago>
    {
        IList<Pago> ObtenerPagos();
        Pago ObtenerPago(int id);
    }
}