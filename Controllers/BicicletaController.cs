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
        RepositorioPropietario RepositorioPropietario;

        public BicicletaController()
        {
            repositorio = new RepositorioBicicleta();
            RepositorioPropietario = new RepositorioPropietario();
        }

        // GET: Bicicleta
        public ActionResult Index()
        {
            var lista = repositorio.ObtenerBicicletas();
            return View(lista);
        }

        // GET: Bicicleta/Details/5
        public ActionResult Detalles(int id)
        {
            var lista = repositorio.ObtenerBicicleta(id);
            return View(lista);
        }

        // GET: Bicicleta/Create
        public ActionResult Create()
        {
            ViewBag.Propietario = RepositorioPropietario.ObtenerPropietarios();
            return View();
        }

        // POST: Bicicleta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Bicicleta bicicleta)
        {
            try
            {
                var res = repositorio.Alta(bicicleta);

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

        // GET: Bicicleta/Edit/5
        public ActionResult Editar(int id)
        {
            ViewBag.Propietario = RepositorioPropietario.ObtenerPropietarios();
            var lista = repositorio.ObtenerBicicleta(id);
            return View(lista);
        }

        // POST: Bicicleta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, IFormCollection collection)
        {
            try
            {   
                var b = repositorio.ObtenerBicicleta(id);
                b.IdPropietario = Int32.Parse(collection["IdPropietario"]);
                b.Marca = collection["Marca"];
                b.Color = collection["Color"];
                b.NumeroSerie = collection["NumeroSerie"];
                b.Tipo = collection["Tipo"];
                b.Imagen = collection["Imagen"];

                var res = repositorio.Editar(b);

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

        // GET: Bicicleta/Delete/5
        public ActionResult Eliminar(int id)
        {
            var lista = repositorio.ObtenerBicicleta(id);
            return View(lista);
        }

        // POST: Bicicleta/Delete/5
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