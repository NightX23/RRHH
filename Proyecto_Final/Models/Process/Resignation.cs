using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Final.Models.Process
{
    public class Resignation
    {
        public int Id { get; set; }
        //FK EMPLOYEES-------------------------------------------------------------
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
        //--------------------------------------------------------------------------
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Tipo")]
        public string ResignationType { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [MaxLength(50)]
        [Display(Name = "Movito")]
        public string Reason { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        public DateTime ResignationDate { get; set; }
    }
}
