using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SARH___JMéndez_Constructora.Models
{
    public class ApplicationUser : IdentityUser
    {
        [DefaultValue(false)]
        public bool isDeleted { get; set; }
        public string nombre { get; set; }
    }
}
