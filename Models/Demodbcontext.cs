using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APISolution3;

public partial class Demodbcontext : DbContext
{
    public Demodbcontext()
    {
    }

    public Demodbcontext(DbContextOptions<Demodbcontext> options)
        : base(options)
    {
    }

    public virtual DbSet<MstCity> MstCities { get; set; }

    public virtual DbSet<MstCompany> MstCompanies { get; set; }

    public virtual DbSet<MstCountry> MstCountries { get; set; }

    public virtual DbSet<MstUser> MstUsers { get; set; }

    public virtual DbSet<Msttype> Msttypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-92R3ST6F\\SQLEXPRESS;Database=DBtest;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MstCity>(entity =>
        {
            entity.ToTable("MstCity");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<MstCompany>(entity =>
        {
            entity.HasKey(e => e.CompanyId);

            entity.ToTable("MstCompany");

            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.Modified).HasMaxLength(50);
            entity.Property(e => e.Others).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<MstCountry>(entity =>
        {
            entity.ToTable("MstCountry");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<MstUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("mstUser");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.LoginId).HasColumnName("LoginID");
        });

        modelBuilder.Entity<Msttype>(entity =>
        {
            entity.HasKey(e => e.TheId);

            entity.ToTable("Msttype");

            entity.Property(e => e.TheId).HasColumnName("TheID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
