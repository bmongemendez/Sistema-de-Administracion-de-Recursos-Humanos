using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SARH___JMéndez_Constructora.Models
{
    [Table("evidencias")]
    public partial class Evidencias
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("idIncapacidad")]
        public int IdIncapacidad { get; set; }
        [Required]
        [Column("evidencia", TypeName = "mediumblob")]
        public byte[] Evidencia { get; set; }
        public string Tipo { get; set; }

        [ForeignKey(nameof(IdIncapacidad))]
        [InverseProperty(nameof(Incapacidades.Evidencias))]
        public virtual Incapacidades IdIncapacidadNavigation { get; set; }
    }
}
