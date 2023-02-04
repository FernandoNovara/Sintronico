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
	public class BicicletaController : ControllerBase
	{

        private readonly DataContext Contexto;
        private readonly IConfiguration config;
		private readonly IWebHostEnvironment environment;


        public BicicletaController(DataContext dataContext, IConfiguration config,IWebHostEnvironment environment)
		{
			this.Contexto = dataContext;
            this.config = config;
			this.environment = environment;
		}

		// GET: api/<controller>
		[HttpGet]
		public async Task<ActionResult<Bicicleta>> Get()
		{
			try
			{
				var propietario = await Contexto.Propietario.FirstOrDefaultAsync(x => x.Email == User.Identity.Name );
                var bicicleta = Contexto.Bicicleta.Where(x => x.Dueño.IdPropietario == propietario.IdPropietario);
                return bicicleta != null ? Ok(bicicleta) : NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<ActionResult> ObtenerBicicletaPorId(int id)
		{
			try
			{
				var propietario = await Contexto.Propietario.FirstOrDefaultAsync(x => x.Email == User.Identity.Name );
                var bicicleta = Contexto.Bicicleta.Include(x => x.Dueño).Where(y => y.Dueño.Email == propietario.Email).SingleOrDefault(z => z.IdBicicleta == id);
                return bicicleta != null ? Ok(bicicleta) : NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		// GET: api/<controller>
		[HttpPost("AltaBicicleta")]
		public async Task<IActionResult> AltaBicicleta([FromBody] Bicicleta bicicleta)
		{
			try
			{
				if(ModelState.IsValid)
				{
					var propietario = await Contexto.Propietario.FirstOrDefaultAsync(x => x.Email == User.Identity.Name);
					bicicleta.IdPropietario = propietario.IdPropietario;

					// falta agregar la imagen

					await Contexto.Bicicleta.AddAsync(bicicleta);
					Contexto.SaveChanges();
					return CreatedAtAction(nameof(Get),new {id = bicicleta.IdBicicleta},bicicleta);

				}
                return Ok("llamo a un propietario");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
    }
}