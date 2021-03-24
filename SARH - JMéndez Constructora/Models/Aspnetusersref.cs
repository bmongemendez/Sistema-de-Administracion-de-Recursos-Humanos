using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SARH___JMéndez_Constructora.Models
{
    [Table("aspnetusersref")]
    public partial class Aspnetusersref
    {
        [Key]
        [Column("userName")]
        [StringLength(256)]
        public string UserName { get; set; }
        [Column("lightVersionEnabled")]
        public bool LightVersionEnabled { get; set; }
        [Column("rightToLeftEnabled")]
        public bool RightToLeftEnabled { get; set; }
    }
}
