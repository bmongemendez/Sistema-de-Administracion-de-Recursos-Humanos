using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SARH___JMéndez_Constructora.Models.IncapacidadesViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Empleadosregistroauditoria> BitacoraEmpleados { get; set; }
        public IEnumerable<Pagosregistroauditoria> BitacoraPagos { get; set; }
    }
}