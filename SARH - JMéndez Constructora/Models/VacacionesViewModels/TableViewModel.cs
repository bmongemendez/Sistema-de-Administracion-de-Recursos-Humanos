using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SARH___JMéndez_Constructora.Models.VacacionesViewModels
{
    public class TableViewModel
    {
        public int Cedula { get; set; }
        [Display(Name = "Empleado")]
        public string Nombre { get; set; }
        
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        [Display(Name = "Tipo")]
        public int Tipo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMMM-yyyy}")]
        [Display(Name = "Desde")]
        public DateTime Desde { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMMM-yyyy}")]
        [Display(Name = "Hasta")]
        public DateTime Hasta { get; set; }
        public string Observaciones { get; set; }
        [Display(Name = "Aprobadas")]
        public bool FueronAprobadas { get; set; }
    }
}
