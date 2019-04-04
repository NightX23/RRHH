using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Final.Data;
using Proyecto_Final.Models;
using Microsoft.EntityFrameworkCore;
namespace Proyecto_Final.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Create));
        }

        public IActionResult Create()
        {
            //DROPDOWNLISTS DATA HOLDERS
            List<Department> departmentsList = new List<Department>();
            List<Position> positionsList = new List<Position>();

            //GETTING DATA FROM DATABASE
            departmentsList = (from departmentName in _db.Departments
                               select departmentName).ToList();

            positionsList = (from positionName in _db.Positions
                               select positionName).OrderBy(p=>p).ToList();

            //INSERTING SELECT ITEM IN LIST
            departmentsList.Insert(0, new Department { Id = 0, DepartmentName = "Select" });
            positionsList.Insert(0, new Position { Id = 0, PositionName = "Select" });

            //SENDING LISTS OF DATA TO THE VIEW
            ViewBag.ListofDepartments = departmentsList;
            ViewBag.ListofPositions = positionsList;

            return View();
        }
        
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _db.Add(employee);
                await _db.SaveChangesAsync();
                return View();
            }
            return View(employee);
        }
    }
}