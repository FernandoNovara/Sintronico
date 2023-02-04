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
	public class DetallePresupuestoController : ControllerBase
	{

        private readonly DataContext Contexto;
        private readonly IConfiguration config;
		private readonly IWebHostEnvironment environment;


        public DetallePresupuestoController(DataContext dataContext, IConfiguration config,IWebHostEnvironment environment)
		{
			this.Contexto = dataContext;
            this.config = config;
			this.environment = environment;
		}

        [HttpGet("{id}")]
		public async Task<ActionResult> ObtenerDetalle(int id)
		{
			try
			{   
                var detalle = await Contexto.DetallePresupuesto.FirstAsync(x => x.IdPresupuesto == id);
                return detalle != null ? Ok(detalle) : NotFound(); 
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

    }
}