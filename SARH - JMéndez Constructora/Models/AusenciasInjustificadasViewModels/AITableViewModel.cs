using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SARH___JMéndez_Constructora.Models.AusenciasInjustificadasViewModels
{
    public class AITableViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Cédula")]
        public int Cedula { get; set; }
        [Display(Name = "Empleado")]
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
       
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMMM-yyyy}")]
        [Display(Name = "Desde")]
        public DateTime Desde { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMMM-yyyy}")]
        [Display(Name = "Hasta")]
        public DateTime Hasta { get; set; }
        public string Notas { get; set; }
        public int IdTiempo { get; set; }
    }
}
