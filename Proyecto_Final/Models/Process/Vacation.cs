using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Final.Models.Process
{
    public class Vacation
    {

        public int Id { get; set; }
        //FK EMPLOYEES ----------------------------------------------------------------------------
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
        //-----------------------------------------------------------------------------------------
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Desde")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hasta")]
        public DateTime EndDate { get; set; }

        [MaxLength(30)]
        [Display(Name ="Comentario")]
        public string Comment { get; set; }
    }
}
