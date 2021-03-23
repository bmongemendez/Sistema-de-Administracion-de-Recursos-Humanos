using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SARH___JMéndez_Constructora.Models
{
    [Table("incapacidades")]
    public partial class Incapacidades
    {
        public Incapacidades()
        {
            Evidencias = new HashSet<Evidencias>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("tipo")]
        public int Tipo { get; set; }
        [Column("idTiempo")]
        public int IdTiempo { get; set; }

        [ForeignKey(nameof(IdTiempo))]
        [InverseProperty(nameof(Tiempo.Incapacidades))]
        public virtual Tiempo IdTiempoNavigation { get; set; }
        [InverseProperty("IdIncapacidadNavigation")]
        public virtual ICollection<Evidencias> Evidencias { get; set; }
    }
}
