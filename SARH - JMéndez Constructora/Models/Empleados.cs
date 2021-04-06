using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SARH___JMéndez_Constructora.Models
{
    [Table("empleados")]
    public partial class Empleados
    {
        public Empleados() 
        {
            Aguinaldos = new HashSet<Aguinaldos>();
            Ingresocontrato = new HashSet<Ingresocontrato>();
            Ausenciasinjustificadas = new HashSet<Ausenciasinjustificadas>();
        }
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("cedula")]
        [Display(Name = "Cedula")]
        public int Cedula { get; set; }
        [Required]
        [Column("nombre")]
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Required]
        [Column("apellido1")]
        [StringLength(50)]
        [Display(Name = "Primer Apellido")]
        public string Apellido1 { get; set; }
        [Column("apellido2")]
        [StringLength(50)]
        [Display(Name = "Segundo Apellido")]
        public string Apellido2 { get; set; }
        [Column("fechaNacimiento", TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "El campo 'Telefono' es requerido")]
        [Column("telefono")]
        [StringLength(50)]
        [Display(Name = "Telefono")]
        public string Telefono { get; set; }
        [Column("telefonoEmergencia")]
        [StringLength(50)]
        [Display(Name = "Telefono Emergencia")]
        public string TelefonoEmergencia { get; set; }
        [Column("contactoEmergencia")]
        [StringLength(50)]
        [Display(Name = "Contacto de Emergencia")]
        public string ContactoEmergencia { get; set; }
        [Column("tieneBachiller")]
        [Display(Name = "Grado Bachiller")]
        public bool? TieneBachiller { get; set; }
        [Column("tieneLicenciatura")]
        [Display(Name = "Grado Licenciatura")]
        public bool? TieneLicenciatura { get; set; }
        [Column("tieneTecnico")]
        [Display(Name = "Grado Tecnico")]
        public bool? TieneTecnico { get; set; }
        [Column("tieneLicenciaA3")]
        [Display(Name = "Licencia A3")]
        public bool? TieneLicenciaA3 { get; set; }
        [Column("tieneLicenciaB1")]
        [Display(Name = "Licencia B1")]
        public bool? TieneLicenciaB1 { get; set; }
        [Column("tieneLicenciaB2")]
        [Display(Name ="Licencia B2")]
        public bool? TieneLicenciaB2 { get; set; }
        [Column("tieneLicenciaB3")]
        [Display(Name ="Licencia B3")]
        public bool? TieneLicenciaB3 { get; set; }
        [Column("tieneLicenciaD")]
        [Display(Name ="Licencia D")]
        public bool? TieneLicenciaD { get; set; }
        [Column("tieneLicenciaE")]
        [Display(Name ="Licencia E")]
        public bool? TieneLicenciaE { get; set; }
        [Column("seElimino")]
        public bool SeElimino { get; set; }
        [Required]
        [Column("userName")]
        [StringLength(256)]
        public string UserName { get; set; }
        
        [InverseProperty("IdEmpleadoNavigation")]
        public virtual ICollection<Aguinaldos> Aguinaldos { get; set; }
        [InverseProperty("IdEmpleadoNavigation")]
        public virtual ICollection<Ingresocontrato> Ingresocontrato { get; set; }
        [InverseProperty("IdEmpleadoNavigation")]
        public virtual ICollection<Vacaciones> Vacaciones { get; set; }
        [InverseProperty("IdEmpleadoNavigation")]
        public virtual ICollection<Tiempo> Tiempo { get; set; }
        [InverseProperty("IdEmpleadoNavigation")]
        public virtual ICollection<Evaluaciones> Evaluaciones { get; set; }

        [InverseProperty("IdEmpleadoNavigation")]
        public virtual ICollection<Pagos> Pagos { get; set; }
        [InverseProperty("IdEmpleadoNavigation")]
        public virtual ICollection<Ausenciasinjustificadas> Ausenciasinjustificadas { get; set; }
    }
}
