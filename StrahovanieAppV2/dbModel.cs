using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace StrahovanieAppV2
{
    public partial class dbModel : DbContext
    {
        public dbModel()
            : base("name=dbModel")
        {
        }

        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<DriverLicense> DriverLicense { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Inspection> Inspection { get; set; }
        public virtual DbSet<InspectionResult> InspectionResult { get; set; }
        public virtual DbSet<Insurer> Insurer { get; set; }
        public virtual DbSet<Passport> Passport { get; set; }
        public virtual DbSet<PolicyTable> PolicyTable { get; set; }
        public virtual DbSet<Position> Position { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .Property(e => e.SpecialMarks)
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .HasMany(e => e.Inspection)
                .WithRequired(e => e.Car)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Car>()
                .HasMany(e => e.PolicyTable)
                .WithRequired(e => e.Car)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.PolicyTable)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DriverLicense>()
                .HasMany(e => e.Client)
                .WithRequired(e => e.DriverLicense1)
                .HasForeignKey(e => e.DriverLicense)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Car)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Inspection)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.PolicyTable)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Insurer>()
                .HasMany(e => e.PolicyTable)
                .WithRequired(e => e.Insurer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Passport>()
                .HasMany(e => e.Client)
                .WithRequired(e => e.Passport1)
                .HasForeignKey(e => e.Passport)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Passport>()
                .HasMany(e => e.Employee)
                .WithRequired(e => e.Passport1)
                .HasForeignKey(e => e.Passport)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Position>()
                .Property(e => e.Salary)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Position>()
                .HasMany(e => e.Employee)
                .WithRequired(e => e.Position)
                .WillCascadeOnDelete(false);
        }
    }
}
