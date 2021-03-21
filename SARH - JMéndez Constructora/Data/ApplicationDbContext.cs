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
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Empleados> Empleados { get; set; }
        public virtual DbSet<Fincontrato> Fincontrato { get; set; }
        public virtual DbSet<Deducciones> Deducciones { get; set; }
        public virtual DbSet<Ingresocontrato> Ingresocontrato { get; set; }
        public virtual DbSet<Puestos> Puestos { get; set; }
        public virtual DbSet<Vacaciones> Vacaciones { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleados>(entity =>
            {
                entity.HasIndex(e => e.Cedula)
                    .HasName("cedula_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserName)
                    .HasName("idUser_Empleados_idx");
                
                entity.Property(e => e.TieneBachiller).HasDefaultValueSql("'0'");

                entity.Property(e => e.TieneLicenciaA3).HasDefaultValueSql("'0'");

                entity.Property(e => e.TieneLicenciaB1).HasDefaultValueSql("'0'");

                entity.Property(e => e.TieneLicenciaB2).HasDefaultValueSql("'0'");

                entity.Property(e => e.TieneLicenciaB3).HasDefaultValueSql("'0'");

                entity.Property(e => e.TieneLicenciaD).HasDefaultValueSql("'0'");

                entity.Property(e => e.TieneLicenciaE).HasDefaultValueSql("'0'");

                entity.Property(e => e.TieneLicenciatura).HasDefaultValueSql("'0'");

                entity.Property(e => e.TieneTecnico).HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Fincontrato>(entity =>
            {
                entity.HasKey(e => e.IdInicioContrato)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.IdInicioContrato)
                    .HasName("idInicioContrato_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.DiasPendientesPreaviso).HasDefaultValueSql("'0'");

                entity.Property(e => e.MotivoSalida).HasComment(@"3: despido.respnbld.patronal
                    2: despido.sin.respnbld.patronal
                    0: renuncia
                    1: renuncia.respnbld.patronal");

                entity.Property(e => e.PreavisoEjercido).HasDefaultValueSql("'0'");

                entity.HasOne(d => d.IdInicioContratoNavigation)
                    .WithOne(p => p.Fincontrato)
                    .HasForeignKey<Fincontrato>(d => d.IdInicioContrato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idInicioContratoFN");
            });


            modelBuilder.Entity<Deducciones>(entity =>
            {
                entity.Property(e => e.Grupo).IsFixedLength();

                entity.Property(e => e.Patrono).HasDefaultValueSql("'0.000'");

                entity.Property(e => e.Trabajador).HasDefaultValueSql("'0.000'");
            });

            modelBuilder.Entity<Vacaciones>(entity =>
            {
                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("idEmpleadoVacaciones_idx");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Vacaciones)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idEmpleadoVacaciones");
            });

            modelBuilder.Entity<Ingresocontrato>(entity =>
            {
                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("idEmpleadoIC_idx");

                entity.HasIndex(e => e.IdPuesto)
                    .HasName("idPuesto_idx");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Ingresocontrato)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idEmpleadoIC");

                entity.HasOne(d => d.IdPuestoNavigation)
                    .WithMany(p => p.Ingresocontrato)
                    .HasForeignKey(d => d.IdPuesto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idPuestoIC");
            });

            modelBuilder.Entity<Tiempo>(entity =>
            {
                entity.HasIndex(e => e.IdContrato)
                    .HasName("idContratoT_idx");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("idEmpeladoT_idx");

                entity.Property(e => e.EsIncapacidad).HasDefaultValueSql("'0'");

                entity.Property(e => e.EsInjustificado).HasDefaultValueSql("'0'");

                entity.Property(e => e.EsLaborado).HasDefaultValueSql("'0'");

                entity.Property(e => e.EsVacaciones).HasDefaultValueSql("'0'");

                entity.HasOne(d => d.IdContratoNavigation)
                    .WithMany(p => p.Tiempo)
                    .HasForeignKey(d => d.IdContrato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idContratoT");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Tiempo)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idEmpeladoT");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<SARH___JMéndez_Constructora.Models.Tiempo> Tiempo { get; set; }

    }
}
