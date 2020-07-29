using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FedChoice_Bank.Models
{
    public partial class AccountDbContext : DbContext
    {
        public AccountDbContext()
        {
        }

        public AccountDbContext(DbContextOptions<AccountDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=DESKTOP-LTBMIDR\\SQLEXPRESS;Initial Catalog=FedChoiceBankDB;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.AccountId)
                    .HasColumnName("AccountID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AccountType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Crdate)
                    .HasColumnName("CRDate")
                    .HasColumnType("date");

                entity.Property(e => e.CrlastDate)
                    .HasColumnName("CRLastDate")
                    .HasColumnType("date");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
