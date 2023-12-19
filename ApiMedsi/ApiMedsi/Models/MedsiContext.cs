using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiMedsi.Models
{
    public partial class MedsiContext : DbContext
    {
        public MedsiContext()
        {
        }

        public MedsiContext(DbContextOptions<MedsiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Patient> Patients { get; set; } = null!;
        public virtual DbSet<PatientCard> PatientCards { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;
        public virtual DbSet<Recording> Recordings { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Synopsis> Synopses { get; set; } = null!;
        public virtual DbSet<Token> Tokens { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-OD7QUTU\\MYSERVIS;Initial Catalog=Medsi;Persist Security Info=True;User ID=sa;Password=23082004");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.IdEmployee);

                entity.ToTable("Employee");

                entity.Property(e => e.IdEmployee).HasColumnName("ID_Employee");

                entity.Property(e => e.FirstNameEmp)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("First_Name_Emp");

                entity.Property(e => e.LoginEmp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Login_Emp");


                entity.Property(e => e.MiddleNameEmp)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Middle_Name_Emp")
                    .HasDefaultValueSql("('-')");

                entity.Property(e => e.PasswordEmp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Password_Emp");

                entity.Property(e => e.PositionId).HasColumnName("Position_ID");

                entity.Property(e => e.RoleId).HasColumnName("Role_ID");

                entity.Property(e => e.Salt)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SecondNameEmp)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Second_Name_Emp");

            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.IdPatient);

                entity.ToTable("Patient");

                entity.Property(e => e.IdPatient).HasColumnName("ID_Patient");

                entity.Property(e => e.FirstNamePatient)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("First_Name_Patient");

                entity.Property(e => e.InsurancePolicy)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("Insurance_Policy")
                    .HasDefaultValueSql("('[1-16]')");

                entity.Property(e => e.LoginPatient)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Login_Patient");

                entity.Property(e => e.MiddleNamePatient)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Middle_Name_Patient")
                    .HasDefaultValueSql("('-')");

                entity.Property(e => e.PassportNumber)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("Passport_Number")
                    .HasDefaultValueSql("('[1-10]')");

                entity.Property(e => e.PassportSeries)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("Passport_Series")
                    .HasDefaultValueSql("('[1-10]')");

                entity.Property(e => e.PasswordPatient)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Password_Patient");

                entity.Property(e => e.SecondNamePatient)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Second_Name_Patient");

                entity.Property(e => e.Snils)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("SNILS")
                    .HasDefaultValueSql("('[1-10]')");
            });

            modelBuilder.Entity<PatientCard>(entity =>
            {
                entity.HasKey(e => e.IdPatientCard);

                entity.ToTable("Patient_Card");

                entity.HasIndex(e => e.CardNumberPatient, "UQ_Card_Number")
                    .IsUnique();

                entity.Property(e => e.IdPatientCard).HasColumnName("ID_Patient_Card");

                entity.Property(e => e.CardNumberPatient)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Card_Number_Patient");

                entity.Property(e => e.DateOfCreation)
                    .HasColumnType("date")
                    .HasColumnName("Date_Of_Creation");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");

                entity.Property(e => e.NameDiseases)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Name_Diseases");

                entity.Property(e => e.PatientId).HasColumnName("Patient_ID");

            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.IdPosition);

                entity.ToTable("Position");

                entity.Property(e => e.IdPosition).HasColumnName("ID_Position");

                entity.Property(e => e.NamePosition)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Position");
            });

            modelBuilder.Entity<Recording>(entity =>
            {
                entity.HasKey(e => e.IdRecording);

                entity.ToTable("Recording");

                entity.Property(e => e.IdRecording).HasColumnName("ID_Recording");

                entity.Property(e => e.Direction)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");

                entity.Property(e => e.PatientId).HasColumnName("Patient_ID");

                entity.Property(e => e.RecordDate)
                    .HasColumnType("date")
                    .HasColumnName("Record_Date");

                entity.Property(e => e.RecordTime).HasColumnName("Record_Time");

                entity.Property(e => e.StatusId).HasColumnName("Status_ID");

            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole);

                entity.ToTable("Role");

                entity.Property(e => e.IdRole).HasColumnName("ID_Role");

                entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");

                entity.Property(e => e.NameRole)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Name_Role");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.IdStatus);

                entity.ToTable("Status");

                entity.HasIndex(e => e.RecordStatus, "UQ_Record_Status")
                    .IsUnique();

                entity.Property(e => e.IdStatus).HasColumnName("ID_Status");

                entity.Property(e => e.RecordStatus)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Record_Status");
            });

            modelBuilder.Entity<Synopsis>(entity =>
            {
                entity.HasKey(e => e.IdSynopsis);

                entity.ToTable("Synopsis");

                entity.Property(e => e.IdSynopsis).HasColumnName("ID_Synopsis");

                entity.Property(e => e.NumberOfDays)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Number_Of_Days");

                entity.Property(e => e.PatientCardId).HasColumnName("Patient_Card_ID");

            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.Property(e => e.TokenId).HasColumnName("token_id");

                entity.Property(e => e.Token1)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("token");

                entity.Property(e => e.TokenDatetime)
                    .HasColumnName("token_datetime")
                    .HasDefaultValueSql("(sysdatetime())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
