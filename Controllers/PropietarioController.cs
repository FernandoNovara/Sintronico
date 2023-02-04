using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sintronico.Models;

namespace Sintronico.Controllers
{
    public class PropietarioController : Controller
    {
        RepositorioPropietario repositorio;
        private readonly IConfiguration configuration;

        public PropietarioController(IConfiguration configuration)
        {
            this.repositorio = new RepositorioPropietario();
            this.configuration = configuration;
        }

        // GET: Propietario
        public ActionResult Index()
        {
            var lista = repositorio.ObtenerPropietarios();
            return View(lista);
        }

        // GET: Propietario/Details/5
        public ActionResult Detalles(int id)
        {
            var p = repositorio.ObtenerPropietario(id);
            return View(p);
        }

        // GET: Propietario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Propietario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Propietario p)
        {
            try
            {
                String hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: p.Clave,
                    salt : System.Text.Encoding.ASCII.GetBytes(configuration["salt"]),
                    prf : KeyDerivationPrf.HMACSHA1,
                    iterationCount : 1000,
                    numBytesRequested : 256 / 8 
                ));
                
                p.Clave = hashed;

                var res = repositorio.Alta(p);
                if (res > 0)
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

        // GET: Propietario/Edit/5
        public ActionResult Editar(int id)
        {
            var p = repositorio.ObtenerPropietario(id);
            return View(p);
        }

        // POST: Propietario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, IFormCollection collection)
        {
            try
            {
                var p = repositorio.ObtenerPropietario(id);
                p.Nombre = collection["Nombre"];
                p.Apellido = collection["Apellido"];
                p.Dni = collection["Dni"];
                p.Telefono = collection["Telefono"];
                p.Direccion = collection["Direccion"];
                p.Email = collection["Email"];
                p.Avatar = collection["Avatar"];

                String hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: collection["Clave"],
                    salt : System.Text.Encoding.ASCII.GetBytes(configuration["salt"]),
                    prf : KeyDerivationPrf.HMACSHA1,
                    iterationCount : 1000,
                    numBytesRequested : 256 / 8 
                ));

                p.Clave = hashed;

                var res = repositorio.Editar(p);

                if (res > 0)
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

        // GET: Propietario/Delete/5
        public ActionResult Eliminar(int id)
        {
            var p = repositorio.ObtenerPropietario(id);
            return View(p);
        }

        // POST: Propietario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int id, IFormCollection collection)
        {
            try
            {
                var res = repositorio.Baja(id);

                if (res > 0)
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
    }
}