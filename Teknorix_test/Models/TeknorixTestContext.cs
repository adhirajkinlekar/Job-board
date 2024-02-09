using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Teknorix_test.Models;

public partial class TeknorixTestContext : DbContext
{
    public TeknorixTestContext()
    {
    }

    public TeknorixTestContext(DbContextOptions<TeknorixTestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyLocation> CompanyLocations { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<State> States { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:TTConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__companie__3E2672353C4DE5E4");

            entity.ToTable("companies");

            entity.HasIndex(e => e.CompanyName, "UQ__companie__6D1B87CB7CD85542").IsUnique();

            entity.HasIndex(e => e.ContactEmail, "UQ__companie__8262A9CFF31AAB09").IsUnique();

            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("company_name");
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contact_email");
            entity.Property(e => e.Industry)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("industry");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.RegistrationDate).HasColumnName("registration_date");
        });

        modelBuilder.Entity<CompanyLocation>(entity =>
        {
            entity.HasKey(e => e.CompanyLocationId).HasName("PK__company___2A01372E0CD10577");

            entity.ToTable("company_locations");

            entity.Property(e => e.CompanyLocationId).HasColumnName("company_location_id");
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.Title)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.Zip)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("zip");

            entity.HasOne(d => d.Company).WithMany(p => p.CompanyLocations)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__company_l__compa__44FF419A");

            entity.HasOne(d => d.State).WithMany(p => p.CompanyLocations)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__company_l__state__440B1D61");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__countrie__7E8CD0558407BA81");

            entity.ToTable("countries");

            entity.HasIndex(e => e.CountryName, "UQ__countrie__F70188946CBE88B5").IsUnique();

            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CountryName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("country_name");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__departme__C2232422C91E414C");

            entity.ToTable("departments");

            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("department_name");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PK__jobs__6E32B6A5B87B8F34");

            entity.ToTable("jobs");

            entity.Property(e => e.JobId).HasColumnName("job_id");
            entity.Property(e => e.ClosingDate).HasColumnName("closing_date");
            entity.Property(e => e.CompanyLocationId).HasColumnName("company_location_id");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.JobCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("job_code");
            entity.Property(e => e.PostedDate).HasColumnName("posted_date");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.CompanyLocation).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.CompanyLocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__jobs__company_lo__48CFD27E");

            entity.HasOne(d => d.Department).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__jobs__department__47DBAE45");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("PK__states__81A474179BF875A0");

            entity.ToTable("states");

            entity.HasIndex(e => new { e.StateName, e.CountryId }, "UQ_StateName_CountryID").IsUnique();

            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.StateName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("state_name");

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__states__country___3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
