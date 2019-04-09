using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Models.Process;
using Proyecto_Final.ViewModels;

namespace Proyecto_Final.Controllers.Process
{
    public class LicenseController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LicenseController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var searchList = _db.Licenses.Include(e => e.Employee);

            return View(await searchList.OrderBy(l => l.Id).ToListAsync());
        }

        // GET: License/Create
        public async Task<IActionResult> Create()
        {
            var model = new LicenseAndEmployeeViewModel
            {
                EmployeeList = await _db.Employees.Where(e => e.Status == "Active").ToListAsync()
            };

            return View(model);
        }

        // POST: License/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LicenseAndEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                _db.Add(model.LicenseObj);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            var newmodel = new LicenseAndEmployeeViewModel();
            return View(newmodel);

        }

        // GET: License/Edit/#id
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = new LicenseAndEmployeeViewModel
            {
                EmployeeList = await _db.Employees.Where(e => e.Status == "Active").ToListAsync(),
                LicenseObj = await _db.Licenses.SingleOrDefaultAsync(l => l.Id == id)
            };

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: License/Edit/#id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LicenseAndEmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            else
            {
                var licenseInDB = await _db.Licenses.SingleOrDefaultAsync(v => v.Id == model.LicenseObj.Id);

                if (licenseInDB == null)
                {
                    return NotFound();
                }
                else
                {
                    licenseInDB.EmployeeId = model.LicenseObj.EmployeeId;
                    licenseInDB.StartDate = model.LicenseObj.StartDate;
                    licenseInDB.EndDate = model.LicenseObj.EndDate;
                    licenseInDB.Reason = model.LicenseObj.Reason;
                    licenseInDB.Comment = model.LicenseObj.Comment;

                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
        }
        //GET: License/Details/#id
        public async Task<IActionResult> Details(int id)
        {
            var model = await _db.Licenses.SingleOrDefaultAsync(l => l.Id == id);

            model.Employee = await _db.Employees.SingleOrDefaultAsync(e => e.Id == model.EmployeeId);
            model.Employee.Department = await _db.Departments.SingleOrDefaultAsync(d => d.Id == model.Employee.DepartmentId);
            model.Employee.Position = await _db.Positions.SingleOrDefaultAsync(p => p.Id == model.Employee.PositionId);

            return View(model);
        }

        // GET: License/Delete/#id
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            License model = await _db.Licenses.SingleOrDefaultAsync(l => l.Id == id);
            model.Employee = await _db.Employees.SingleOrDefaultAsync(e => e.Id == model.EmployeeId);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: License/Delete/#id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deleting(int id)
        {
            var license = await _db.Licenses.SingleOrDefaultAsync(p => p.Id == id);
            license.Employee = await _db.Employees.SingleOrDefaultAsync(e => e.Id == license.EmployeeId);
            _db.Remove(license);
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