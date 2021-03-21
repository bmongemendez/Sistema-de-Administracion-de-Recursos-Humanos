using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SARH___JMéndez_Constructora.Models2;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SARH___JMéndez_Constructora.Data2
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

        public virtual DbSet<Aguinaldos> Aguinaldos { get; set; }
        public virtual DbSet<Aspnetusersref> Aspnetusersref { get; set; }
        public virtual DbSet<Empleados> Empleados { get; set; }
        public virtual DbSet<Empleadosregistroauditoria> Empleadosregistroauditoria { get; set; }
        public virtual DbSet<Evaluaciones> Evaluaciones { get; set; }
        public virtual DbSet<Fincontrato> Fincontrato { get; set; }
        public virtual DbSet<Ingresocontrato> Ingresocontrato { get; set; }
        public virtual DbSet<Pagos> Pagos { get; set; }
        public virtual DbSet<Pagosregistroauditoria> Pagosregistroauditoria { get; set; }
        public virtual DbSet<Puestos> Puestos { get; set; }
        public virtual DbSet<Tiempo> Tiempo { get; set; }
        public virtual DbSet<Vacaciones> Vacaciones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=localhost;database=sahr.application;user=root;password=fidelitas");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aguinaldos>(entity =>
            {
                entity.HasIndex(e => e.IdContrato)
                    .HasName("idContratoP_idx");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("idEmpleadoA_idx");

                entity.HasOne(d => d.IdContratoNavigation)
                    .WithMany(p => p.Aguinaldos)
                    .HasForeignKey(d => d.IdContrato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idContratoA");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Aguinaldos)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idEmpleadoA");
            });

            modelBuilder.Entity<Aspnetusersref>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PRIMARY");
            });


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

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userNameEmpleados");
            });

            modelBuilder.Entity<Evaluaciones>(entity =>
            {
                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("idEmpleado_Eval_idx");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Evaluaciones)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idEmpleadoEval");
            });

            modelBuilder.Entity<Fincontrato>(entity =>
            {
                entity.HasKey(e => e.IdInicioContrato)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.IdInicioContrato)
                    .HasName("idInicioContratoFN_idx");

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

            modelBuilder.Entity<Pagos>(entity =>
            {
                entity.HasIndex(e => e.IdContrato)
                    .HasName("idContratoP_idx");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("idEmpleadoP_idx");

                entity.HasIndex(e => e.UserName)
                    .HasName("idUserPagos_idx");

                entity.Property(e => e.Deducciones).HasDefaultValueSql("'0.000'");

                entity.Property(e => e.HorasExtra).HasDefaultValueSql("'0.000'");

                entity.HasOne(d => d.IdContratoNavigation)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.IdContrato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idContratoPagos");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idEmpleadoPagos");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userNamePagos");
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
