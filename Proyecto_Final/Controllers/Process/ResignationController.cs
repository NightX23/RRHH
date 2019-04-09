using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Models;
using Proyecto_Final.Models.Process;
using Proyecto_Final.ViewModels;

namespace Proyecto_Final.Controllers.Process
{
    public class ResignationController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ResignationController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var searchList = _db.Resignations.Include(e => e.Employee);

            return View(await searchList.OrderBy(l => l.Id).ToListAsync());
        }

        // GET: Resignation/Create
        public IActionResult Create()
        {
            var model = new ResignationAndEmployeeViewModel
            {
                EmployeeList = _db.Employees.Where(e => e.Status == "Active").ToList()
            };

            return View(model);
        }

        // POST: Resignation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResignationAndEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employeeInDB = await _db.Employees
                    .SingleOrDefaultAsync(e => e.Id == model.ResignationObj.EmployeeId);

                if (employeeInDB == null)
                {
                    return NotFound();
                }
                else
                {
                    employeeInDB.Status = "Inactive";
                    _db.Add(model.ResignationObj);
                    await _db.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));

                }
            }

            var newmodel = new ResignationAndEmployeeViewModel();
            return View(newmodel);
        }

        // GET: Permission/Edit/#id
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = new ResignationAndEmployeeViewModel
            {
                EmployeeList = await _db.Employees.Where(e => e.Status == "Inactive").ToListAsync(),
                ResignationObj = await _db.Resignations.SingleOrDefaultAsync(r => r.Id == id)
            };

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Permission/Edit/#id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ResignationAndEmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            else
            {
                var resignationInDB = await _db.Resignations.SingleOrDefaultAsync(r => r.Id == model.ResignationObj.Id);

                if (resignationInDB == null)
                {
                    return NotFound();
                }
                else
                {
                    resignationInDB.EmployeeId = model.ResignationObj.EmployeeId;
                    resignationInDB.ResignationType = model.ResignationObj.ResignationType;
                    resignationInDB.Reason = model.ResignationObj.Reason;
                    resignationInDB.ResignationDate = model.ResignationObj.ResignationDate;

                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = new ResignationAndEmployeeViewModel
            {
                ResignationObj = await _db.Resignations.SingleOrDefaultAsync(r => r.Id == id), 
            };
            model.ResignationObj.Employee = await _db.Employees.SingleOrDefaultAsync(e => e.Id == model.ResignationObj.EmployeeId);
            model.ResignationObj.Employee.Department = await _db.Departments.SingleOrDefaultAsync(d => d.Id == model.ResignationObj.Employee.DepartmentId);
            model.ResignationObj.Employee.Position = await _db.Positions.SingleOrDefaultAsync(p => p.Id == model.ResignationObj.Employee.PositionId);

            return View(model);
        }

        // GET: Resignation/Delete/#id
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Resignation model = await _db.Resignations.SingleOrDefaultAsync(r => r.Id == id);
            model.Employee = await _db.Employees.SingleOrDefaultAsync(e => e.Id == model.EmployeeId);
            model.Employee.Department = await _db.Departments.SingleOrDefaultAsync(d => d.Id == model.Employee.DepartmentId);
            model.Employee.Position = await _db.Positions.SingleOrDefaultAsync(p => p.Id == model.Employee.PositionId);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Resignation/Delete/#id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deleting(int id)
        {
            var resignation = await _db.Resignations.SingleOrDefaultAsync(r => r.Id == id);
            resignation.Employee = await _db.Employees.SingleOrDefaultAsync(e => e.Id == resignation.EmployeeId);

            var employeeInDB = await _db.Employees.SingleOrDefaultAsync(e => e.Id == resignation.EmployeeId);

            if (employeeInDB == null)
            {
                return NotFound();
            }
            else
            {
                employeeInDB.Status = "Active";
                _db.Remove(resignation);

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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