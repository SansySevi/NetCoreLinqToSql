﻿using Microsoft.AspNetCore.Mvc;
using NetCoreLinqToSqlSSG.Models;
using NetCoreLinqToSqlSSG.Repositories;

namespace NetCoreLinqToSqlSSG.Controllers
{
    public class EmpleadosController : Controller
    {
        private RepositoryEmpleados repo;

        public EmpleadosController()
        {
            this.repo = new RepositoryEmpleados();
        }

        public IActionResult Index()
        {
            List<Empleado> empleados = this.repo.GetEmpleados();
            return View(empleados);
        }

        public IActionResult Details(int id)
        {
            Empleado empleado = this.repo.FindEmpleado(id);
            return View(empleado);
        }

        public IActionResult BuscadorEmpleados()
        {
            List<Empleado> empleados = this.repo.GetEmpleados();
            return View(empleados);
        }

        [HttpPost]
        public IActionResult BuscadorEmpleados(string oficio, int salario)
        {
            List<Empleado> empleados =
                this.repo.GetEmpleados(oficio.ToUpper(), salario);
            if (empleados == null)
            {
                ViewData["MENSAJE"] = "No existen empleados con oficio " + oficio
                    + " o salario superior a " + salario;
                return View();
            }
            else
            {
                return View(empleados);
            }
        }


    }
}
