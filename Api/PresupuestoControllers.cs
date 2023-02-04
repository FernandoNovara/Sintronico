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
	public class PresupuestoController : ControllerBase
	{

        private readonly DataContext Contexto;
        private readonly IConfiguration config;
		private readonly IWebHostEnvironment environment;


        public PresupuestoController(DataContext dataContext, IConfiguration config,IWebHostEnvironment environment)
		{
			this.Contexto = dataContext;
            this.config = config;
			this.environment = environment;
		}

        [HttpGet]
		public async Task<ActionResult<Presupuesto>> ObtenerPresupuestos()
		{
			try
			{   
                var propietario = await Contexto.Propietario.FirstOrDefaultAsync(x => x.Email == User.Identity.Name);
                var presupuesto = Contexto.Presupuesto.Include(b => b.Bicicleta).Where(x => x.Bicicleta.Dueño.Email == propietario.Email);
                return Ok(presupuesto);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

    }
}