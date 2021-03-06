using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SARH___JMéndez_Constructora.Models;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SARH___JMéndez_Constructora.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public virtual DbSet<Empleados> Empleados { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleados>(entity =>
            {
                entity.HasIndex(e => e.Cedula)
                    .HasName("cedula_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserName)
                    .HasName("idUser_Empleados_idx");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
