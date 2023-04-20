namespace Sintronico.Models
{
    public abstract class RepositorioBase
    {
        protected readonly IConfiguration configuration;
        protected readonly String ConnectionString;

        protected RepositorioBase(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.ConnectionString = configuration["ConnectionString:DefaultConnection"];
        }
    }

}