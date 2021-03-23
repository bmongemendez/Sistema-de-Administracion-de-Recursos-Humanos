using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SARH___JMéndez_Constructora.Models.IncapacidadesViewModels
{
    public class IncapacidadesFormViewModel
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Empleado**")]
        public int IdEmpleado { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Tipo de incapacidad**")]
        public int Tipo { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMMM-yyyy}")]
        [Display(Name = "Desde**")]
        public DateTime Desde { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMMM-yyyy}")]
        [Display(Name = "Hasta**")]
        public DateTime Hasta { get; set; }
        public List<IFormFile> Evidencias { get; set; }
    }
}