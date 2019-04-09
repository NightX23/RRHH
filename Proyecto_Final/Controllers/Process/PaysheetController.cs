using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Final.Data;
using Proyecto_Final.Models.Process;

namespace Proyecto_Final.Controllers.Process
{
    public class PaysheetController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PaysheetController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string year = null)
        {
            var paysheetList = _db.Paysheets.ToList();

            if (year != null)
            {
                 paysheetList = _db.Paysheets.Where(p => p.Year == Convert.ToInt32(year)).ToList();
            }

            return View(paysheetList);
        }

        //GET: Paysheet/Create
        public  IActionResult Create()
        {
            var recentPaysheet = _db.Paysheets
                                .OrderByDescending(p => p.Id).First();

            if (recentPaysheet.Year == DateTime.Now.Year)
            {
                if (recentPaysheet.Month == DateTime.Now.Month)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            Paysheet model = new Paysheet
            {
                Year = DateTime.Now.Year,
                Month = DateTime.Now.Month
            };
            var total = from n in _db.Employees
                        where n.Status == "Active"
                        select n.Salary;
            model.TotalAmount = total.Sum();
            return View(model);
        }

        //POST: Paysheet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Paysheet paysheet)
        {
            if (ModelState.IsValid)
            {
                _db.Add(paysheet);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(paysheet);
        }

        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Paysheet paysheet =  _db.Paysheets.SingleOrDefault(p => p.Id == id);
            if (paysheet == null)
            {
                return NotFound();
            }
            return View(paysheet);
        }

        //POST:Contact/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deleting(int id)
        {
            var paysheet = _db.Paysheets.SingleOrDefault(p => p.Id == id);
            _db.Remove(paysheet);
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