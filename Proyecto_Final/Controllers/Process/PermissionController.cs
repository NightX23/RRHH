using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Models.Process;
using Proyecto_Final.ViewModels;

namespace Proyecto_Final.Controllers.Process
{
    public class PermissionController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PermissionController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var searchList = _db.Permissions.Include(v => v.Employee);

            return View(searchList.OrderBy(p => p.Id).ToList());
        }

        
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: Permission/Create
        public async Task<IActionResult> Create()
        {
            var model = new PermissionAndEmployeeViewModel
            {
                EmployeeList = await _db.Employees.Where(e => e.Status == "Active").ToListAsync()
            };

            return View(model);
        }

        // POST: Permission/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PermissionAndEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                _db.Add(model.PermissionObj);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            var newmodel = new PermissionAndEmployeeViewModel();
            return View(newmodel);

        }

        // GET: Permission/Edit/#id
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = new PermissionAndEmployeeViewModel
            {
                EmployeeList = await _db.Employees.Where(e => e.Status == "Active").ToListAsync(),
                PermissionObj = await _db.Permissions.SingleOrDefaultAsync(p => p.Id == id)
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
        public async Task<IActionResult> Edit(PermissionAndEmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            else
            {
                var permissionInDB = await _db.Permissions.SingleOrDefaultAsync(p => p.Id == model.PermissionObj.Id);

                if (permissionInDB == null)
                {
                    return NotFound();
                }
                else
                {
                    permissionInDB.EmployeeId = model.PermissionObj.EmployeeId;
                    permissionInDB.StartDate = model.PermissionObj.StartDate;
                    permissionInDB.EndDate = model.PermissionObj.EndDate;
                    permissionInDB.Comment = model.PermissionObj.Comment;

                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
        }

        // GET: Permission/Delete/#id
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Permission model = await _db.Permissions.SingleOrDefaultAsync(p => p.Id == id);
            model.Employee = await _db.Employees.SingleOrDefaultAsync(e => e.Id == model.EmployeeId);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Permission/Delete/#id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deleting(int id)
        {
            var permission = await _db.Permissions.SingleOrDefaultAsync(p => p.Id == id);
            permission.Employee = await _db.Employees.SingleOrDefaultAsync(e => e.Id == permission.EmployeeId);
            _db.Remove(permission);
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