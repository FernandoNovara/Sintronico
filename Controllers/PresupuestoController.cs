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

        public PresupuestoController()
        {
            repositorio = new RepositorioPresupuesto();
        }
        // GET: Presupuesto
        public ActionResult Index()
        {
            var lista = repositorio.ObtenerPresupuestos();
            return View(lista);
        }

        // GET: Presupuesto/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Presupuesto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Presupuesto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Presupuesto/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Presupuesto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Presupuesto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Presupuesto/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}