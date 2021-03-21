using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SARH___JMéndez_Constructora.Models2
{
    [Table("empleadosregistroauditoria")]
    public partial class Empleadosregistroauditoria
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("idEmpleado")]
        public int IdEmpleado { get; set; }
        [Required]
        [Column("columna")]
        [StringLength(255)]
        public string Columna { get; set; }
        [Column("valorAnterior")]
        [StringLength(255)]
        public string ValorAnterior { get; set; }
        [Column("valorNuevo")]
        [StringLength(255)]
        public string ValorNuevo { get; set; }
        [Required]
        [Column("userName")]
        [StringLength(256)]
        public string UserName { get; set; }
    }
}
