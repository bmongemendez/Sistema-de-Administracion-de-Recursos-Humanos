using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SARH___JMéndez_Constructora.Models2
{
    [Table("fincontrato")]
    public partial class Fincontrato
    {
        [Key]
        [Column("idInicioContrato")]
        public int IdInicioContrato { get; set; }
        [Column("fechaFin", TypeName = "date")]
        public DateTime FechaFin { get; set; }
        [Column("preavisoEjercido")]
        public int? PreavisoEjercido { get; set; }
        [Column("diasPendientesPreaviso")]
        public int? DiasPendientesPreaviso { get; set; }
        [Column("motivoSalida")]
        public int MotivoSalida { get; set; }
        [Column("saldoVacaciones")]
        public int SaldoVacaciones { get; set; }
        [Column("aguinaldo", TypeName = "decimal(13,3)")]
        public decimal? Aguinaldo { get; set; }
        [Column("cesantia", TypeName = "decimal(13,3)")]
        public decimal? Cesantia { get; set; }
        [Column("vacaciones", TypeName = "decimal(13,3)")]
        public decimal? Vacaciones { get; set; }
        [Column("preaviso", TypeName = "decimal(13,3)")]
        public decimal? Preaviso { get; set; }

        [ForeignKey(nameof(IdInicioContrato))]
        [InverseProperty(nameof(Ingresocontrato.Fincontrato))]
        public virtual Ingresocontrato IdInicioContratoNavigation { get; set; }
    }
}
