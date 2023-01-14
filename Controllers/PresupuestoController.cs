using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sintronico.Models;

namespace Sintronico.Controllers
{
    public class PresupuestoController : Controller
    {
        RepositorioPresupuesto repositorio;
        RepositorioBicicleta repositorioBicicleta;
        RepositorioUsuario repositorioUsuario;

        public PresupuestoController()
        {
            repositorio = new RepositorioPresupuesto();
            repositorioBicicleta = new RepositorioBicicleta();
            repositorioUsuario = new RepositorioUsuario();
        }
        // GET: Presupuesto
        public ActionResult Index()
        {
            var lista = repositorio.ObtenerPresupuestos();
            return View(lista);
        }

        // GET: Presupuesto/Details/5
        public ActionResult Detalles(int id)
        {
            var lista = repositorio.ObtenerPresupuesto(id);
            return View(lista);
        }

        // GET: Presupuesto/Create
        public ActionResult Create()
        {
            ViewBag.Bicicletas = repositorioBicicleta.ObtenerBicicletas();
            ViewBag.Usuarios = repositorioUsuario.ObtenerUsuarios();
            return View();
        }

        // POST: Presupuesto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Presupuesto Presupuesto)
        {
            try
            {
                var res = repositorio.Alta(Presupuesto);
                if(res > 0)
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

        // GET: Presupuesto/Edit/5
        public ActionResult Editar(int id)
        {
            var lista = repositorio.ObtenerPresupuesto(id);
            return View(lista);
        }

        // POST: Presupuesto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, IFormCollection collection)
        {
            try
            {
                var p = repositorio.ObtenerPresupuesto(id);
                p.IdBicicleta = Int32.Parse(collection["IdBicicleta"]);
                p.IdUsuario = Int32.Parse(collection["IdUsuario"]);
                p.FechaInicio = DateTime.Parse(collection["FechaInicio"]) ;
                p.FechaEntrega = DateTime.Parse(collection["FechaEntrega"]);
                p.Monto = Double.Parse(collection["Monto"]);
                p.Estado = collection["Estado"];

                var res = repositorio.Editar(p);

                if(res > 0)
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

        // GET: Presupuesto/Delete/5
        public ActionResult Eliminar(int id)
        {
            var lista = repositorio.ObtenerPresupuesto(id);
            return View(lista);
        }

        // POST: Presupuesto/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int id, IFormCollection collection)
        {
            try
            {
                var res = repositorio.Baja(id);

                if(res > 0)
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