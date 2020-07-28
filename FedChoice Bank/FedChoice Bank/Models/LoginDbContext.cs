using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FedChoice_Bank.Models
{
    public partial class LoginDbContext : DbContext
    {
        public LoginDbContext()
        {
        }

        public LoginDbContext(DbContextOptions<LoginDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Userstore> Userstore { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
               //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
               
                //optionsBuilder.UseSqlServer("Data Source=DESKTOP-LTBMIDR\\SQLEXPRESS;Initial Catalog=FedChoiceBankDB;Integrated Security=True;");
               
                optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=FedChoiceBankDB;Integrated Security=True;");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Userstore>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("userstore");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Timestamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
