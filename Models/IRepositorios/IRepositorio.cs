namespace Sintronico.Models
{
    public interface IRepositorio<T>
    {
        int Alta(T o);

        int Baja(int id);

        int Editar(T o);
    }
}