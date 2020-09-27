using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGI.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGI.Infrastructure.Builders
{
    public class DealerBuilder : BaseBuilder<Dealer>
    {
        public DealerBuilder(EntityTypeBuilder<Dealer> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.ToTable("Dealers");

            entityBuilder.Property(x => x.Name).HasColumnName("Name").HasColumnType("varchar(200)").IsRequired();
            entityBuilder.Property(x => x.LocationCode).HasColumnName("LocationCode").HasColumnType("varchar(200)");
            entityBuilder.Property(x => x.DistributorCode).HasColumnName("DistributorCode").HasColumnType("varchar(200)");
            entityBuilder.Property(x => x.Country).HasColumnName("Country").HasColumnType("varchar(200)");
            entityBuilder.Property(x => x.State).HasColumnName("State").HasColumnType("varchar(200)");
            entityBuilder.Property(x => x.City).HasColumnName("City").HasColumnType("varchar(200)");
            entityBuilder.Property(x => x.Address).HasColumnName("Address").HasColumnType("varchar(MAX)");
            entityBuilder.Property(x => x.Phone).HasColumnName("Phone").HasColumnType("varchar(200)");
            entityBuilder.Property(x => x.Fax).HasColumnName("Fax").HasColumnType("varchar(200)");
            entityBuilder.Property(x => x.Zip).HasColumnName("Zip").HasColumnType("varchar(200)");
            entityBuilder.Property(x => x.Email).HasColumnName("Email").HasColumnType("varchar(200)");
            entityBuilder.Property(x => x.Website).HasColumnName("Website").HasColumnType("varchar(MAX)");
            entityBuilder.Property(x => x.LatitudGps).HasColumnName("LatitudGps").HasColumnType("float");
            entityBuilder.Property(x => x.LongitudGps).HasColumnName("LongitudGps").HasColumnType("float");
            entityBuilder.Property(x => x.SkipLongitude).HasColumnName("SkipLongitude").HasColumnType("varchar(MAX)");


        }
    }
}
