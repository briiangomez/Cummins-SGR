using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SGR.Models.Models
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
        public virtual DbSet<Equipo> Equipos { get; set; }
        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<EstadoGarantium> EstadoGarantia { get; set; }
        public virtual DbSet<EstadoIncidencium> EstadoIncidencia { get; set; }
        public virtual DbSet<Falla> Fallas { get; set; }
        public virtual DbSet<ImagenesIncidencium> ImagenesIncidencia { get; set; }
        public virtual DbSet<Incidencia> Incidencias { get; set; }
        public virtual DbSet<IncidenciaSurvey> IncidenciaSurveys { get; set; }
        public virtual DbSet<Motor> Motors { get; set; }
        public virtual DbSet<MotorDealer> MotorDealers { get; set; }
        public virtual DbSet<MotorIncidencium> MotorIncidencia { get; set; }
        public virtual DbSet<Notificacione> Notificaciones { get; set; }
        public virtual DbSet<Oem> Oems { get; set; }
        public virtual DbSet<Pai> Pais { get; set; }
        public virtual DbSet<Provincium> Provincia { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Sintoma> Sintomas { get; set; }
        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<SurveyAnswer> SurveyAnswers { get; set; }
        public virtual DbSet<SurveyItem> SurveyItems { get; set; }
        public virtual DbSet<SurveyItemOption> SurveyItemOptions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-1CD4841;Database=FASS-DB;Trusted_Connection=True;");
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

                entity.Property(e => e.Aux1).IsUnicode(false);

                entity.Property(e => e.Aux2).IsUnicode(false);

                entity.Property(e => e.Aux3).IsUnicode(false);

                entity.Property(e => e.Celular).IsUnicode(false);

                entity.Property(e => e.Contacto).IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Direccion).IsUnicode(false);

                entity.Property(e => e.Dni)
                    .IsUnicode(false)
                    .HasColumnName("DNI");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Localidad).IsUnicode(false);

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Nombre).IsUnicode(false);

                entity.Property(e => e.Provincia).IsUnicode(false);

                entity.Property(e => e.Telefono).IsUnicode(false);

                entity.Property(e => e.TipoDni)
                    .IsUnicode(false)
                    .HasColumnName("TipoDNI");
            });

            modelBuilder.Entity<Dealer>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.Aux1).IsUnicode(false);

                entity.Property(e => e.Aux2).IsUnicode(false);

                entity.Property(e => e.Aux3).IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Contacto).IsUnicode(false);

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

                entity.HasOne(d => d.Pais)
                    .WithMany(p => p.Dealers)
                    .HasForeignKey(d => d.PaisId)
                    .HasConstraintName("FK_Dealers_Pais");

                entity.HasOne(d => d.Provincia)
                    .WithMany(p => p.Dealers)
                    .HasForeignKey(d => d.ProvinciaId)
                    .HasConstraintName("FK_Dealers_Provincia");
            });

            modelBuilder.Entity<Equipo>(entity =>
            {
                entity.ToTable("Equipo");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Equipo1)
                    .IsUnicode(false)
                    .HasColumnName("Equipo");

                entity.Property(e => e.Modelo).IsUnicode(false);

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.NumeroMotor).IsUnicode(false);

                entity.Property(e => e.Oemid).HasColumnName("OEMId");

                entity.HasOne(d => d.Motor)
                    .WithMany(p => p.Equipos)
                    .HasForeignKey(d => d.MotorId)
                    .HasConstraintName("FK_Equipo_Motor");

                entity.HasOne(d => d.Oem)
                    .WithMany(p => p.Equipos)
                    .HasForeignKey(d => d.Oemid)
                    .HasConstraintName("FK_Equipo_OEM");
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

            modelBuilder.Entity<EstadoGarantium>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Codigo).ValueGeneratedOnAdd();

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.IdEstadoGarantia).HasColumnName("idEstadoGarantia");

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ObservacionesGarantia).IsUnicode(false);

                entity.Property(e => e.ObservacionesProveedor).IsUnicode(false);

                entity.HasOne(d => d.IdIncidenciaNavigation)
                    .WithMany(p => p.EstadoGarantia)
                    .HasForeignKey(d => d.IdIncidencia)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<EstadoIncidencium>(entity =>
            {
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
                    .WithMany(p => p.EstadoIncidencia)
                    .HasForeignKey(d => d.EstadoId);

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.EstadoIncidencia)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_EstadoIncidencia_User");

                entity.HasOne(d => d.Incidencia)
                    .WithMany(p => p.EstadoIncidencia)
                    .HasForeignKey(d => d.IncidenciaId);
            });

            modelBuilder.Entity<Falla>(entity =>
            {
                entity.ToTable("Falla");

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

            modelBuilder.Entity<ImagenesIncidencium>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.FechaCarga).HasColumnType("datetime");

                entity.Property(e => e.Imagen).IsUnicode(false);

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Tipo).IsUnicode(false);

                entity.HasOne(d => d.Incidencia)
                    .WithMany(p => p.ImagenesIncidencia)
                    .HasForeignKey(d => d.IncidenciaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImagenesIncidencia_Incidencias");
            });

            modelBuilder.Entity<Incidencia>(entity =>
            {
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

                entity.Property(e => e.NroIncidencia).ValueGeneratedOnAdd();

                entity.Property(e => e.NroIncidenciaPauny).IsUnicode(false);

                entity.Property(e => e.PathImagenes).IsUnicode(false);

                entity.Property(e => e.Sintoma).IsUnicode(false);

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Incidencia)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK_Incidencias_Clientes");

                entity.HasOne(d => d.IdDealerNavigation)
                    .WithMany(p => p.Incidencia)
                    .HasForeignKey(d => d.IdDealer)
                    .HasConstraintName("FK_Incidencias_Dealers1");
            });

            modelBuilder.Entity<IncidenciaSurvey>(entity =>
            {
                entity.ToTable("IncidenciaSurvey");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Aux1).IsUnicode(false);

                entity.Property(e => e.Aux2).IsUnicode(false);

                entity.Property(e => e.Aux3).IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.HasOne(d => d.IdIncidenciaNavigation)
                    .WithMany(p => p.IncidenciaSurveys)
                    .HasForeignKey(d => d.IdIncidencia)
                    .HasConstraintName("FK_IncidenciaSurvey_Incidencias");

                entity.HasOne(d => d.IdSurveyNavigation)
                    .WithMany(p => p.IncidenciaSurveys)
                    .HasForeignKey(d => d.IdSurvey)
                    .HasConstraintName("FK_IncidenciaSurvey_Survey");
            });

            modelBuilder.Entity<Motor>(entity =>
            {
                entity.ToTable("Motor");

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

            modelBuilder.Entity<MotorDealer>(entity =>
            {
                entity.ToTable("MotorDealer");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.HasOne(d => d.Dealer)
                    .WithMany(p => p.MotorDealers)
                    .HasForeignKey(d => d.DealerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MotorDealer_Dealers");
            });

            modelBuilder.Entity<MotorIncidencium>(entity =>
            {
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
                    .WithMany(p => p.MotorIncidencia)
                    .HasForeignKey(d => d.IncidenciaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MotorIncidencia_Incidencias");

                entity.HasOne(d => d.Motor)
                    .WithMany(p => p.MotorIncidencia)
                    .HasForeignKey(d => d.MotorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MotorIncidencia_Motor");
            });

            modelBuilder.Entity<Notificacione>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Aux1).IsUnicode(false);

                entity.Property(e => e.Aux2).IsUnicode(false);

                entity.Property(e => e.Aux3).IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Notificaciones)
                    .HasForeignKey(d => d.EstadoId)
                    .HasConstraintName("FK_Notificaciones_Notificaciones");
            });

            modelBuilder.Entity<Oem>(entity =>
            {
                entity.ToTable("OEM");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Aux1).IsUnicode(false);

                entity.Property(e => e.Aux2).IsUnicode(false);

                entity.Property(e => e.Aux3).IsUnicode(false);

                entity.Property(e => e.Codigo).IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Nombre).IsUnicode(false);
            });

            modelBuilder.Entity<Pai>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Aux1).IsUnicode(false);

                entity.Property(e => e.Aux2).IsUnicode(false);

                entity.Property(e => e.Aux3).IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Nombre).IsUnicode(false);
            });

            modelBuilder.Entity<Provincium>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Aux1).IsUnicode(false);

                entity.Property(e => e.Aux2).IsUnicode(false);

                entity.Property(e => e.Aux3).IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Nombre).IsUnicode(false);

                entity.HasOne(d => d.Pais)
                    .WithMany(p => p.Provincia)
                    .HasForeignKey(d => d.PaisId)
                    .HasConstraintName("FK_Provincia_Pais");
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
                    .HasColumnType("datetime")
                    .HasColumnName("expiry_date");

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("token");

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

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Aux1).IsUnicode(false);

                entity.Property(e => e.Aux2).IsUnicode(false);

                entity.Property(e => e.Aux3).IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Key).IsUnicode(false);

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Value).IsUnicode(false);
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

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.ToTable("Survey");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.SurveyName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Surveys)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Survey_Users");
            });

            modelBuilder.Entity<SurveyAnswer>(entity =>
            {
                entity.ToTable("SurveyAnswer");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AnswerValue).HasMaxLength(500);

                entity.Property(e => e.AnswerValueDateTime).HasColumnType("datetime");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.HasOne(d => d.SurveyItem)
                    .WithMany(p => p.SurveyAnswers)
                    .HasForeignKey(d => d.SurveyItemId)
                    .HasConstraintName("FK_SurveyAnswer_SurveyItem");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SurveyAnswers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyAnswer_Users");
            });

            modelBuilder.Entity<SurveyItem>(entity =>
            {
                entity.ToTable("SurveyItem");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.ItemLabel)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ItemType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ItemValue).HasMaxLength(50);

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.HasOne(d => d.SurveyNavigation)
                    .WithMany(p => p.SurveyItems)
                    .HasForeignKey(d => d.Survey)
                    .HasConstraintName("FK_SurveyItem_Survey");
            });

            modelBuilder.Entity<SurveyItemOption>(entity =>
            {
                entity.ToTable("SurveyItemOption");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.OptionLabel)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.SurveyItemNavigation)
                    .WithMany(p => p.SurveyItemOptions)
                    .HasForeignKey(d => d.SurveyItem)
                    .HasConstraintName("FK_SurveyItemOption_SurveyItem");
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
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email_address");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.HireDate)
                    .HasColumnType("datetime")
                    .HasColumnName("hire_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("middle_name")
                    .IsFixedLength(true);

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Source)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("source");

                entity.HasOne(d => d.IdDealerNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdDealer)
                    .HasConstraintName("FK_User_Dealers");

                entity.HasOne(d => d.IdOemNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdOem)
                    .HasConstraintName("FK_User_OEM");

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
