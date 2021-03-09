using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SARH___JMéndez_Constructora.Models
{
    [Table("puestos")]
    public partial class Puestos
    {
        public Puestos()
        {
            Ingresocontrato = new HashSet<Ingresocontrato>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("siglas")]
        [StringLength(10)]
        public string Siglas { get; set; }
        [Required]
        [Column("nombre")]
        [StringLength(128)]
        public string Nombre { get; set; }
        [Column("salarioMes", TypeName = "decimal(13,3)")]
        public decimal? SalarioMes { get; set; }
        [Column("salarioDia", TypeName = "decimal(9,3)")]
        public decimal? SalarioDia { get; set; }
        [Column("salarioHora", TypeName = "decimal(9,3)")]
        public decimal? SalarioHora { get; set; }
        [Column("salarioMesJm", TypeName = "decimal(13,3)")]
        public decimal? SalarioMesJm { get; set; }
        [Column("salarioDiaJm", TypeName = "decimal(9,3)")]
        public decimal? SalarioDiaJm { get; set; }
        [Column("salarioHoraJm", TypeName = "decimal(9,3)")]
        public decimal? SalarioHoraJm { get; set; }

        [InverseProperty("IdPuestoNavigation")]
        public virtual ICollection<Ingresocontrato> Ingresocontrato { get; set; }
    }
}
