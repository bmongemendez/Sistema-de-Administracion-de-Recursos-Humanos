using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SARH___JMéndez_Constructora.Models2
{
    [Table("empleados")]
    public partial class Empleados
    {
        public Empleados()
        {
            Aguinaldos = new HashSet<Aguinaldos>();
            Evaluaciones = new HashSet<Evaluaciones>();
            Ingresocontrato = new HashSet<Ingresocontrato>();
            Pagos = new HashSet<Pagos>();
            Tiempo = new HashSet<Tiempo>();
            Vacaciones = new HashSet<Vacaciones>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("cedula")]
        public int Cedula { get; set; }
        [Required]
        [Column("nombre")]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [Column("apellido1")]
        [StringLength(50)]
        public string Apellido1 { get; set; }
        [Column("apellido2")]
        [StringLength(50)]
        public string Apellido2 { get; set; }
        [Column("fechaNacimiento", TypeName = "date")]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        [Column("telefono")]
        [StringLength(50)]
        public string Telefono { get; set; }
        [Column("telefonoEmergencia")]
        [StringLength(50)]
        public string TelefonoEmergencia { get; set; }
        [Column("contactoEmergencia")]
        [StringLength(50)]
        public string ContactoEmergencia { get; set; }
        [Column("tieneBachiller")]
        public bool? TieneBachiller { get; set; }
        [Column("tieneLicenciatura")]
        public bool? TieneLicenciatura { get; set; }
        [Column("tieneTecnico")]
        public bool? TieneTecnico { get; set; }
        [Column("tieneLicenciaA3")]
        public bool? TieneLicenciaA3 { get; set; }
        [Column("tieneLicenciaB1")]
        public bool? TieneLicenciaB1 { get; set; }
        [Column("tieneLicenciaB2")]
        public bool? TieneLicenciaB2 { get; set; }
        [Column("tieneLicenciaB3")]
        public bool? TieneLicenciaB3 { get; set; }
        [Column("tieneLicenciaD")]
        public bool? TieneLicenciaD { get; set; }
        [Column("tieneLicenciaE")]
        public bool? TieneLicenciaE { get; set; }
        [Column("seElimino")]
        public bool SeElimino { get; set; }
        [Required]
        [Column("userName")]
        [StringLength(256)]
        public string UserName { get; set; }

        [ForeignKey(nameof(UserName))]
        [InverseProperty(nameof(Aspnetusersref.Empleados))]
        public virtual Aspnetusersref UserNameNavigation { get; set; }
        [InverseProperty("IdEmpleadoNavigation")]
        public virtual ICollection<Aguinaldos> Aguinaldos { get; set; }
        [InverseProperty("IdEmpleadoNavigation")]
        public virtual ICollection<Evaluaciones> Evaluaciones { get; set; }
        [InverseProperty("IdEmpleadoNavigation")]
        public virtual ICollection<Ingresocontrato> Ingresocontrato { get; set; }
        [InverseProperty("IdEmpleadoNavigation")]
        public virtual ICollection<Pagos> Pagos { get; set; }
        [InverseProperty("IdEmpleadoNavigation")]
        public virtual ICollection<Tiempo> Tiempo { get; set; }
        [InverseProperty("IdEmpleadoNavigation")]
        public virtual ICollection<Vacaciones> Vacaciones { get; set; }
    }
}
