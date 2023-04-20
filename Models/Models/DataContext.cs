using Microsoft.EntityFrameworkCore;

namespace Sintronico.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        
        public DbSet<Propietario> Propietario { get; set; }
        public DbSet<Bicicleta> Bicicleta { get; set; }
        public DbSet<Presupuesto> Presupuesto { get; set; }
        public DbSet<DetallePresupuesto> DetallePresupuesto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Repuestos> Repuesto { get; set; }
        public DbSet<Pago> Pago { get; set; }

    }
}