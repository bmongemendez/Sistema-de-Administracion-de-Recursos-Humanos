using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SARH___JMéndez_Constructora.ViewModels
{
    public class TrabajadorViewModel
    {
        public IngresoTrabajadorFormViewModel ingresoTrabajador { get; set; }
        public TrabajadoresListViewModel trabajadoresList { get; set; }
        // PARA ENLISTAR PUESTOS EN FORMULARIO
        public IEnumerable<SelectListItem> puestos { get; set; }
    }
}
