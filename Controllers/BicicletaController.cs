using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sintronico.Models;

namespace Sintronico.Controllers
{
    public class BicicletaController : Controller
    {
        RepositorioBicicleta repositorio;

        public BicicletaController()
        {
            repositorio = new RepositorioBicicleta();
        }

        // GET: Bicicleta
        public ActionResult Index()
        {
            var lista = repositorio.ObtenerBicicletas();
            return View(lista);
        }

        // GET: Bicicleta/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Bicicleta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bicicleta/Create
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

        // GET: Bicicleta/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Bicicleta/Edit/5
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

        // GET: Bicicleta/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Bicicleta/Delete/5
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