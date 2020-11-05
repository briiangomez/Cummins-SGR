using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using Sgi.Models.SgiCore;

namespace Sgi.Data
{
  public partial class SgiCoreContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public SgiCoreContext(DbContextOptions<SgiCoreContext> options):base(options)
    {
    }

    public SgiCoreContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Sgi.Models.SgiCore.ClienteIncidencium>()
              .HasOne(i => i.Cliente)
              .WithMany(i => i.ClienteIncidencia)
              .HasForeignKey(i => i.ClienteId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<Sgi.Models.SgiCore.ClienteIncidencium>()
              .HasOne(i => i.Incidencia)
              .WithMany(i => i.ClienteIncidencia)
              .HasForeignKey(i => i.IncidenciaId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<Sgi.Models.SgiCore.EstadoGarantium>()
              .HasOne(i => i.Incidencia)
              .WithMany(i => i.EstadoGarantia)
              .HasForeignKey(i => i.IdIncidencia)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<Sgi.Models.SgiCore.EstadoIncidencium>()
              .HasOne(i => i.Incidencia)
              .WithMany(i => i.EstadoIncidencia)
              .HasForeignKey(i => i.IncidenciaId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<Sgi.Models.SgiCore.EstadoIncidencium>()
              .HasOne(i => i.Estado)
              .WithMany(i => i.EstadoIncidencia)
              .HasForeignKey(i => i.EstadoId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<Sgi.Models.SgiCore.Falla>()
              .HasOne(i => i.Incidencia)
              .WithMany(i => i.Fallas)
              .HasForeignKey(i => i.IdIncidencia)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<Sgi.Models.SgiCore.Incidencia>()
              .HasOne(i => i.Dealer)
              .WithMany(i => i.Incidencia)
              .HasForeignKey(i => i.IdDealer)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<Sgi.Models.SgiCore.MotorIncidencium>()
              .HasOne(i => i.Motor)
              .WithMany(i => i.MotorIncidencia)
              .HasForeignKey(i => i.MotorId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<Sgi.Models.SgiCore.MotorIncidencium>()
              .HasOne(i => i.Incidencia)
              .WithMany(i => i.MotorIncidencia)
              .HasForeignKey(i => i.IncidenciaId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<Sgi.Models.SgiCore.RolePermission>()
              .HasOne(i => i.Role)
              .WithMany(i => i.RolePermissions)
              .HasForeignKey(i => i.RoleId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<Sgi.Models.SgiCore.RolePermission>()
              .HasOne(i => i.Permission)
              .WithMany(i => i.RolePermissions)
              .HasForeignKey(i => i.PermissionId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<Sgi.Models.SgiCore.UserRole>()
              .HasOne(i => i.User)
              .WithMany(i => i.UserRoles)
              .HasForeignKey(i => i.UserId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<Sgi.Models.SgiCore.UserRole>()
              .HasOne(i => i.Role)
              .WithMany(i => i.UserRoles)
              .HasForeignKey(i => i.RoleId)
              .HasPrincipalKey(i => i.Id);

        builder.Entity<Sgi.Models.SgiCore.Cliente>()
              .Property(p => p.Id)
              .HasDefaultValueSql("(newid())");

        builder.Entity<Sgi.Models.SgiCore.Cliente>()
              .Property(p => p.Created)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<Sgi.Models.SgiCore.ClienteIncidencium>()
              .Property(p => p.Id)
              .HasDefaultValueSql("(newid())");

        builder.Entity<Sgi.Models.SgiCore.ClienteIncidencium>()
              .Property(p => p.Created)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<Sgi.Models.SgiCore.Dealer>()
              .Property(p => p.Id)
              .HasDefaultValueSql("(newid())");

        builder.Entity<Sgi.Models.SgiCore.Dealer>()
              .Property(p => p.Created)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<Sgi.Models.SgiCore.Estado>()
              .Property(p => p.Id)
              .HasDefaultValueSql("(newid())");

        builder.Entity<Sgi.Models.SgiCore.Estado>()
              .Property(p => p.Created)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<Sgi.Models.SgiCore.EstadoGarantium>()
              .Property(p => p.Id)
              .HasDefaultValueSql("(newid())");

        builder.Entity<Sgi.Models.SgiCore.EstadoGarantium>()
              .Property(p => p.Created)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<Sgi.Models.SgiCore.EstadoIncidencium>()
              .Property(p => p.Id)
              .HasDefaultValueSql("(newid())");

        builder.Entity<Sgi.Models.SgiCore.EstadoIncidencium>()
              .Property(p => p.Created)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<Sgi.Models.SgiCore.Falla>()
              .Property(p => p.Id)
              .HasDefaultValueSql("(newid())");

        builder.Entity<Sgi.Models.SgiCore.Falla>()
              .Property(p => p.Created)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<Sgi.Models.SgiCore.Incidencia>()
              .Property(p => p.Id)
              .HasDefaultValueSql("(newid())");

        builder.Entity<Sgi.Models.SgiCore.Incidencia>()
              .Property(p => p.Created)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<Sgi.Models.SgiCore.Motor>()
              .Property(p => p.Id)
              .HasDefaultValueSql("(newid())");

        builder.Entity<Sgi.Models.SgiCore.Motor>()
              .Property(p => p.Created)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<Sgi.Models.SgiCore.MotorIncidencium>()
              .Property(p => p.Id)
              .HasDefaultValueSql("(newid())");

        builder.Entity<Sgi.Models.SgiCore.MotorIncidencium>()
              .Property(p => p.Created)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<Sgi.Models.SgiCore.Permission>()
              .Property(p => p.Id)
              .HasDefaultValueSql("(newid())");

        builder.Entity<Sgi.Models.SgiCore.Permission>()
              .Property(p => p.Created)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<Sgi.Models.SgiCore.Role>()
              .Property(p => p.Id)
              .HasDefaultValueSql("(newid())");

        builder.Entity<Sgi.Models.SgiCore.Role>()
              .Property(p => p.Created)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<Sgi.Models.SgiCore.RolePermission>()
              .Property(p => p.Id)
              .HasDefaultValueSql("(newid())");

        builder.Entity<Sgi.Models.SgiCore.RolePermission>()
              .Property(p => p.Created)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<Sgi.Models.SgiCore.User>()
              .Property(p => p.Id)
              .HasDefaultValueSql("(newid())");

        builder.Entity<Sgi.Models.SgiCore.User>()
              .Property(p => p.Created)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<Sgi.Models.SgiCore.UserRole>()
              .Property(p => p.Id)
              .HasDefaultValueSql("(newid())");

        builder.Entity<Sgi.Models.SgiCore.UserRole>()
              .Property(p => p.Created)
              .HasDefaultValueSql("(getdate())");


        builder.Entity<Sgi.Models.SgiCore.Cliente>()
              .Property(p => p.Created)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Cliente>()
              .Property(p => p.Modified)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Cliente>()
              .Property(p => p.Deleted)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.ClienteIncidencium>()
              .Property(p => p.Created)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.ClienteIncidencium>()
              .Property(p => p.Modified)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.ClienteIncidencium>()
              .Property(p => p.Deleted)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Dealer>()
              .Property(p => p.Created)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Dealer>()
              .Property(p => p.Modified)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Dealer>()
              .Property(p => p.Deleted)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Estado>()
              .Property(p => p.Created)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Estado>()
              .Property(p => p.Modified)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Estado>()
              .Property(p => p.Deleted)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.EstadoGarantium>()
              .Property(p => p.Created)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.EstadoGarantium>()
              .Property(p => p.Modified)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.EstadoGarantium>()
              .Property(p => p.Deleted)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.EstadoIncidencium>()
              .Property(p => p.Created)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.EstadoIncidencium>()
              .Property(p => p.Modified)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.EstadoIncidencium>()
              .Property(p => p.Deleted)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Falla>()
              .Property(p => p.Created)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Falla>()
              .Property(p => p.Modified)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Falla>()
              .Property(p => p.Deleted)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Incidencia>()
              .Property(p => p.Created)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Incidencia>()
              .Property(p => p.Modified)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Incidencia>()
              .Property(p => p.Deleted)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Incidencia>()
              .Property(p => p.FechaPreEntrega)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Incidencia>()
              .Property(p => p.FechaIncidencia)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Incidencia>()
              .Property(p => p.FechaRegistro)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Incidencia>()
              .Property(p => p.FechaCierre)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Motor>()
              .Property(p => p.Created)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Motor>()
              .Property(p => p.Modified)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Motor>()
              .Property(p => p.Deleted)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.MotorIncidencium>()
              .Property(p => p.Created)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.MotorIncidencium>()
              .Property(p => p.Modified)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.MotorIncidencium>()
              .Property(p => p.Deleted)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.MotorIncidencium>()
              .Property(p => p.FechaCompra)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.MotorIncidencium>()
              .Property(p => p.FechaInicioGarantia)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.MotorIncidencium>()
              .Property(p => p.FechaFalla)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Permission>()
              .Property(p => p.Created)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Permission>()
              .Property(p => p.Modified)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Permission>()
              .Property(p => p.Deleted)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Role>()
              .Property(p => p.Created)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Role>()
              .Property(p => p.Modified)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.Role>()
              .Property(p => p.Deleted)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.RolePermission>()
              .Property(p => p.Created)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.RolePermission>()
              .Property(p => p.Modified)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.RolePermission>()
              .Property(p => p.Deleted)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.User>()
              .Property(p => p.Created)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.User>()
              .Property(p => p.Modified)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.User>()
              .Property(p => p.Deleted)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.UserRole>()
              .Property(p => p.Created)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.UserRole>()
              .Property(p => p.Modified)
              .HasColumnType("datetime");

        builder.Entity<Sgi.Models.SgiCore.UserRole>()
              .Property(p => p.Deleted)
              .HasColumnType("datetime");

        this.OnModelBuilding(builder);
    }


    public DbSet<Sgi.Models.SgiCore.Cliente> Clientes
    {
      get;
      set;
    }

    public DbSet<Sgi.Models.SgiCore.ClienteIncidencium> ClienteIncidencia
    {
      get;
      set;
    }

    public DbSet<Sgi.Models.SgiCore.Dealer> Dealers
    {
      get;
      set;
    }

    public DbSet<Sgi.Models.SgiCore.Estado> Estados
    {
      get;
      set;
    }

    public DbSet<Sgi.Models.SgiCore.EstadoGarantium> EstadoGarantia
    {
      get;
      set;
    }

    public DbSet<Sgi.Models.SgiCore.EstadoIncidencium> EstadoIncidencia
    {
      get;
      set;
    }

    public DbSet<Sgi.Models.SgiCore.Falla> Fallas
    {
      get;
      set;
    }

    public DbSet<Sgi.Models.SgiCore.Incidencia> Incidencia
    {
      get;
      set;
    }

    public DbSet<Sgi.Models.SgiCore.Motor> Motors
    {
      get;
      set;
    }

    public DbSet<Sgi.Models.SgiCore.MotorIncidencium> MotorIncidencia
    {
      get;
      set;
    }

    public DbSet<Sgi.Models.SgiCore.Permission> Permissions
    {
      get;
      set;
    }

    public DbSet<Sgi.Models.SgiCore.Role> Roles
    {
      get;
      set;
    }

    public DbSet<Sgi.Models.SgiCore.RolePermission> RolePermissions
    {
      get;
      set;
    }

    public DbSet<Sgi.Models.SgiCore.User> Users
    {
      get;
      set;
    }

    public DbSet<Sgi.Models.SgiCore.UserRole> UserRoles
    {
      get;
      set;
    }
  }
}
