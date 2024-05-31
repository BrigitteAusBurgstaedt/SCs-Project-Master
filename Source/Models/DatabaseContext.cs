using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace SCsProjectMaster.Source.Models;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<ExternalUri> ExternalUris { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Stopwatch> Stopwatches { get; set; }

    public virtual DbSet<TimeStopped> TimeStoppeds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("data source=wdb2.hs-mittweida.de;initial catalog=jpfeifer;user id=jpfeifer;password=Naen^oyee9ah", ServerVersion.Parse("10.3.39-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("address");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.AddressLine1)
                .HasMaxLength(32)
                .HasColumnName("address_line_1");
            entity.Property(e => e.AddressLine2)
                .HasMaxLength(32)
                .HasColumnName("address_line_2");
            entity.Property(e => e.AddressLine3)
                .HasMaxLength(32)
                .HasColumnName("address_line_3");
            entity.Property(e => e.City)
                .HasMaxLength(64)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(64)
                .HasDefaultValueSql("'Germany'")
                .HasColumnName("country");
            entity.Property(e => e.HouseNumber)
                .HasMaxLength(8)
                .HasColumnName("house_number");
            entity.Property(e => e.Street)
                .HasMaxLength(32)
                .HasColumnName("street");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("customer");

            entity.HasIndex(e => e.AddressId, "fk_customer_address");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.AddressId)
                .HasColumnType("int(11)")
                .HasColumnName("address_id");
            entity.Property(e => e.EMailAddress)
                .HasMaxLength(320)
                .HasColumnName("e_mail_address");
            entity.Property(e => e.Name)
                .HasMaxLength(512)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(16)
                .HasColumnName("phone_number");
            entity.Property(e => e.Priority)
                .HasColumnType("int(11)")
                .HasColumnName("priority");
            entity.Property(e => e.WebsiteUri)
                .HasMaxLength(2083)
                .HasColumnName("website_uri");

            entity.HasOne(d => d.Address).WithMany(p => p.Customers)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_customer_address");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Login).HasName("PRIMARY");

            entity.ToTable("employee");

            entity.HasIndex(e => e.AddressId, "fk_employee_address");

            entity.Property(e => e.Login)
                .HasMaxLength(32)
                .HasColumnName("login");
            entity.Property(e => e.AddressId)
                .HasColumnType("int(11)")
                .HasColumnName("address_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(128)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(512)
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(64)
                .IsFixedLength()
                .HasColumnName("password_hash");
            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("password_salt");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(16)
                .HasColumnName("phone_number");

            entity.HasOne(d => d.Address).WithMany(p => p.Employees)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_employee_address");
        });

        modelBuilder.Entity<ExternalUri>(entity =>
        {
            entity.HasKey(e => new { e.ProjectId, e.Id })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("external_uri");

            entity.Property(e => e.ProjectId)
                .HasColumnType("int(11)")
                .HasColumnName("project_id");
            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Uri)
                .HasMaxLength(2083)
                .HasColumnName("uri");

            entity.HasOne(d => d.Project).WithMany(p => p.ExternalUris)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_external_uri_project");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Number).HasName("PRIMARY");

            entity.ToTable("invoice");

            entity.Property(e => e.Number)
                .HasMaxLength(8)
                .HasColumnName("number");
            entity.Property(e => e.CreationDate).HasColumnName("creation_date");
        });

        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.HasKey(e => new { e.InvoiceNumber, e.Position })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("invoice_item");

            entity.Property(e => e.InvoiceNumber)
                .HasMaxLength(8)
                .HasColumnName("invoice_number");
            entity.Property(e => e.Position)
                .HasColumnType("int(11)")
                .HasColumnName("position");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(12, 2)
                .HasColumnName("unit_price");
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasColumnName("name");
            entity.Property(e => e.Units)
                .HasPrecision(20, 10)
                .HasDefaultValueSql("'1.0000000000'")
                .HasColumnName("units");

            entity.HasOne(d => d.InvoiceNumberNavigation).WithMany(p => p.InvoiceItems)
                .HasForeignKey(d => d.InvoiceNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_invoice_item_invoice");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("project");

            entity.HasIndex(e => e.CustomerId, "fk_project_customer");

            entity.HasIndex(e => e.ContactPerson, "fk_project_employee");

            entity.HasIndex(e => e.InvoiceNumber, "invoice_number").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Categorie)
                .HasMaxLength(256)
                .HasColumnName("categorie");
            entity.Property(e => e.CloudPreviewFolderUri)
                .HasMaxLength(2083)
                .HasColumnName("cloud_preview_folder_uri");
            entity.Property(e => e.CloudUri)
                .HasMaxLength(2083)
                .HasColumnName("cloud_uri");
            entity.Property(e => e.CloudVideoUri)
                .HasMaxLength(2083)
                .HasColumnName("cloud_video_uri");
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(32)
                .HasColumnName("contact_person");
            entity.Property(e => e.CustomerId)
                .HasColumnType("int(11)")
                .HasColumnName("customer_id");
            entity.Property(e => e.Deadline).HasColumnName("deadline");
            entity.Property(e => e.DokumentsFolderUri)
                .HasMaxLength(2083)
                .HasColumnName("dokuments_folder_uri");
            entity.Property(e => e.InvoiceNumber)
                .HasMaxLength(8)
                .HasColumnName("invoice_number");
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasColumnName("name");
            entity.Property(e => e.OtherInformation)
                .HasColumnType("text")
                .HasColumnName("other_information");
            entity.Property(e => e.PreviewUri)
                .HasMaxLength(2083)
                .HasColumnName("preview_uri");
            entity.Property(e => e.ProjectFileName)
                .HasMaxLength(256)
                .HasDefaultValueSql("'.project_data.json'")
                .HasColumnName("project_file_name");
            entity.Property(e => e.ShowcaseFolderUri)
                .HasMaxLength(2083)
                .HasColumnName("showcase_folder_uri");
            entity.Property(e => e.ShowcasePictureUri)
                .HasMaxLength(2083)
                .HasColumnName("showcase_picture_uri");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.TextInfo1)
                .HasMaxLength(256)
                .HasColumnName("text_info_1");
            entity.Property(e => e.TextInfo2)
                .HasMaxLength(256)
                .HasColumnName("text_info_2");
            entity.Property(e => e.TotalWorkload)
                .HasColumnType("int(11)")
                .HasColumnName("total_workload");
            entity.Property(e => e.Type)
                .HasMaxLength(64)
                .HasColumnName("type");

            entity.HasOne(d => d.ContactPersonNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ContactPerson)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_project_employee");

            entity.HasOne(d => d.Customer).WithMany(p => p.Projects)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_project_customer");

            entity.HasOne(d => d.InvoiceNumberNavigation).WithOne(p => p.Project)
                .HasForeignKey<Project>(d => d.InvoiceNumber)
                .HasConstraintName("fk_project_invoice");

            entity.HasMany(d => d.EmployeeLogins).WithMany(p => p.ProjectsNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "ProjectTeam",
                    r => r.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmployeeLogin")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_project_team_employee"),
                    l => l.HasOne<Project>().WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_project_team_project"),
                    j =>
                    {
                        j.HasKey("ProjectId", "EmployeeLogin")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("project_team");
                        j.HasIndex(new[] { "EmployeeLogin" }, "fk_project_team_employee");
                        j.IndexerProperty<int>("ProjectId")
                            .HasColumnType("int(11)")
                            .HasColumnName("project_id");
                        j.IndexerProperty<string>("EmployeeLogin")
                            .HasMaxLength(32)
                            .HasColumnName("employee_login");
                    });
        });

        modelBuilder.Entity<Stopwatch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("stopwatch");

            entity.HasIndex(e => e.ProjectId, "fk_stopwatch_project");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasColumnName("name");
            entity.Property(e => e.ProjectId)
                .HasColumnType("int(11)")
                .HasColumnName("project_id");

            entity.HasOne(d => d.Project).WithMany(p => p.Stopwatches)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stopwatch_project");
        });

        modelBuilder.Entity<TimeStopped>(entity =>
        {
            entity.HasKey(e => new { e.StopwatchId, e.EmployeeLogin })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("time_stopped");

            entity.HasIndex(e => e.EmployeeLogin, "fk_time_stopped_employee");

            entity.Property(e => e.StopwatchId)
                .HasColumnType("int(11)")
                .HasColumnName("stopwatch_id");
            entity.Property(e => e.EmployeeLogin)
                .HasMaxLength(32)
                .HasColumnName("employee_login");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");

            entity.HasOne(d => d.EmployeeLoginNavigation).WithMany(p => p.TimeStoppeds)
                .HasForeignKey(d => d.EmployeeLogin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_time_stopped_employee");

            entity.HasOne(d => d.Stopwatch).WithMany(p => p.TimeStoppeds)
                .HasForeignKey(d => d.StopwatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_time_stopped_stopwatch");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
