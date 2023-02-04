using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sintronico.Models;

namespace Sintronico.Api
{
	[Route("api/[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ApiController]
	public class PagoController : ControllerBase
	{

        private readonly DataContext Contexto;
        private readonly IConfiguration config;
		private readonly IWebHostEnvironment environment;


        public PagoController(DataContext dataContext, IConfiguration config,IWebHostEnvironment environment)
		{
			this.Contexto = dataContext;
            this.config = config;
			this.environment = environment;
		}

        [HttpGet]
		public async Task<ActionResult> ObtenerPagos(int id)
		{
			try
			{   
                var propietario = await Contexto.Propietario.FirstAsync(x => x.Email == User.Identity.Name);
                var pago = Contexto.Pago.Include(p => p.Presupuesto).Where( pa => pa.Presupuesto.Bicicleta.Dueño.IdPropietario == propietario.IdPropietario);
                return pago != null ? Ok(pago) : NotFound(); 
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

    }
}