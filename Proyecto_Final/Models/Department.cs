using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Final.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Nombre de departamento")]
        [StringLength(25, ErrorMessage = "El campo ingresado es demasiado largo.")]
        public string DepartmentName { get; set; }
    }
}
