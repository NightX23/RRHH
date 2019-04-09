using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Final.Models.Process
{
    public class Paysheet
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Año")]
        public int Year { get; set; }

        [Required]
        [Display(Name = "Mes")]
        public int Month { get; set; }

        [Required]
        [Display(Name = "Total")]
        public double TotalAmount { get; set; }
    }
}
