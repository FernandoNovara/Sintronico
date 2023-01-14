using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sintronico.Models;

namespace Sintronico.Controllers
{
    public class UsuarioController : Controller
    {
        RepositorioUsuario repositorioUsuario;

        public UsuarioController()
        {
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
    }
}