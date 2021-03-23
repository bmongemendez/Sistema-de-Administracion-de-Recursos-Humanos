using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SARH___JMéndez_Constructora.Models
{
    [Table("tiempo")]
    public partial class Tiempo
    {
        public Tiempo()
        {
            Pagos = new HashSet<Pagos>();
            Vacaciones = new HashSet<Vacaciones>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("idEmpleado")]

        public int IdEmpleado { get; set; }
        [Column("fechaInicio", TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Fecha de Inicio")]

        public DateTime FechaInicio { get; set; }
        [Column("fechaFin", TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Fecha de Fin")]

        public DateTime FechaFin { get; set; }
        [Column("idContrato")]

        public int IdContrato { get; set; }
        [Column("esLaborado")]
        [Display(Name = "Laborado")]

        public bool? EsLaborado { get; set; }
        [Column("esInjustificado")]
        [Display(Name = "Injustificado")]

        public bool? EsInjustificado { get; set; }
        [Column("esVacaciones")]
        [Display(Name = "Vacaciones")]

        public bool? EsVacaciones { get; set; }
        [Column("esIncapacidad")]
        [Display(Name = "Incapacidad")]

        public bool? EsIncapacidad { get; set; }

        [ForeignKey(nameof(IdContrato))]
        [InverseProperty(nameof(Ingresocontrato.Tiempo))]
        [Display(Name = "# de Contrato")]
        public virtual Ingresocontrato IdContratoNavigation { get; set; }
        [ForeignKey(nameof(IdEmpleado))]
        [InverseProperty(nameof(Empleados.Tiempo))]
        [Display(Name = "Empleado")]
        public virtual Empleados IdEmpleadoNavigation { get; set; }
        [InverseProperty("IdTiempoNavigation")]
        public virtual ICollection<Pagos> Pagos { get; set; }
        [InverseProperty("IdTiempoNavigation")]
        public virtual ICollection<Vacaciones> Vacaciones { get; set; }
    }
}
