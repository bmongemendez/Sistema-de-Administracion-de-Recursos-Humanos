using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SARH___JMéndez_Constructora.Models
{
    public partial class Pagosregistroauditoria
    {
        [Display(Name = "Reg No")]
        public int Id { get; set; }
        [Display(Name = "Pago")]
        public int IdPago { get; set; }
        [Display(Name = "Dato")]
        [StringLength(255)]
        public string Columna { get; set; }
        [Display(Name = "Hecho el")]
        public DateTime FechaHoraTransaccion { get; set; }
        [Display(Name = "Valor Anterior")]
        [StringLength(255)]
        public string ValorAnterior { get; set; }
        [Display(Name = "Valor Nuevo")]
        [StringLength(255)]
        public string ValorNuevo { get; set; }
        [Display(Name = "Hecho por")]
        [StringLength(256)]
        public string UserName { get; set; }
    }
}
