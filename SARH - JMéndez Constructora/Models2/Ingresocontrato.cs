using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SARH___JMéndez_Constructora.Models2
{
    [Table("ingresocontrato")]
    public partial class Ingresocontrato
    {
        public Ingresocontrato()
        {
            Aguinaldos = new HashSet<Aguinaldos>();
            Pagos = new HashSet<Pagos>();
            Tiempo = new HashSet<Tiempo>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("idEmpleado")]
        public int IdEmpleado { get; set; }
        [Column("inicio", TypeName = "date")]
        public DateTime Inicio { get; set; }
        [Column("idPuesto")]
        public int IdPuesto { get; set; }
        [Column("salarioDefinidoDia", TypeName = "decimal(10,0)")]
        public decimal SalarioDefinidoDia { get; set; }
        [Column("cargoEspecifico")]
        [StringLength(70)]
        public string CargoEspecifico { get; set; }

        [ForeignKey(nameof(IdEmpleado))]
        [InverseProperty(nameof(Empleados.Ingresocontrato))]
        public virtual Empleados IdEmpleadoNavigation { get; set; }
        [ForeignKey(nameof(IdPuesto))]
        [InverseProperty(nameof(Puestos.Ingresocontrato))]
        public virtual Puestos IdPuestoNavigation { get; set; }
        [InverseProperty("IdInicioContratoNavigation")]
        public virtual Fincontrato Fincontrato { get; set; }
        [InverseProperty("IdContratoNavigation")]
        public virtual ICollection<Aguinaldos> Aguinaldos { get; set; }
        [InverseProperty("IdContratoNavigation")]
        public virtual ICollection<Pagos> Pagos { get; set; }
        [InverseProperty("IdContratoNavigation")]
        public virtual ICollection<Tiempo> Tiempo { get; set; }
    }
}
