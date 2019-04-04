using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Final.Data;
using Proyecto_Final.Models;

namespace Proyecto_Final.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DepartmentController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var departments = _db.Departments.ToList(); ;

            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }

        //POST Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _db.Add(department);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(department);
        }
    }
}