//using EleterosEB.Domain;
//using Microsoft.EntityFrameworkCore;

//namespace EleterosEB.Data
//{
//    public partial class EleterosEBContext : DbContext
//    {
//        public EleterosEBContext(DbContextOptions<EleterosEBContext> options)
//            : base(options)
//        {
//            //ChangeTracker.LazyLoadingEnabled = false;
//            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
//        }

//        public EleterosEBContext()
//        {
//        }

//        public virtual DbSet<AppointmentType> AppointmentTypes { get; set; }
//        public virtual DbSet<Appointment> Appointments { get; set; }
//        public virtual DbSet<Client> Clients { get; set; }
//        public virtual DbSet<Doctor> Doctors { get; set; }
//        public virtual DbSet<Patient> Patients { get; set; }
//        public virtual DbSet<Product> Products { get; set; }
//        public virtual DbSet<Category> Categories { get; set; }
//        public virtual DbSet<Room> Rooms { get; set; }
//        public virtual DbSet<SurgeryRoomAppointment> SurgeryRoomAppointments { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            //optionsBuilder.UseSqlServer(
//            //    "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = EleterosEBAppData");
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<AppointmentType>(entity =>
//            {
//                entity.HasKey(e => e.AppointmentTypeId);
//            });

//            modelBuilder.Entity<Appointment>(entity =>
//            {
//                entity.HasKey(e => e.AppointmentId);

//                entity.Property(e => e.EndDate).HasColumnType("datetime");

//                entity.Property(e => e.IsPotentiallyConflicting).HasColumnName("isPotentiallyConflicting");

//                entity.Property(e => e.StartDate).HasColumnType("datetime");

//                entity.Property(e => e.Title).HasMaxLength(50);

//                //entity.HasOne(d => d.AppointmentType)
//                //    .WithMany(p => p.Appointments)
//                //    .HasForeignKey(d => d.AppointmentTypeId)
//                //    .OnDelete(DeleteBehavior.ClientSetNull)
//                //    .HasConstraintName("FK_Appointments_AppointmentTypes");

//                //entity.HasOne(d => d.Client)
//                //    .WithMany(p => p.Appointments)
//                //    .HasForeignKey(d => d.ClientId)
//                //    .HasConstraintName("FK_Appointments_Clients");

//                //entity.HasOne(d => d.Doctor)
//                //    .WithMany(p => p.Appointments)
//                //    .HasForeignKey(d => d.DoctorId)
//                //    .HasConstraintName("FK_Appointments_Doctors");

//                //entity.HasOne(d => d.Room)
//                //    .WithMany(p => p.Appointments)
//                //    .HasForeignKey(d => d.RoomId)
//                //    .OnDelete(DeleteBehavior.ClientSetNull)
//                //    .HasConstraintName("FK_Appointments_Rooms");
//            });

//            modelBuilder.Entity<Category>(entity =>
//            {
//                entity.HasKey(e => e.CategoryId);

//                entity.Property(e => e.CategoryName)
//                    .IsRequired()
//                    .HasMaxLength(15)
//                    .IsFixedLength();

//                entity.Property(e => e.Description).HasColumnType("ntext");
//            });

//            modelBuilder.Entity<Client>(entity =>
//            {
//                entity.HasKey(e => e.ClientId);
//            });

//            modelBuilder.Entity<Doctor>(entity =>
//            {
//                entity.HasKey(e => e.DoctorId);
//            });

//            modelBuilder.Entity<Patient>(entity =>
//            {
//                entity.HasKey(e => e.PatientId);

//                entity.HasIndex(e => e.ClientId);

//                entity.Property(e => e.AnimalTypeBreed).HasColumnName("AnimalType_Breed");

//                entity.Property(e => e.AnimalTypeSpecies).HasColumnName("AnimalType_Species");

//                entity.HasOne(d => d.Client)
//                    .WithMany(p => p.Patients)
//                    .HasForeignKey(d => d.ClientId);

//                //entity.HasOne(d => d.Doctor)
//                //    .WithMany(p => p.Patients)
//                //    .HasForeignKey(d => d.DoctorId);
//            });


//            modelBuilder.Entity<Product>(entity =>
//            {
//                entity.HasKey(e => e.ProductId);

//                entity.Property(e => e.ProductName)
//                    .IsRequired()
//                    .HasMaxLength(40);

//                entity.Property(e => e.QuantityPerUnit).HasMaxLength(20);

//                entity.Property(e => e.UnitPrice).HasColumnType("money");

//                //entity.HasOne(d => d.Category)
//                //    .WithMany(p => p.Products)
//                //    .HasForeignKey(d => d.CategoryId)
//                //    .HasConstraintName("FK_Products_Categories");
//            });

//            modelBuilder.Entity<Room>(entity =>
//            {
//                entity.HasKey(e => e.RoomId);
//            });

//            modelBuilder.Entity<SurgeryRoomAppointment>(entity =>
//            {
//                //entity.HasOne(d => d.Doctor)
//                //    .WithMany(p => p.SurgeryRoomBooking)
//                //    .HasForeignKey(d => d.DoctorId)
//                //    .OnDelete(DeleteBehavior.ClientSetNull)
//                //    .HasConstraintName("FK_SurgeryRoomBooking_Doctors");

//                //entity.HasOne(d => d.Room)
//                //    .WithMany(p => p.SurgeryRoomBooking)
//                //    .HasForeignKey(d => d.RoomId)
//                //    .OnDelete(DeleteBehavior.ClientSetNull)
//                //    .HasConstraintName("FK_SurgeryRoomBooking_Rooms");
//            });

//            OnModelCreatingPartial(modelBuilder);
//        }

//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//    }
//}


