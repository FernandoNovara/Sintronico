using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sintronico.Models;

namespace Sintronico.Controllers
{
    public class DetallePresupuestoController : Controller
    {

        private IRepositorioDetallePresupuestos repositorio;
        private IRepositorioRepuesto repositorioRepuesto;
        private IRepositorioPresupuesto repositorioPresupuesto;
 
        public DetallePresupuestoController(IRepositorioDetallePresupuestos repositorio, IRepositorioRepuesto repositorioRepuesto, IRepositorioPresupuesto repositorioPresupuesto)
        {
            this.repositorio = repositorio;
            this.repositorioRepuesto = repositorioRepuesto;
            this.repositorioPresupuesto = repositorioPresupuesto;
        }

        // GET: DetallePresupuesto
        public ActionResult Index(int id)
        {
            var lista = repositorio.ObtenerDetallePresupuestos(id);
            ViewBag.IdPresupuesto = id;
            return View(lista);
        }

        // GET: DetallePresupuesto/Details/5
        public ActionResult Detalles(int id)
        {
            var lista = repositorio.ObtenerDetallePresupuesto(id);
            return View(lista);
        }

        // GET: DetallePresupuesto/Create
        public ActionResult CreateArreglo()
        {
            ViewBag.Presupuesto = repositorioPresupuesto.ObtenerPresupuestos();
            return View();
        }

               // POST: DetallePresupuesto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateArreglo(DetallePresupuesto detalle)
        {
            try
            {
                var res = repositorio.Alta(detalle);
 
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

        public ActionResult CreateRepuesto(int id)
        {
            ViewBag.Repuestos = repositorioRepuesto.ObtenerRepuestos();
            ViewBag.IdPresupuesto = id;
            return View();
        }

        // POST: DetallePresupuesto/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRepuesto(DetallePresupuesto detalle)
        {
            try
            {
                var repuesto = repositorioRepuesto.ObtenerRepuesto(detalle.IdRepuesto);
                detalle.Total = repuesto.Monto * detalle.Cantidad;
                var res = repositorio.Alta(detalle);
 
                if(res > 0)
                {
                    return RedirectToAction(nameof(Index),new {id = detalle.IdPresupuesto});
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

        // GET: DetallePresupuesto/Edit/5
        public ActionResult Editar(int id)
        {
            ViewBag.Repuestos = repositorioRepuesto.ObtenerRepuestos();
            ViewBag.Presupuesto = repositorioPresupuesto.ObtenerPresupuestos();
            var lista = repositorio.ObtenerDetallePresupuesto(id);
            return View(lista);
        }

        // POST: DetallePresupuesto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, IFormCollection collection)
        {
            try
            {
                var detalle = repositorio.ObtenerDetallePresupuesto(id);
                detalle.IdRepuesto = Int32.Parse(collection["IdRepuesto"]);
                detalle.IdPresupuesto = Int32.Parse(collection["IdPresupuesto"]);
                detalle.Total = Double.Parse(collection["Total"]);
                detalle.Cantidad = Int32.Parse(collection["Cantidad"]);

                var res = repositorio.Editar(detalle);

                if(res > 0)
                {
                    return RedirectToAction(nameof(Index),new {id = detalle.IdPresupuesto});
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

        // GET: DetallePresupuesto/Delete/5
        public ActionResult Eliminar(int id)
        {
            var lista = repositorio.ObtenerDetallePresupuesto(id);
            ViewBag.IdPresupuesto = lista.IdPresupuesto;
            return View(lista);
        }

        // POST: DetallePresupuesto/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int id, IFormCollection collection)
        {
            try
            {
                var res = repositorio.Baja(id);

                if(res > 0)
                {
                    return RedirectToAction(nameof(Index),new {id = collection["IdPresupuesto"]});
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