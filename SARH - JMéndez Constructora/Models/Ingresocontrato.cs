using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SARH___JMéndez_Constructora.Models
{
    [Table("ingresocontrato")]
    public partial class Ingresocontrato
    {
        public Ingresocontrato() { }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("idEmpleado")]
        public int IdEmpleado { get; set; }
        [Required]
        [Column("inicio", TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMMM-yyyy}")]
        [Display(Name = "Fecha de Inicio")]
        public DateTime Inicio { get; set; }
        [Required]
        [Column("idPuesto")]
        public int IdPuesto { get; set; }
        [Required]
        [Column("salarioDefinidoDia", TypeName = "decimal(10,0)")]
        [Display(Name = "Salario Definido (dia)")]
        public decimal SalarioDefinidoDia { get; set; }
        [Column("cargoEspecifico")]
        [StringLength(70)]
        [Display(Name = "Cargo Especifico")]
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
        public virtual ICollection<Pagos> Pagos { get; set; }

        [InverseProperty("IdContratoNavigation")]
        public virtual ICollection<Tiempo> Tiempo { get; set; }
    }
}
