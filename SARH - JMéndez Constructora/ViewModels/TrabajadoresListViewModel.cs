using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SARH___JMéndez_Constructora.Models;

namespace SARH___JMéndez_Constructora.ViewModels
{
    public class TrabajadoresListViewModel
    {
        public IEnumerable<Empleados> empleadosActivos { get; set; }
        public IEnumerable<Empleados> empleadosInactivos { get; set; }
    }
}
