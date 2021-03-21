using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using SARH___JMéndez_Constructora.Models;

namespace SARH___JMéndez_Constructora.Models.TrabajadoresViewModels
{
    public class IngresoTrabajadorFormViewModel
    {
        // EMPLEADO
        [Display(Name = "Numero de Cedula**")]
        public int Cedula { get; set; }
        [Required(ErrorMessage = "El campo 'Nombre' es requerido")]
        [StringLength(50)]
        [Display(Name = "Nombre**")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo 'Primer Apellido' es requerido")]
        [StringLength(50)]
        [Display(Name = "Primer Apellido**")]
        public string Apellido1 { get; set; }
        [StringLength(50)]
        [Display(Name = "Segundo Apellido")]
        public string Apellido2 { get; set; }
        [Column("fechaNacimiento", TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Fecha de Nacimiento**")]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "El campo 'Telefono' es requerido")]
        [StringLength(50)]
        [Display(Name = "Telefono**")]
        public string Telefono { get; set; }
        [StringLength(50)]
        [Display(Name = "Telefono Emergencia")]
        public string TelefonoEmergencia { get; set; }
        [StringLength(50)]
        [Display(Name = "Contacto de Emergencia")]
        public string ContactoEmergencia { get; set; }
        [Display(Name = "Grado Bachiller")]
        public bool TieneBachiller { get; set; }
        [Display(Name = "Grado Licenciatura")]
        public bool TieneLicenciatura { get; set; }
        [Display(Name = "Grado Tecnico")]
        public bool TieneTecnico { get; set; }
        [Display(Name = "Licencia A3")]
        public bool TieneLicenciaA3 { get; set; }
        [Display(Name = "Licencia B1")]
        public bool TieneLicenciaB1 { get; set; }
        [Display(Name ="Licencia B2")]
        public bool TieneLicenciaB2 { get; set; }
        [Display(Name ="Licencia B3")]
        public bool TieneLicenciaB3 { get; set; }
        [Display(Name ="Licencia D")]
        public bool TieneLicenciaD { get; set; }
        [Display(Name ="Licencia E")]
        public bool TieneLicenciaE { get; set; }
        // CONTRATO
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
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
