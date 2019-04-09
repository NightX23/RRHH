using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Final.Data;
using Proyecto_Final.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_Final.ViewModels;

namespace Proyecto_Final.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(string option = null, string search =  null)
        {
            var searchResult = _db.Employees.Where(e => e.Status == "Active")
                .Include(e => e.Position).Include(e => e.Department);

            //if (option == "name" && search != null)
            //{
            //    searchResult = _db.Employees.Where(u => u.FirstName.ToLower().Contains(search.ToLower()))
            //        .Include(e => e.Department).Include(e => e.Position).ToListAsync();
            //}

            return View(await searchResult.OrderByDescending(e => e.Id).ToListAsync());
        }

        public IActionResult Create()
        {
            var model = new DepartmentAndPositionInEmployeeViewModel
            {
                DepartmentObj = _db.Departments.OrderBy(d => d.DepartmentName).ToList(),
                PositionObj = _db.Positions.OrderBy(p => p.PositionName).ToList()
            };

            return View(model);
        }
        
        //POST: Create/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentAndPositionInEmployeeViewModel model)
        {
            model.EmployeeObj.Department = await _db.Departments.SingleOrDefaultAsync(d => d.Id == model.EmployeeObj.DepartmentId);
            if (ModelState.IsValid)
            {
                _db.Add(model.EmployeeObj);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var newmodel = new DepartmentAndPositionInEmployeeViewModel
            {
                DepartmentObj = _db.Departments.ToList(),
                PositionObj = _db.Positions.ToList()
            };

            return View(newmodel);
        }



        //GET: Edit/#id
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DepartmentAndPositionInEmployeeViewModel model = new DepartmentAndPositionInEmployeeViewModel()
            {
                EmployeeObj = await _db.Employees.Include(e => e.Position).Include(e => e.Department)
                .SingleOrDefaultAsync(e => e.Id == id),
                DepartmentObj = await _db.Departments.ToListAsync(),
                PositionObj = await _db.Positions.ToListAsync()

            };
            if(model == null)
            {
                return NotFound();
            }

            //model.EmployeeObj.Department = await _db.Departments.SingleOrDefaultAsync(d => d.Id == model.EmployeeObj.DepartmentId);
            //model.EmployeeObj.Position = await _db.Positions.SingleOrDefaultAsync(p => p.Id == model.EmployeeObj.PositionId);

            return View(model);
        }

        //POST: Edit/#id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DepartmentAndPositionInEmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var employeeInDB = await _db.Employees.SingleOrDefaultAsync(e => e.Id == model.EmployeeObj.Id);

                if (employeeInDB ==  null)
                {
                    return NotFound();
                }
                else
                {
                    employeeInDB = model.EmployeeObj;

                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee employee = await _db.Employees.SingleOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.Department = await _db.Departments.SingleOrDefaultAsync(d => d.Id == employee.DepartmentId);
            employee.Position = await _db.Positions.SingleOrDefaultAsync(p => p.Id == employee.PositionId);

            return View(employee);
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