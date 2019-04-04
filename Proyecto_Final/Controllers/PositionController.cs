using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            var positions = _db.Positions.ToList(); ;

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
    }
}