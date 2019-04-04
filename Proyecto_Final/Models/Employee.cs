using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Final.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(11, ErrorMessage = "El campo ingresado debe tener {0} dígitos.")]
        [Display(Name = "Cédula")]
        public string IdentificationId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "El nombre ingresado debe tener entre {2} y {1} dígitos.")]
        [Display(Name = "Primer nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "El nombre ingresado debe tener entre {2} y {1} dígitos.")]
        [Display(Name = "Primer apellido")]
        public string LastName { get; set; }

        [Phone]
        [Display(Name = "Teléfono")]
        public string Phone { get; set; }

        [EmailAddress]
        [StringLength(50, ErrorMessage = "El campo ingresado excede el máximo de caractéres permitidos, " +
            "de ser necesario comuníquese con el departamento de tecnología.")]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        //FK DEPARTMENT---------------------------------------------------------------
        [ForeignKey("Department")]
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Departamento")]
        public int Department { get; set; }
        //----------------------------------------------------------------------------

        //FK POSITION-----------------------------------------------------------------
        [ForeignKey("Position")]
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Cargo")]
        public int Position { get; set; }
        //----------------------------------------------------------------------------

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de inicio")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Range(12000, 100000, ErrorMessage = "La cantidad ingresada debe estar en rango {2} - {1}.")]
        [Display(Name = "Salario")]
        public double Salary { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 6)]
        [Display(Name = "Estado")]
        public string Status { get; set; }
    }
}
