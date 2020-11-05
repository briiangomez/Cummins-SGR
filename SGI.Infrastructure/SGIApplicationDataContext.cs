using Microsoft.EntityFrameworkCore;
using SGI.ApplicationCore.Entities;
using SGI.Infrastructure.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGI.Infrastructure
{
    public partial class SGIApplicationDataContext : DbContext
    {
        public SGIApplicationDataContext(DbContextOptions<SGIApplicationDataContext> options)
            : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder  //tie-up DbContext with LoggerFactory object
            .EnableSensitiveDataLogging();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            _ = new DealerBuilder(modelBuilder.Entity<Dealer>());
            _ = new ClienteBuilder(modelBuilder.Entity<Cliente>());
            _ = new MotorBuilder(modelBuilder.Entity<Motor>());
            _ = new EstadoGarantiaBuilder(modelBuilder.Entity<EstadoGarantia>());
            _ = new EstadoBuilder(modelBuilder.Entity<Estado>());
            _ = new FallaBuilder(modelBuilder.Entity<Falla>());
            _ = new IncidenciaBuilder(modelBuilder.Entity<Incidencia>());
            _ = new PermissionBuilder(modelBuilder.Entity<Permission>());
            _ = new PermissionBuilder(modelBuilder.Entity<Permission>());
            _ = new RolePermissionBuilder(modelBuilder.Entity<RolePermission>());
            _ = new RoleBuilder(modelBuilder.Entity<Role>());
            _ = new UserRoleBuilder(modelBuilder.Entity<UserRole>());
            _ = new UserBuilder(modelBuilder.Entity<User>());
        }
    }
}
