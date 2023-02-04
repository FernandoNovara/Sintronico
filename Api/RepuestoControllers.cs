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
	public class RepuestoController : ControllerBase
	{

        private readonly DataContext Contexto;
        private readonly IConfiguration config;
		private readonly IWebHostEnvironment environment;


        public RepuestoController(DataContext dataContext, IConfiguration config,IWebHostEnvironment environment)
		{
			this.Contexto = dataContext;
            this.config = config;
			this.environment = environment;
		}

        [HttpGet]
		public async Task<ActionResult<Repuestos>> ObtenerRepuestos()
		{
			try
			{   
                var repuesto = Contexto.Repuesto.Where(x => x.Tipo == "Repuesto");
                return repuesto != null ? Ok(repuesto) : NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

    }
}