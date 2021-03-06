using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SARH___JMéndez_Constructora.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SARH___JMéndez_Constructora.Data
{
    public class IdentityDbContext : IdentityDbContext <ApplicationUser>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }
    }
}
