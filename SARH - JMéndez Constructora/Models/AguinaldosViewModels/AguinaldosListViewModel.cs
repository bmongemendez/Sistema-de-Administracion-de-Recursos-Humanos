using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SARH___JMéndez_Constructora.Models.AguinaldosViewModels
{
    public class AguinaldosListViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Empleado")]
        public int IdEmpleado { get; set; }
        public int IdContrato { get; set; }
        [Display(Name = "Cedula")]
        public int Cedula { get; set; }
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [StringLength(50)]
        [Display(Name = "Primer Apellido")]
        public string Apellido1 { get; set; }
        [StringLength(50)]
        [Display(Name = "Segundo Apellido")]
        public string Apellido2 { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Inicio Contrato")]
        public DateTime FechaInicio { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Fecha Pago")]
        public DateTime FechaFin { get; set; }
        public decimal SumatoriaSalarioBrutos { get; set; }
        [Display(Name = "Monto")]
        public decimal MontoAguinaldo { get; set; }
        public string Anotaciones { get; set; }
    }
}
