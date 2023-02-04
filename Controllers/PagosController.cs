using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sintronico.Models;

namespace Sintronico.Controllers
{
    public class PagosController : Controller
    {
        RepositorioPago repositorioPagos;
        RepositorioPresupuesto repositorioPresupuesto;

        public PagosController()
        {
            repositorioPagos = new RepositorioPago();
            repositorioPresupuesto = new RepositorioPresupuesto();
        }

        // GET: Pagos
        public ActionResult Index()
        {
            var lista = repositorioPagos.ObtenerPagos();
            return View(lista);
        }

        // GET: Pagos/Details/5
        public ActionResult Detalles(int id)
        {
            var lista = repositorioPagos.ObtenerPago(id);
            return View(lista);
        }

        // GET: Pagos/Create
        public ActionResult Create()
        {
            ViewBag.Presupuesto = repositorioPresupuesto.ObtenerPresupuestos();
            return View();
        }

        // POST: Pagos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pago pago)
        {
            try
            {
                var res = repositorioPagos.Alta(pago);

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

        // GET: Pagos/Edit/5
        public ActionResult Editar(int id)
        {
            ViewBag.Presupuesto = repositorioPresupuesto.ObtenerPresupuestos();
            var lista = repositorioPagos.ObtenerPago(id);
            return View(lista);
        }

        // POST: Pagos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, IFormCollection collection)
        {
            try
            {
                var pago = new Pago();
                pago.IdPresupuesto = Int32.Parse(collection["IdPresupuesto"]);
                pago.FechaEmision = DateTime.Parse(collection["FechaEmision"]);
                pago.Monto = Double.Parse(collection["Monto"]);

                var res = repositorioPagos.Editar(pago);

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

        // GET: Pagos/Delete/5
        public ActionResult Eliminar(int id)
        {
            var lista = repositorioPagos.ObtenerPago(id);
            return View(lista);
        }

        // POST: Pagos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int id, IFormCollection collection)
        {
            try
            {
                var res = repositorioPagos.Baja(id);

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