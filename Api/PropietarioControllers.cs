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
	public class PropietarioController : ControllerBase
	{

        private readonly DataContext Contexto;
        private readonly IConfiguration config;
		private readonly IWebHostEnvironment environment;


        public PropietarioController(DataContext dataContext, IConfiguration config,IWebHostEnvironment environment)
		{
			this.Contexto = dataContext;
            this.config = config;
			this.environment = environment;
		}

        // GET: api/<controller>
		[HttpGet]
		public async Task<ActionResult<Propietario>> Get()
		{
			try
			{
				// Funciona retorna los datos del usuario logueado
                var propietario = await Contexto.Propietario.FirstOrDefaultAsync(x => x.Email == User.Identity.Name );
                return propietario != null ? Ok(propietario) : NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

        // Post: api/<controller>
		[HttpPost("actualizar")]
		public async Task<ActionResult> Actualizar([FromBody] Propietario propietario)
		{
			try
			{
                if(ModelState.IsValid)
                {
                        var Original = await Contexto.Propietario.AsNoTracking().SingleAsync(x => x.Email == User.Identity.Name);
                        propietario.IdPropietario = Original.IdPropietario;

                        if(String.IsNullOrEmpty(propietario.Clave))
                        {
                            propietario.Clave = Original.Clave;
                        }
                        else
                        {
                            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                password: propietario.Clave,
                                salt: System.Text.Encoding.ASCII.GetBytes(config["Salt"]),
                                prf: KeyDerivationPrf.HMACSHA1,
                                iterationCount: 1000,
                                numBytesRequested: 256 / 8));

                            propietario.Clave = hashed;
                        }
                        Contexto.Propietario.Update(propietario);
                        await Contexto.SaveChangesAsync();
                        return Ok(propietario);
                }
                else
				{
					return BadRequest();
				}   
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 


        // POST api/<controller>/login
		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<IActionResult> Login([FromBody] LoginView loginView)
		{
			try
			{
				string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: loginView.Clave,
				salt: System.Text.Encoding.ASCII.GetBytes(config["Salt"]),
				prf: KeyDerivationPrf.HMACSHA1,
				iterationCount: 1000,
				numBytesRequested: 256 / 8));
				var p = await Contexto.Propietario.FirstOrDefaultAsync(x => x.Email == loginView.Usuario);
				if (p == null || p.Clave != hashed)
				{
					return BadRequest("Nombre de usuario o clave incorrecta");
				}
				else
				{
					var key = new SymmetricSecurityKey(
						System.Text.Encoding.ASCII.GetBytes(config["TokenAuthentication:SecretKey"]));
					var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, p.Email),
						new Claim("FullName", p.Nombre + " " + p.Apellido),
						new Claim(ClaimTypes.Role, "Propietarios"),
					};

					var token = new JwtSecurityToken(
						issuer: config["TokenAuthentication:Issuer"],
						audience: config["TokenAuthentication:Audience"],
						claims: claims,
						expires: DateTime.Now.AddMinutes(60),
						signingCredentials: credenciales
					);
					return Ok(new JwtSecurityTokenHandler().WriteToken(token));
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

    }
}