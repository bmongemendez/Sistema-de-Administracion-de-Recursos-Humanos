using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SARH___JMéndez_Constructora.Models2
{
    [Table("evaluaciones")]
    public partial class Evaluaciones
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("idEmpleado")]
        public int IdEmpleado { get; set; }
        [Column("calificacion")]
        public int Calificacion { get; set; }
        [Column("observaciones")]
        [StringLength(128)]
        public string Observaciones { get; set; }

        [ForeignKey(nameof(IdEmpleado))]
        [InverseProperty(nameof(Empleados.Evaluaciones))]
        public virtual Empleados IdEmpleadoNavigation { get; set; }
    }
}
