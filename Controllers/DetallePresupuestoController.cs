using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sintronico.Controllers
{
    public class DetallePresupuestoController : Controller
    {
        // GET: DetallePresupuesto
        public ActionResult Index()
        {
            return View();
        }

        // GET: DetallePresupuesto/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DetallePresupuesto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DetallePresupuesto/Create
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

        // GET: DetallePresupuesto/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DetallePresupuesto/Edit/5
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

        // GET: DetallePresupuesto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DetallePresupuesto/Delete/5
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