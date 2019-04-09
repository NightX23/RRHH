using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Models;
using Proyecto_Final.Models.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Final.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options)
            :base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<Resignation> Resignations { get; set; }
        public DbSet<Paysheet> Paysheets { get; set; }
    }
}
