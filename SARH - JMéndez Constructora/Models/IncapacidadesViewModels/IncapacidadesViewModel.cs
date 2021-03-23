using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SARH___JMéndez_Constructora.Models.IncapacidadesViewModels
{
    public class IncapacidadesViewModel
    {
        public IncapacidadesFormViewModel Incapacidad { get; set; }
        public IEnumerable<SelectListItem> Empleados { get; set; }
        public IEnumerable<SelectListItem> Tipo { get; set; }
        public IEnumerable<IncapacidadesListViewModel> Incapacidades { get; set; }
    }
}