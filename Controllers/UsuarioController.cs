using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sintronico.Models;

namespace Sintronico.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IConfiguration configuration;
        RepositorioUsuario repositorioUsuario;

        public UsuarioController(IConfiguration configuration)
        {
            this.configuration = configuration;
            repositorioUsuario = new RepositorioUsuario();
        }

        // GET: Usuario
        public ActionResult Index()
        {
            var lista = repositorioUsuario.ObtenerUsuarios();
            return View(lista);
        }

        // GET: Usuario/Details/5
        public ActionResult Detalles(int id)
        {
            var lista = repositorioUsuario.ObtenerUsuario(id);
            return View(lista);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            try
            {
                String hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: usuario.Clave,
                        salt : System.Text.Encoding.ASCII.GetBytes(configuration["salt"]),
                        prf : KeyDerivationPrf.HMACSHA1,
                        iterationCount : 1000,
                        numBytesRequested : 256 / 8 
                    ));
                
                usuario.Clave = hashed;
                
                var res = repositorioUsuario.Alta(usuario);

                if(res > 0 )
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Edit/5
        public ActionResult Editar(int id)
        {
            var lista = repositorioUsuario.ObtenerUsuario(id);
            return View(lista);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, IFormCollection collection)
        {
            try
            {
                var usuario = repositorioUsuario.ObtenerUsuario(id);
                usuario.Nombre = collection["Nombre"];
                usuario.Apellido = collection["Apellido"];
                usuario.Email = collection["Email"];
                usuario.Clave = collection["Clave"];
                usuario.Avatar = collection["Avatar"];
                usuario.Rol = Int32.Parse(collection["Rol"]);

                var res = repositorioUsuario.Editar(usuario);

                if(res > 0 )
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Delete/5
        public ActionResult Eliminar(int id)
        {
            var lista = repositorioUsuario.ObtenerUsuario(id);
            return View(lista);
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int id, IFormCollection collection)
        {
            try
            {
                var res = repositorioUsuario.Baja(id);

                if(res > 0 )
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Login(String returnUrl)
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        } 

        // POST : Usuario/Login/
        [HttpPost]
        public async Task<IActionResult> Login(LoginView login)
        {
            try
            {
                var returnUrl = String.IsNullOrEmpty(TempData["returnUrl"] as String)? "/Home" : TempData["returnUrl"].ToString();
                if(ModelState.IsValid)
                {
                    String hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: login.Clave,
                        salt : System.Text.Encoding.ASCII.GetBytes(configuration["salt"]),
                        prf : KeyDerivationPrf.HMACSHA1,
                        iterationCount : 1000,
                        numBytesRequested : 256 / 8 
                    ));

                    Usuario user = repositorioUsuario.ObtenerUsuarioPorEmail(login.Usuario);
                    if(user == null || user.Clave != hashed)
                    {
                        ModelState.AddModelError("","El email o la clave ingresada es invalida");
                        TempData["returnUrl"] = returnUrl;
                        return View();
                    }

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim("FullName", user.Nombre + " " + user.Apellido),
                        new Claim(ClaimTypes.Role, user.RolNombre),
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    TempData.Remove(returnUrl);
                    return Redirect(returnUrl);
                }
                TempData["returnUrl"] = returnUrl;
                return Redirect(returnUrl);
            }
            catch(Exception ex)
            {
                throw;
            }
        }


        [Route("salir", Name = "logout")]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}