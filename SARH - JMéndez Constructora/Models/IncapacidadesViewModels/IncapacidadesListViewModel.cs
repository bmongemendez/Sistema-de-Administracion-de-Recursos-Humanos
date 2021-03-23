using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SARH___JMéndez_Constructora.Models.IncapacidadesViewModels
{
    public class IncapacidadesListViewModel
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
        public IEnumerable<Evidencias> Evidencias { get; set; }
    }
}