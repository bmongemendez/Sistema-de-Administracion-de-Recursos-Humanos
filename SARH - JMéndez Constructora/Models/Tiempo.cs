<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SARH___JMéndez_Constructora.Models
{
    [Table("tiempo")]
    public partial class Tiempo
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("idEmpleado")]
        public int IdEmpleado { get; set; }
        [Column("fechaInicio", TypeName = "date")]
        public DateTime FechaInicio { get; set; }
        [Column("fechaFin", TypeName = "date")]
        public DateTime FechaFin { get; set; }
        [Column("idContrato")]
        public int IdContrato { get; set; }
        [Column("esLaborado")]
        public bool? EsLaborado { get; set; }
        [Column("esInjustificado")]
        public bool? EsInjustificado { get; set; }
        [Column("esVacaciones")]
        public bool? EsVacaciones { get; set; }
        [Column("esIncapacidad")]
        public bool? EsIncapacidad { get; set; }

        [ForeignKey(nameof(IdContrato))]
        [InverseProperty(nameof(Ingresocontrato.Tiempo))]
        public virtual Ingresocontrato IdContratoNavigation { get; set; }
        [ForeignKey(nameof(IdEmpleado))]
        [InverseProperty(nameof(Empleados.Tiempo))]
        public virtual Empleados IdEmpleadoNavigation { get; set; }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SARH___JMéndez_Constructora.Models
{
    [Table("tiempo")]
    public partial class Tiempo
    {
        public Tiempo()
        {
            Pagos = new HashSet<Pagos>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("idEmpleado")]
        public int IdEmpleado { get; set; }
        [Column("fechaInicio", TypeName = "date")]
        public DateTime FechaInicio { get; set; }
        [Column("fechaFin", TypeName = "date")]
        public DateTime FechaFin { get; set; }
        [Column("idContrato")]
        public int IdContrato { get; set; }
        [Column("esLaborado")]
        public bool? EsLaborado { get; set; }
        [Column("esInjustificado")]
        public bool? EsInjustificado { get; set; }
        [Column("esVacaciones")]
        public bool? EsVacaciones { get; set; }
        [Column("esIncapacidad")]
        public bool? EsIncapacidad { get; set; }

        [ForeignKey(nameof(IdContrato))]
        [InverseProperty(nameof(Ingresocontrato.Tiempo))]
        public virtual Ingresocontrato IdContratoNavigation { get; set; }
        [ForeignKey(nameof(IdEmpleado))]
        [InverseProperty(nameof(Empleados.Tiempo))]
        public virtual Empleados IdEmpleadoNavigation { get; set; }
        [InverseProperty("IdTiempoNavigation")]
        public virtual ICollection<Pagos> Pagos { get; set; }
    }
}
>>>>>>> main
