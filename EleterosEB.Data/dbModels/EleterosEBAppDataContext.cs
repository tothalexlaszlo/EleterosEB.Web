using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EleterosEB.Data.dbModels
{
    public partial class EleterosEBAppDataContext : DbContext
    {
        public EleterosEBAppDataContext()
        {
        }

        public EleterosEBAppDataContext(DbContextOptions<EleterosEBAppDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppointmentTypes> AppointmentTypes { get; set; }
        public virtual DbSet<Appointments> Appointments { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Doctors> Doctors { get; set; }
        public virtual DbSet<Patients> Patients { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
        public virtual DbSet<SurgeryRoomAppointment> SurgeryRoomAppointment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDb;Database=EleterosEBAppData;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppointmentTypes>(entity =>
            {
                entity.HasKey(e => e.AppointmentTypeId);
            });

            modelBuilder.Entity<Appointments>(entity =>
            {
                entity.HasKey(e => e.AppointmentId);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.IsPotentiallyConflicting).HasColumnName("isPotentiallyConflicting");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.AppointmentType)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.AppointmentTypeId)
                    .HasConstraintName("FK_Appointments_AppointmentTypes");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_Appointments_Clients");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_Appointments_Doctors");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_Appointments_Patients");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsFixedLength();

                entity.Property(e => e.Description).HasColumnType("ntext");
            });

            modelBuilder.Entity<Clients>(entity =>
            {
                entity.HasKey(e => e.ClientId);
            });

            modelBuilder.Entity<Doctors>(entity =>
            {
                entity.HasKey(e => e.DoctorId);
            });

            modelBuilder.Entity<Patients>(entity =>
            {
                entity.HasKey(e => e.PatientId);

                entity.HasIndex(e => e.ClientId);

                entity.Property(e => e.AnimalTypeBreed).HasColumnName("AnimalType_Breed");

                entity.Property(e => e.AnimalTypeSpecies).HasColumnName("AnimalType_Species");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.ClientId);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.QuantityPerUnit).HasMaxLength(20);

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Products_Categories");
            });

            modelBuilder.Entity<Rooms>(entity =>
            {
                entity.HasKey(e => e.RoomId);
            });

            modelBuilder.Entity<SurgeryRoomAppointment>(entity =>
            {
                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.SurgeryRoomAppointment)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurgeryRoomBooking_Doctors");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.SurgeryRoomAppointment)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurgeryRoomBooking_Rooms");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
