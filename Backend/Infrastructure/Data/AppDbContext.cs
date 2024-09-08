using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Device
            modelBuilder.Entity<Device>()
                .Property(d => d.Name)
                .IsRequired();

            modelBuilder.Entity<Device>()
                .HasMany(d => d.PatientMeasures)
                .WithOne(p => p.Device)
                .HasForeignKey(p => p.DeviceId);

            //PatientMeasure
            modelBuilder.Entity<PatientMeasure>()
                .Property(p => p.Name)
                .IsRequired();

            modelBuilder.Entity<PatientMeasure>()
                .Property(p => p.Embg)
                .IsRequired();

            modelBuilder.Entity<PatientMeasure>()
                .Property(p => p.MinThreshold)
                .IsRequired();

            modelBuilder.Entity<PatientMeasure>()
                .Property(p => p.MaxThreshold)
                .IsRequired();

            modelBuilder.Entity<PatientMeasure>()
                .HasMany(p => p.DataReadings)
                .WithOne(d => d.PatientMeasure)
                .HasForeignKey(d => d.PatientMeasureId);

            modelBuilder.Entity<PatientMeasure>()
                .HasMany(p => p.Warnings)
                .WithOne(w => w.PatientMeasure)
                .HasForeignKey(w => w.PatientMeasureId);

            //DataReading
            modelBuilder.Entity<DataReading>()
                .Property(dr => dr.Value)
                .IsRequired();

            modelBuilder.Entity<DataReading>()
                .Property(dr => dr.DateTime)
                .IsRequired();

            modelBuilder.Entity<DataReading>()
                .HasOne(dr => dr.PatientMeasure)
                .WithMany(p => p.DataReadings)
                .HasForeignKey(dr => dr.PatientMeasureId)
                .IsRequired();

            //Warning
            modelBuilder.Entity<Warning>()
                .Property(w => w.DateTime)
                .IsRequired();

            modelBuilder.Entity<Warning>()
                .Property(w => w.Value)
                .IsRequired();

            modelBuilder.Entity<Warning>()
                .Property(w => w.CurrentMinThreshold)
                .IsRequired();

            modelBuilder.Entity<Warning>()
                .Property(w => w.CurrentMaxThreshold)
                .IsRequired();

            modelBuilder.Entity<Warning>()
                .HasOne(w => w.PatientMeasure)
                .WithMany(p => p.Warnings)
                .HasForeignKey(w => w.PatientMeasureId)
                .IsRequired();

        }
        public DbSet<Device> Devices { get; set; }
        public DbSet<PatientMeasure> PatientMeasures { get; set; }
        public DbSet<DataReading> DataReadings { get; set; }
        public DbSet<Warning> Warnings { get; set; }
    }
}
