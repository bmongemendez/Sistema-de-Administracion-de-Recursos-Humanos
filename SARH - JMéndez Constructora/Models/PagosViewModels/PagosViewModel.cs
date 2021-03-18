using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SARH___JMéndez_Constructora.Models.PagosViewModels
{
    public class PagosViewModel
    {
        public IngresoPagoFormViewModel ingresoPago { get; set; }
        // PARA ENLISTAR EMPLEADOS EN FORMULARIO
        public IEnumerable<SelectListItem> empleados { get; set; }
        // PARA ENLISTAR TODOS LOS PAGOS GENERADOS
        public IEnumerable<PagosList> pagos { get; set; }
    }
}
