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
    public class PositionController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PositionController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var positions = _db.Positions.OrderBy(p => p.PositionName).ToList(); ;

            return View(positions);
        }

        public IActionResult Create()
        {
            return View();
        }

        //POST Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Position position)
        {
            if (ModelState.IsValid)
            {
                _db.Add(position);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(position);
        }

        //GET: Edit/#id
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Position position = await _db.Positions.SingleOrDefaultAsync(p => p.Id == id);

            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        //POST: Edit/#id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Position position)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            else
            {
                var positionInDB = await _db.Positions.SingleOrDefaultAsync(p => p.Id == position.Id);
                if (positionInDB == null)
                {
                    return NotFound();
                }

                else
                {
                    positionInDB.PositionName = position.PositionName;
                    await _db.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
        }

        //GET: Position/Delete/#id
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Position position = await _db.Positions.SingleOrDefaultAsync(p => p.Id == id);

            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        //POST: Position/Delete/#id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deleting(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var position = await _db.Positions.SingleOrDefaultAsync(p => p.Id == id);

            if (position == null)
            {
                return NotFound();
            }

            _db.Remove(position);
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