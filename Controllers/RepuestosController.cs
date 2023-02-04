using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sintronico.Models;

namespace Sintronico.Controllers
{
    public class RepuestosController : Controller
    {
        RepositorioRepuesto repositorioRepuesto;

        public RepuestosController()
        {
            repositorioRepuesto = new RepositorioRepuesto();
        }

        // GET: Repuestos
        public ActionResult Index()
        {
            var lista = repositorioRepuesto.ObtenerRepuestos();
            return View(lista);
        }

        // GET: Repuestos/Details/5
        public ActionResult Detalles(int id)
        {
            var lista = repositorioRepuesto.ObtenerRepuesto(id);
            return View(lista);
        }

        // GET: Repuestos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Repuestos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Repuestos repuesto)
        {
            try
            {
                var res = repositorioRepuesto.Alta(repuesto);

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

        // GET: Repuestos/Edit/5
        public ActionResult Editar(int id)
        {
            var lista = repositorioRepuesto.ObtenerRepuesto(id);
            return View(lista);
        }

        // POST: Repuestos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, IFormCollection collection)
        {
            try
            {
                var repuesto = repositorioRepuesto.ObtenerRepuesto(id);
                repuesto.Nombre = collection["Nombre"];
                repuesto.Tipo = collection["Tipo"];
                repuesto.Monto = Double.Parse(collection["Monto"]);
                repuesto.Detalle = collection["Detalle"];
                repuesto.Imagen = collection["Imagen"];

                var res = repositorioRepuesto.Editar(repuesto);

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

        // GET: Repuestos/Delete/5
        public ActionResult Eliminar(int id)
        {
            var lista = repositorioRepuesto.ObtenerRepuesto(id);
            return View(lista);
        }

        // POST: Repuestos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int id, IFormCollection collection)
        {
            try
            {
                var res = repositorioRepuesto.Baja(id);

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