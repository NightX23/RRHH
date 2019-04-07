using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        //GET: Edit/#id
        public async Task<IActionResult> Edit(int id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Department department = await _db.Departments.SingleOrDefaultAsync(d => d.Id == id);

            if(department == null)
            {
                return NotFound();
            }

            return View(department);
        }
        
        //POST: Edit/#id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Department department)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            else
            {
                var departmentInDB = await _db.Departments.SingleOrDefaultAsync(d => d.Id == department.Id);
                if (departmentInDB ==  null)
                {
                    return NotFound();
                }

                else
                {
                    departmentInDB.DepartmentName = department.DepartmentName;
                    await _db.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }                
            }
        }

        //GET: Department/Delete/#id
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Department department = await _db.Departments.SingleOrDefaultAsync(d => d.Id == id);

            if (department == null)
            {
                return NotFound();
            }

            return  View(department);
        }

        //POST: Department/Delete/#id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deleting(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _db.Departments.SingleOrDefaultAsync(d => d.Id == id);

            if (department == null)
            {
                return NotFound();
            }

            _db.Remove(department);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }
    }
}