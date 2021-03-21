using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SARH___JMéndez_Constructora.Models
{
    [Table("deducciones")]
    public partial class Deducciones
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("grupo")]
        [StringLength(3)]
        public string Grupo { get; set; }
        [Required]
        [Column("concepto")]
        [StringLength(128)]
        public string Concepto { get; set; }
        [Column("patrono", TypeName = "decimal(5,3)")]
        public decimal? Patrono { get; set; }
        [Column("trabajador", TypeName = "decimal(5,3)")]
        public decimal? Trabajador { get; set; }
    }
}
