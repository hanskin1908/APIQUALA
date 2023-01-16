using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIQUALA.Models;

public partial class TestDbContext : DbContext
{
    public TestDbContext()
    {
    }

    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MonMonedum> MonMoneda { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ins-dllo-test-01.public.33e082952ab4.database.windows.net,3342;Database=TestDB;user id=prueba;password=pruebaconcepto;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MonMonedum>(entity =>
        {
            entity.HasKey(e => e.IdMoneda).HasName("PK__mon_mone__807063E324FD8C6B");

            entity.ToTable("mon_moneda");

            entity.Property(e => e.IdMoneda)
                .ValueGeneratedNever()
                .HasColumnName("id_moneda");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__sucursal__40F9A20776F0A32C");

            entity.ToTable("sucursal");

            entity.Property(e => e.Codigo)
                .ValueGeneratedNever()
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Direccion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.IdMonedaSuc).HasColumnName("id_moneda_suc");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("identificacion");

            entity.HasOne(d => d.IdMonedaSucNavigation).WithMany(p => p.Sucursals)
                .HasForeignKey(d => d.IdMonedaSuc)
                .HasConstraintName("FK__sucursal__id_mon__7CD98669");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
