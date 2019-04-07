using Proyecto_Final.Models;
using Proyecto_Final.Models.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Final.ViewModels
{
    public class VacationAndEmployeeViewModel
    {
        public Vacation VacationObj { get; set; }
       // public Employee EmployeeObj { get; set; }
        public List<Employee> EmployeeList { get; set; }
    }
}
