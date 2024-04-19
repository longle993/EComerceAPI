using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebSuggestAPI.Model.Model;

public partial class DbContextWebSuggest : DbContext
{
    public DbContextWebSuggest()
    {
    }

    public DbContextWebSuggest(DbContextOptions<DbContextWebSuggest> options)
        : base(options)
    {
    }

    public virtual DbSet<HinhAnhSanPham> HinhAnhSanPhams { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }
    public virtual DbSet<HoaDonShow> HoaDonShows { get; set; }


    public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<TanSuatHaiSanPham> TanSuatHaiSanPhams { get; set; }

    public virtual DbSet<TanSuatMotSanPham> TanSuatMotSanPhams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:connectString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HinhAnhSanPham>(entity =>
        {
            entity.HasKey(e => e.IdHinhAnh).HasName("PK__HinhAnhS__97A548CFC12CBAA1");

            entity.ToTable("HinhAnhSanPham");

            entity.Property(e => e.IdHinhAnh)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.HinhAnh)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.IdSanPham)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.IdSanPhamNavigation).WithMany(p => p.HinhAnhSanPhams)
                .HasForeignKey(d => d.IdSanPham)
                .HasConstraintName("FK__HinhAnhSa__IdSan__2F10007B");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.IdHoaDon).HasName("PK__HoaDon__4DD461C8B25AFB7F");

            entity.ToTable("HoaDon");

            entity.Property(e => e.IdHoaDon).HasMaxLength(255);
        });

        modelBuilder.Entity<HoaDonShow>(entity =>
        {
            entity.HasKey(e => e.IdHoaDon).HasName("PK__HoaDonShow__4DD461C8B25AFB7F");

            entity.ToTable("HoaDonShow");

            entity.Property(e => e.IdHoaDon).HasMaxLength(255);
        });

        modelBuilder.Entity<LoaiSanPham>(entity =>
        {
            entity.HasKey(e => e.IdLoaiSp).HasName("PK__LoaiSanP__B41BA51C77BD8DF9");

            entity.ToTable("LoaiSanPham");

            entity.Property(e => e.IdLoaiSp)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("IdLoaiSP");
            entity.Property(e => e.LoaiSp)
                .HasMaxLength(70)
                .HasColumnName("LoaiSP");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.IdSanPham).HasName("PK__SanPham__5FFA2D42D229BF31");

            entity.ToTable("SanPham");

            entity.Property(e => e.IdSanPham)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.GiaSp)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("GiaSP");
            entity.Property(e => e.IdLoaiSp)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("IdLoaiSP");
            entity.Property(e => e.TenSanPham).HasMaxLength(200);

            entity.HasOne(d => d.IdLoaiSpNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.IdLoaiSp)
                .HasConstraintName("FK__SanPham__IdLoaiS__2C3393D0");
        });

        modelBuilder.Entity<TanSuatHaiSanPham>(entity =>
        {
            entity.HasKey(e => e.ThuTu).HasName("PK__TanSuatH__2E2833D04BA48373");

            entity.ToTable("TanSuatHaiSanPham");

            entity.Property(e => e.ThuTu).ValueGeneratedNever();
        });

        modelBuilder.Entity<TanSuatMotSanPham>(entity =>
        {
            entity.HasKey(e => e.ThuTu).HasName("PK__TanSuatM__2E2833D04CE24A8F");

            entity.ToTable("TanSuatMotSanPham");

            entity.Property(e => e.ThuTu).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
