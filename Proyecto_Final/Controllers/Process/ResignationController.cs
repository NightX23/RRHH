using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Models;
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
                EmployeeList = await _db.Employees.Where(e => e.Status == "Active").ToListAsync(),
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }
    }
}