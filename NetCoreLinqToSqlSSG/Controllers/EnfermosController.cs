using Microsoft.AspNetCore.Mvc;
using NetCoreLinqToSqlSSG.Models;
using NetCoreLinqToSqlSSG.Repositories;

namespace NetCoreLinqToSqlSSG.Controllers
{
    public class EnfermosController : Controller
    {
        private RepositoryEnfermos repo;

        public EnfermosController()
        {
            this.repo = new RepositoryEnfermos();
        }

        public IActionResult Index()
        {
            List<Enfermo> enfermos = this.repo.GetEnfermos();
            return View(enfermos);
        }

        public IActionResult BuscadorEnfermos()
        {
            List<Enfermo> enfermos = this.repo.GetEnfermos();
            return View(enfermos);
        }

        [HttpPost]
        public IActionResult BuscadorEnfermos(DateTime fechanacimiento)
        {
            List<Enfermo> enfermos =
                this.repo.GetEnfermos(fechanacimiento);
            if (enfermos == null)
            {
                ViewData["MENSAJE"] = "No existen enfermos que hayan nacido " + fechanacimiento;
                return View();
            }
            else
            {
                return View(enfermos);
            }
        }

        public IActionResult Delete(int inscripcion)
        {
            this.repo.Delete(inscripcion);
            return RedirectToAction("Index");
        }

    }
}
