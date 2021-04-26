using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AdmissionsWebsite
{
    public partial class StudentContext : DbContext
    {
        public StudentContext()
            : base("name=StudentContext")
        {
        }

        public virtual DbSet<AdmissionDecision> AdmissionDecisions { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Major> Majors { get; set; }
        public virtual DbSet<Semester> Semesters { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdmissionDecision>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.AdmissionDecision)
                .HasForeignKey(e => e.AdmissionDecisionId);

            modelBuilder.Entity<Gender>()
                .Property(e => e.GenderType)
                .IsFixedLength();

            modelBuilder.Entity<Semester>()
                .Property(e => e.SemesterName)
                .IsFixedLength();

            modelBuilder.Entity<Semester>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.Semester)
                .HasForeignKey(e => e.EnrollmentSemesterId);

            modelBuilder.Entity<Student>()
                .Property(e => e.ZipCode)
                .IsFixedLength();

            modelBuilder.Entity<Student>()
                .Property(e => e.CurrentGPA)
                .HasPrecision(2, 1);

            modelBuilder.Entity<Student>()
                .Property(e => e.EnrollmentYear)
                .IsFixedLength();
        }
    }
}
