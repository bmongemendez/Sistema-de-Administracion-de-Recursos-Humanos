using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SARH___JMéndez_Constructora.Models
{
    public partial class Empleadosregistroauditoria
    {
        [Display(Name = "Reg No.")]
        public int Id { get; set; }
        [Display(Name = "Empleado")]
        public int IdEmpleado { get; set; }
        [StringLength(255)]
        [Display(Name = "Dato")]
        public string Columna { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Hecho el")]
        public DateTime FechaHoraTransaccion { get; set; }
        [StringLength(255)]
        [Display(Name = "Valor Anterior")]
        public string ValorAnterior { get; set; }
        [StringLength(255)]
        [Display(Name = "Valor Nuevo")]
        public string ValorNuevo { get; set; }
        [StringLength(256)]
        [Display(Name = "Hecho por")]
        public string UserName { get; set; }
    }
}
