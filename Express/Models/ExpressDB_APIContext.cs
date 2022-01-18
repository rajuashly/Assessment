using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Express.Models
{
    public partial class ExpressDB_APIContext : DbContext
    {
        public ExpressDB_APIContext()
        {
        }

        public ExpressDB_APIContext(DbContextOptions<ExpressDB_APIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<WayBill> WayBill { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=ASHLY\\SQLExpress;Database=ExpressDB;Trusted_Connection=True;");
//            }
//        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

        //    modelBuilder.Entity<Branch>(entity =>
        //    {
        //        entity.ToTable("Branch");

        //        entity.Property(e => e.Address).HasMaxLength(1024);

        //        entity.Property(e => e.Country)
        //            .IsRequired()
        //            .HasMaxLength(255);

        //        entity.Property(e => e.Name)
        //            .IsRequired()
        //            .HasMaxLength(255);
        //    });

        //    modelBuilder.Entity<User>(entity =>
        //    {
        //        entity.Property(e => e.FirstName).HasMaxLength(255);

        //        entity.Property(e => e.LastName).HasMaxLength(255);

        //        entity.Property(e => e.PasswordHash).HasMaxLength(255);

        //        entity.Property(e => e.Username).HasMaxLength(255);
        //    });

        //    modelBuilder.Entity<Vehicle>(entity =>
        //    {
        //        entity.ToTable("Vehicle");

        //        entity.Property(e => e.Color)
        //            .IsRequired()
        //            .HasMaxLength(255);

        //        entity.Property(e => e.Make)
        //            .IsRequired()
        //            .HasMaxLength(255);

        //        entity.Property(e => e.Model)
        //            .IsRequired()
        //            .HasMaxLength(255);

        //        entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

        //        entity.Property(e => e.Registration)
        //            .IsRequired()
        //            .HasMaxLength(255);

        //        entity.Property(e => e.Vinumber)
        //            .IsRequired()
        //            .HasColumnName("VINumber");

        //        entity.HasOne(d => d.Branch)
        //            .WithMany(p => p.Vehicles)
        //            .HasForeignKey(d => d.BranchId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK__Vehicle__BranchI__286302EC");
        //    });

        //    modelBuilder.Entity<WayBill>(entity =>
        //    {
        //        entity.ToTable("WayBill");

        //        entity.Property(e => e.ContentDescription).HasMaxLength(4000);

        //        entity.Property(e => e.CreatedDate).HasColumnType("datetime");

        //        entity.Property(e => e.Destination)
        //            .IsRequired()
        //            .HasMaxLength(4000);

        //        entity.Property(e => e.Reference)
        //            .IsRequired()
        //            .HasMaxLength(255);

        //        entity.Property(e => e.TotalWeight).HasColumnType("decimal(10, 2)");

        //        entity.Property(e => e.VehicleStartsFrom)
        //            .IsRequired()
        //            .HasMaxLength(4000);

        //        entity.HasOne(d => d.AssignedToVehicle)
        //            .WithMany(p => p.WayBills)
        //            .HasForeignKey(d => d.AssignedToVehicleId)
        //            .HasConstraintName("FK__WayBill__Assigne__2B3F6F97");

        //        entity.HasOne(d => d.CreatedBy)
        //            .WithMany(p => p.WayBills)
        //            .HasForeignKey(d => d.CreatedById)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK__WayBill__Created__2C3393D0");
        //    });

        //    OnModelCreatingPartial(modelBuilder);
        //}

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
