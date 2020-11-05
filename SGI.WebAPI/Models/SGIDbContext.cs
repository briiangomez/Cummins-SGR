using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SGIWebApi.Models
{
    public partial class SGIDbContext : DbContext
    {
        public SGIDbContext()
        {
        }

        public SGIDbContext(DbContextOptions<SGIDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Certificacion> Certificacions { get; set; }
        public virtual DbSet<CertificacionDealer> CertificacionDealers { get; set; }
        public virtual DbSet<CertificacionMotor> CertificacionMotors { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Dealer> Dealers { get; set; }
        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<EstadoGarantia> EstadoGarantias { get; set; }
        public virtual DbSet<EstadoIncidencia> EstadoIncidencias { get; set; }
        public virtual DbSet<Falla> Fallas { get; set; }
        public virtual DbSet<Incidencia> Incidencias { get; set; }
        public virtual DbSet<Motor> Motors { get; set; }
        public virtual DbSet<MotorIncidencia> MotorIncidencias { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Sintoma> Sintomas { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=SGI");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Certificacion>(entity =>
            {
                entity.ToTable("Certificacion");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Aux1).IsUnicode(false);

                entity.Property(e => e.Aux2).IsUnicode(false);

                entity.Property(e => e.Aux3).IsUnicode(false);

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.Modified).HasColumnType("datetime");
            });

            modelBuilder.Entity<CertificacionDealer>(entity =>
            {
                entity.ToTable("CertificacionDealer");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.HasOne(d => d.Certificacion)
                    .WithMany(p => p.CertificacionDealers)
                    .HasForeignKey(d => d.CertificacionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CertificacionesDealer_Certificaciones");

                entity.HasOne(d => d.Dealer)
                    .WithMany(p => p.CertificacionDealers)
                    .HasForeignKey(d => d.DealerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CertificacionesDealer_Dealers");
            });

            modelBuilder.Entity<CertificacionMotor>(entity =>
            {
                entity.ToTable("CertificacionMotor");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.HasOne(d => d.Certificacion)
                    .WithMany(p => p.CertificacionMotors)
                    .HasForeignKey(d => d.CertificacionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CertificacionesMotor_Certificaciones");

                entity.HasOne(d => d.Motor)
                    .WithMany(p => p.CertificacionMotors)
                    .HasForeignKey(d => d.MotorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CertificacionesMotor_Motor");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Celular).IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Direccion).IsUnicode(false);

                entity.Property(e => e.Dni)
                    .HasColumnName("DNI")
                    .IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Localidad).IsUnicode(false);

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Nombre).IsUnicode(false);

                entity.Property(e => e.Provincia).IsUnicode(false);

                entity.Property(e => e.Telefono).IsUnicode(false);

                entity.Property(e => e.TipoDni)
                    .HasColumnName("TipoDNI")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Dealer>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.DistributorCode)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LocationCode)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SkipLongitude).IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Website).IsUnicode(false);

                entity.Property(e => e.Zip)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Codigo).ValueGeneratedOnAdd();

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.Modified).HasColumnType("datetime");
            });

            modelBuilder.Entity<EstadoGarantia>(entity =>
            {
                entity.ToTable("EstadoGarantia");

                entity.HasIndex(e => e.IdIncidencia);

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Codigo).ValueGeneratedOnAdd();

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ObservacionesGarantia).IsUnicode(false);

                entity.Property(e => e.ObservacionesProveedor).IsUnicode(false);

                entity.HasOne(d => d.IdIncidenciaNavigation)
                    .WithMany(p => p.EstadoGarantias)
                    .HasForeignKey(d => d.IdIncidencia)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<EstadoIncidencia>(entity =>
            {
                entity.ToTable("EstadoIncidencia");

                entity.HasIndex(e => e.EstadoId);

                entity.HasIndex(e => e.IncidenciaId);

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Aux1).IsUnicode(false);

                entity.Property(e => e.Aux2).IsUnicode(false);

                entity.Property(e => e.Aux3).IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Observacion).IsUnicode(false);

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.EstadoIncidencias)
                    .HasForeignKey(d => d.EstadoId);

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.EstadoIncidencias)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_EstadoIncidencia_User");

                entity.HasOne(d => d.Incidencia)
                    .WithMany(p => p.EstadoIncidencias)
                    .HasForeignKey(d => d.IncidenciaId);
            });

            modelBuilder.Entity<Falla>(entity =>
            {
                entity.ToTable("Falla");

                entity.HasIndex(e => e.IdIncidencia);

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Nombre).IsUnicode(false);

                entity.Property(e => e.Observaciones).IsUnicode(false);

                entity.HasOne(d => d.IdIncidenciaNavigation)
                    .WithMany(p => p.Fallas)
                    .HasForeignKey(d => d.IdIncidencia)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Incidencia>(entity =>
            {
                entity.HasIndex(e => e.IdDealer);

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Aux1).IsUnicode(false);

                entity.Property(e => e.Aux2).IsUnicode(false);

                entity.Property(e => e.Aux3).IsUnicode(false);

                entity.Property(e => e.Aux4).IsUnicode(false);

                entity.Property(e => e.Aux5).IsUnicode(false);

                entity.Property(e => e.ConfiguracionCorta).IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.DireccionInspeccion).IsUnicode(false);

                entity.Property(e => e.Equipo).IsUnicode(false);

                entity.Property(e => e.FechaCierre).HasColumnType("datetime");

                entity.Property(e => e.FechaIncidencia).HasColumnType("datetime");

                entity.Property(e => e.FechaPreEntrega).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.ImagenComprobante).IsUnicode(false);

                entity.Property(e => e.ModeloEquipo).IsUnicode(false);

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.NroIncidenciaPauny).IsUnicode(false);

                entity.Property(e => e.PathImagenes).IsUnicode(false);

                entity.Property(e => e.Sintoma).IsUnicode(false);

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Incidencias)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK_Incidencias_Clientes");

                entity.HasOne(d => d.IdDealerNavigation)
                    .WithMany(p => p.Incidencias)
                    .HasForeignKey(d => d.IdDealer)
                    .HasConstraintName("FK_Incidencias_Dealers1");
            });

            modelBuilder.Entity<Motor>(entity =>
            {
                entity.ToTable("Motor");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Equipo).IsUnicode(false);

                entity.Property(e => e.Modelo).IsUnicode(false);

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.NumeroMotor).IsUnicode(false);
            });

            modelBuilder.Entity<MotorIncidencia>(entity =>
            {
                entity.ToTable("MotorIncidencia");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.FechaCompra).HasColumnType("datetime");

                entity.Property(e => e.FechaFalla).HasColumnType("datetime");

                entity.Property(e => e.FechaInicioGarantia).HasColumnType("datetime");

                entity.Property(e => e.ModeloEquipo).IsUnicode(false);

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.NumeroChasis).IsUnicode(false);

                entity.Property(e => e.NumeroMotor).IsUnicode(false);

                entity.HasOne(d => d.Incidencia)
                    .WithMany(p => p.MotorIncidencias)
                    .HasForeignKey(d => d.IncidenciaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MotorIncidencia_Incidencias");

                entity.HasOne(d => d.Motor)
                    .WithMany(p => p.MotorIncidencias)
                    .HasForeignKey(d => d.MotorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MotorIncidencia_Motor");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshToken");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnName("expiry_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnName("token")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_RefreshToken_User");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Modified).HasColumnType("datetime");
            });

            modelBuilder.Entity<Sintoma>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Aux1).IsUnicode(false);

                entity.Property(e => e.Aux2).IsUnicode(false);

                entity.Property(e => e.Aux3).IsUnicode(false);

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.Modified).HasColumnType("datetime");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasColumnName("email_address")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.HireDate)
                    .HasColumnName("hire_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasColumnName("middle_name")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .IsRequired()
                    .HasColumnName("source")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdDealerNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdDealer)
                    .HasConstraintName("FK_User_Dealers");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRole)
                    .HasConstraintName("FK_User_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
