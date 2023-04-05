using System;
using LotoMate.Client.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LotoMate.Client.Infrastructure
{
    public partial class LottoClientDbContext : DbContext
    {
        public LottoClientDbContext()
        {
        }

        public LottoClientDbContext(DbContextOptions<LottoClientDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BusinessCategory> BusinessCategories { get; set; }
        public virtual DbSet<BusinessType> BusinessTypes { get; set; }
        public virtual DbSet<RBAClient> Clients { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<UserClientRole> UserClientRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BusinessCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("BusinessCategory", "client");

                entity.Property(e => e.BusinessCategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);
            });

            modelBuilder.Entity<BusinessType>(entity =>
            {
                entity.ToTable("BusinessType", "client");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.BusinessTypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);
            });

            modelBuilder.Entity<RBAClient>(entity =>
            {
                entity.ToTable("Client", "client");

                entity.Property(e => e.ClientFname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ClientFName");

                entity.Property(e => e.ClientLname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ClientLName");

                entity.Property(e => e.ClientMname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ClientMName");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_Client_Address");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_Client_Contact");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store", "client");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegisteredName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StoreName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StoreNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tinno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TINNo");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_Store_Address");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_Store_Contact");
            });

            modelBuilder.Entity<UserClientRole>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("UserClientRole", "client");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address", "client");

                entity.Property(e => e.Addr1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Addr2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contact", "client");

                entity.Property(e => e.CellPhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HomePhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OfficePhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
