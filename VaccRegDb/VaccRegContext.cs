using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace VaccRegDb
{
    public partial class VaccRegContext : DbContext
    {
        public VaccRegContext()
        {
        }

        public VaccRegContext(DbContextOptions<VaccRegContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<Vaccination> Vaccinations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("data source=c:\\Users\\eugen\\Unterricht\\Tools\\createWepapiProject\\VaccReg\\VaccRegDb\\vaccinations.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Registration>(entity =>
            {
                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();
            });

            modelBuilder.Entity<Vaccination>(entity =>
            {
                entity.HasIndex(e => e.RegistrationId, "IX_Vaccinations_RegistrationID")
                    .IsUnique();

                entity.Property(e => e.RegistrationId).HasColumnName("RegistrationID");

                entity.Property(e => e.VaccinationDate).IsRequired();

                entity.HasOne(d => d.Registration)
                    .WithOne(p => p.Vaccination)
                    .HasForeignKey<Vaccination>(d => d.RegistrationId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
