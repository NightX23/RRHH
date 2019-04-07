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
    public class VacationController : Controller
    {
        private readonly ApplicationDbContext _db;

        public VacationController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var searchList = _db.Vacations.Include(v => v.Employee);

            return View(await searchList.OrderBy(v => v.Id).ToListAsync());
        }

        //GET: Create/
        public IActionResult Create()
        {
            var model = new VacationAndEmployeeViewModel
            {
                EmployeeList = _db.Employees.ToList()
            };

            return View(model);
        }

        //POST: Create/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VacationAndEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                _db.Add(model.VacationObj);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var newmodel = new VacationAndEmployeeViewModel();

            return View(newmodel);
        }

        //GET: Edit/#id
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = new VacationAndEmployeeViewModel
            {
                EmployeeList = _db.Employees.ToList(),
                VacationObj = _db.Vacations.SingleOrDefault(v => v.Id == id)
            };


            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        //POST: Edit/#id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VacationAndEmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            else
            {
                var vacationInDB = await _db.Vacations.SingleOrDefaultAsync(v => v.Id == model.VacationObj.Id);

                if (vacationInDB == null)
                {
                    return NotFound();
                }
                else
                {
                    vacationInDB.EmployeeId = model.VacationObj.EmployeeId;
                    vacationInDB.StartDate = model.VacationObj.StartDate;
                    vacationInDB.EndDate = model.VacationObj.EndDate;
                    vacationInDB.Comment = model.VacationObj.Comment;

                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }    
        }

        //GET: Delete/#id
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vacation model = await _db.Vacations.SingleOrDefaultAsync(v => v.Id == id);
            model.Employee = await _db.Employees.SingleOrDefaultAsync(e => e.Id == model.EmployeeId);

            if (model ==  null)
            {
                return NotFound();
            }

            return View(model);
        }

        //POST: Delete/#id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Deleting(int id)
        {
            var vacation = await _db.Vacations.SingleOrDefaultAsync(v => v.Id == id);
            vacation.Employee = await _db.Employees.SingleOrDefaultAsync(e => e.Id == vacation.EmployeeId);
            _db.Remove(vacation);
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