using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SARH___JMéndez_Constructora.ViewModels
{
    public class BasicFormViewModel
    {
        public int IdEmpleado { get; set; }
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
        [Column("fechaNacimiento", TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Fecha de Nacimiento**")]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "El campo 'Telefono' es requerido")]
        [StringLength(50)]
        [Display(Name = "Telefono**")]
        public string Telefono { get; set; }
        [StringLength(50)]
        [Display(Name = "Telefono Emergencia")]
        public string TelefonoEmergencia { get; set; }
        [StringLength(50)]
        [Display(Name = "Contacto de Emergencia")]
        public string ContactoEmergencia { get; set; }
    }
}