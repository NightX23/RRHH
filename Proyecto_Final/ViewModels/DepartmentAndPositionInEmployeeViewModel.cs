using Proyecto_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Final.ViewModels
{
    public class DepartmentAndPositionInEmployeeViewModel
    {
        public Employee EmployeeObj { get; set; }
        public List<Department> DepartmentObj { get; set; }
        public List<Position> PositionObj { get; set; }
    }
}
