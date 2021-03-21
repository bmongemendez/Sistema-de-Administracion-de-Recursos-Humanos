using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SARH___JMéndez_Constructora.Models
{
    [Table("vacaciones")]
    public partial class Vacaciones
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("idEmpleado")]
        public int IdEmpleado { get; set; }
        [Column("fueronAprobadas")]
        public bool FueronAprobadas { get; set; }
        [Column("notas")]
        [StringLength(128)]
        public string Notas { get; set; }

        [ForeignKey(nameof(IdEmpleado))]
        [InverseProperty(nameof(Empleados.Vacaciones))]
        public virtual Empleados IdEmpleadoNavigation { get; set; }
    }
}
