using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SARH___JMéndez_Constructora.Models
{
    [Table("ausenciasinjustificadas")]
    public partial class Ausenciasinjustificadas
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("idEmpleado")]
        [Required(ErrorMessage = "Seleccione un empleado**")]
        [Display(Name = "Empleado**")]
        public int IdEmpleado { get; set; }
        [Column("idTiempo")]
        public int IdTiempo { get; set; }
        [Column("notas")]
        [StringLength(128)]
        public string Notas { get; set; }

        [ForeignKey(nameof(IdEmpleado))]
        [InverseProperty(nameof(Empleados.Ausenciasinjustificadas))]
        [Display(Name = "Empleado")]
        public virtual Empleados IdEmpleadoNavigation { get; set; }
        [ForeignKey(nameof(IdTiempo))]
        [InverseProperty(nameof(Tiempo.Ausenciasinjustificadas))]
        public virtual Tiempo IdTiempoNavigation { get; set; }
    }
}
