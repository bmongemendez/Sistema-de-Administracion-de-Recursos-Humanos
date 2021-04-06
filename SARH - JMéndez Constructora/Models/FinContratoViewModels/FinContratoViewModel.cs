using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SARH___JMéndez_Constructora.Models.FinContratoViewModels
{
    public class FinContratoViewModel
    {
        public FinContratoFormViewModel FinContratoForm { get; set; }
        public IEnumerable<SelectListItem> MotivoSalida { get; set; }
        public IEnumerable<SelectListItem> PreavisoEjercido { get; set; }
    }
}
