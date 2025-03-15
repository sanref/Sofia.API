using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Sofia.API.Models;

public partial class SofiaContext : DbContext
{
    string conn;
    public SofiaContext()
    {
    }

    public SofiaContext(DbContextOptions<SofiaContext> options)
        : base(options)
    {
    }

    public SofiaContext(DbContextOptions<SofiaContext> options, IConfiguration configuration)
        : base(options)
    {
        conn = configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string 'BloggingContext' not found.");
    }
    public virtual DbSet<ASSET> ASSETs { get; set; }

    public virtual DbSet<BROKER> BROKERs { get; set; }

    public virtual DbSet<HOLDING> HOLDINGs { get; set; }

    public virtual DbSet<OPERATION> OPERATIONs { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(conn);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ASSET>(entity =>
        {
            entity.HasKey(e => e.asset_id);

            entity.ToTable("ASSETS");

            entity.Property(e => e.asset_name).HasMaxLength(50);
            entity.Property(e => e.company).HasMaxLength(200);
            entity.Property(e => e.symbol).HasMaxLength(10);
        });

        modelBuilder.Entity<BROKER>(entity =>
        {
            entity.HasKey(e => e.broker_id);

            entity.ToTable("BROKERS");

            entity.Property(e => e.broker_name).HasMaxLength(50);
            entity.Property(e => e.logo).HasColumnType("image");
        });

        modelBuilder.Entity<HOLDING>(entity =>
        {
            entity.HasKey(e => e.holding_id);

            entity.ToTable("HOLDINGS");

            entity.Property(e => e.average_days).HasColumnType("numeric(4, 2)");
            entity.Property(e => e.average_price).HasColumnType("numeric(12, 6)");
            entity.Property(e => e.quantity).HasColumnType("numeric(9, 9)");
        });

        modelBuilder.Entity<OPERATION>(entity =>
        {
            entity.HasKey(e => e.operation_id);

            entity.ToTable("OPERATIONS");

            entity.Property(e => e.currency).HasMaxLength(4);
            entity.Property(e => e.date).HasColumnType("datetime");
            entity.Property(e => e.price).HasColumnType("numeric(12, 6)");
            entity.Property(e => e.quantity).HasColumnType("numeric(9, 9)");
            entity.Property(e => e.type).HasMaxLength(4);

            entity.HasOne(d => d.asset).WithMany(p => p.OPERATIONs)
                .HasForeignKey(d => d.asset_id)
                .HasConstraintName("FK_OPERATIONS_ASSETS");

            entity.HasOne(d => d.broker).WithMany(p => p.OPERATIONs)
                .HasForeignKey(d => d.broker_id)
                .HasConstraintName("FK_OPERATIONS_BROKERS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
