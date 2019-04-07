using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Final.Models
{
    public class Position
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        
        [Display(Name = "Cargo")]
        [StringLength(40, ErrorMessage = "El campo ingresado es demasiado largo.")]
        public string PositionName { get; set; }
    }
}
