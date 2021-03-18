using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using SARH___JMéndez_Constructora.Models;

namespace SARH___JMéndez_Constructora.Models.TrabajadoresViewModels
{
    public class ContractFormViewModel
    {
        public int IdEmpleado { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Fecha de Inicio**")]
        public DateTime Inicio { get; set; }
        [Required]
        [Display(Name = "Puesto (Código de Trabajo)**")]
        public string IdPuesto { get; set; }
        [Required]
        [Column("salarioDefinidoDia", TypeName = "decimal(10,0)")]
        [Display(Name = "Salario Definido (dia)**")]
        public decimal SalarioDefinidoDia { get; set; }
        [StringLength(70)]
        [Display(Name = "Cargo Especifico")]
        public string CargoEspecifico { get; set; }
    }
}