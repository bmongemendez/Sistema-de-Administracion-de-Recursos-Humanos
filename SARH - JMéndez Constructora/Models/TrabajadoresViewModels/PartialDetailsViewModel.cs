using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SARH___JMéndez_Constructora.Models.TrabajadoresViewModels
{
    public class PartialDetailsViewModel
    {
        public int Id { get; set; }
        // EMPLEADO
        [Display(Name = "Numero de Cedula**")]
        public int Cedula { get; set; }
        [Required(ErrorMessage = "El campo 'Nombre' es requerido")]
        [StringLength(50)]
        [Display(Name = "Nombre**")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo 'Primer Apellido' es requerido")]
        [StringLength(50)]
        [Display(Name = "Primer Apellido**")]
        public string Apellido1 { get; set; }
        [StringLength(50)]
        [Display(Name = "Segundo Apellido")]
        public string Apellido2 { get; set; }
        [Required]
        [Display(Name = "Puesto (Código de Trabajo)**")]
        public string Puesto { get; set; }
    }
}
