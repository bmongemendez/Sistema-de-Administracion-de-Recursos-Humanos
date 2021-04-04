using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SARH___JMéndez_Constructora.Models.AusenciasInjustificadasViewModels
{
    public class AIFormViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Seleccione un empleado**")]
        [Display(Name = "Empleado**")]
        public int IdEmpleado { get; set; }
        public int IdTiempo { get; set; }
        [StringLength(128)]
        public string Notas { get; set; }
        [Required(ErrorMessage = "Seleccione una fecha de inicio**")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Fecha de Inicio**")]
        public DateTime FechaInicio { get; set; }
        [Required(ErrorMessage = "Seleccione una fecha de fin**")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Fecha de Fin**")]
        public DateTime FechaFin { get; set; }
        public int IdContrato { get; set; }

        public bool? EsInjustificado { get; set; }
    }
}
