using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FedChoice_Bank.Models
{
    public partial class TransactionsDbContext : DbContext
    {
        public TransactionsDbContext()
        {
        }

        public TransactionsDbContext(DbContextOptions<TransactionsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Transactions> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-LTBMIDR\\SQLEXPRESS;Initial Catalog=FedChoiceBankDB;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.AccountType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TargetAccountType)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                
                entity.Property(e => e.Description)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionDate).HasColumnType("date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
