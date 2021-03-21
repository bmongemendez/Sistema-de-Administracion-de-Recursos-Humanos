using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SARH___JMéndez_Constructora.Models
{
    [Table("pagos")]
    public partial class Pagos
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("idEmpleado")]
        public int IdEmpleado { get; set; }
        [Column("idContrato")]
        public int IdContrato { get; set; }
        [Column("idTiempo")]
        public int IdTiempo { get; set; }
        [Column("horasNormal", TypeName = "decimal(7,3)")]
        public decimal HorasNormal { get; set; }
        [Column("horasExtra", TypeName = "decimal(7,3)")]
        public decimal? HorasExtra { get; set; }
        [Column("diaDescanso", TypeName = "decimal(10,3)")]
        public decimal? DiaDescanso { get; set; }
        [Column("salarioNormal", TypeName = "decimal(13,3)")]
        public decimal SalarioNormal { get; set; }
        [Column("salarioExtras", TypeName = "decimal(13,3)")]
        public decimal? SalarioExtras { get; set; }
        [Column("salarioDiaDescanso", TypeName = "decimal(13,3)")]
        public decimal? SalarioDiaDescanso { get; set; }
        [Column("salarioBruto", TypeName = "decimal(13,3)")]
        public decimal SalarioBruto { get; set; }
        [Column("deducciones", TypeName = "decimal(13,3)")]
        public decimal? Deducciones { get; set; }
        [Column("cuentasPorPagar", TypeName = "decimal(13,3)")]
        public decimal? CuentasPorPagar { get; set; }
        [Column("salarioNeto", TypeName = "decimal(13,3)")]
        public decimal SalarioNeto { get; set; }
        [Column("patronoCCSS", TypeName = "decimal(13,3)")]
        public decimal PatronoCcss { get; set; }
        [Column("patronoROtrasInstituciones", TypeName = "decimal(13,3)")]
        public decimal PatronoRotrasInstituciones { get; set; }
        [Column("patronoLPT", TypeName = "decimal(13,3)")]
        public decimal PatronoLpt { get; set; }
        [Column("observaciones")]
        [StringLength(128)]
        public string Observaciones { get; set; }
        [Column("seElimino")]
        public bool SeElimino { get; set; }
        [Required]
        [Column("userName")]
        [StringLength(256)]
        public string UserName { get; set; }

        [ForeignKey(nameof(IdContrato))]
        [InverseProperty(nameof(Ingresocontrato.Pagos))]
        public virtual Ingresocontrato IdContratoNavigation { get; set; }
        [ForeignKey(nameof(IdEmpleado))]
        [InverseProperty(nameof(Empleados.Pagos))]
        public virtual Empleados IdEmpleadoNavigation { get; set; }
        [ForeignKey(nameof(IdTiempo))]
        [InverseProperty(nameof(Tiempo.Pagos))]
        public virtual Tiempo IdTiempoNavigation { get; set; }
    }
}
