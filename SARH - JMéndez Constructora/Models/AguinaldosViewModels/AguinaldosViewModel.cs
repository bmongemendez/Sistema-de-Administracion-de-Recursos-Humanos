using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SARH___JMéndez_Constructora.Models.AguinaldosViewModels
{
    public class AguinaldosViewModel
    {
        public IEnumerable<AguinaldosListViewModel> AguinaldosList { get; set; }
        public Aguinaldos AguinaldoForm { get; set; }
        public IEnumerable<SelectListItem> EmpleadosList { get; set; }
    }
}
