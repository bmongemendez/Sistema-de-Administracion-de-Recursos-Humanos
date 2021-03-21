using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SARH___JMéndez_Constructora.Models.PagosViewModels
{
    public class PagosList
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Seleccione un empleado")]
        [Display(Name = "Empleado")]
        public int IdEmpleado { get; set; }
        public int IdContrato { get; set; }
        [Display(Name = "Cedula")]
        public int Cedula { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Primer Apellido")]
        public string Apellido1 { get; set; }
        [StringLength(50)]
        [Display(Name = "Segundo Apellido")]
        public string Apellido2 { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMMM-yyyy}")]
        [Display(Name = "Desde: ")]
        public DateTime Inicio { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMMM-yyyy}")]
        [Display(Name = "Hasta: ")]
        public DateTime Fin { get; set; }
        [Required]
        [Display (Name = "Horas Salario Normal")]
        public decimal HorasNormal { get; set; }
        [Display(Name = "Horas Extra")]
        public decimal? HorasExtra { get; set; }
        [Display(Name = "Dia Descanso")]
        public decimal? DiaDescanso { get; set; }
        [Display(Name = "Salario Bruto")]
        public decimal SalarioBruto { get; set; }
        [Display(Name = "Salario Neto")]
        [Column("salarioNeto", TypeName = "decimal(13,3)")]
        public decimal SalarioNeto { get; set; }
        [Display(Name = "Total Deducciones (Patrono)")]
        [Column("patronoLPT", TypeName = "decimal(13,3)")]
        public decimal TotalDeduccionesPatrono { get; set; }
        [Display(Name = "Observaciones")]
        [StringLength(128)]
        public string Observaciones { get; set; }
    }
}