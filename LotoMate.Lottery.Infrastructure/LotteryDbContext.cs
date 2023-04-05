using LotoMate.Lottery.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace LotoMate.Lottery.Infrastructure
{
    public partial class LotteryDbContext : DbContext
    {
        public LotteryDbContext()
        {
        }

        public LotteryDbContext(DbContextOptions<LotteryDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<CategorisedSale> CategorisedSales { get; set; }
        public virtual DbSet<DailySummary> DailySummaries { get; set; }
        public virtual DbSet<GameSalesCategory> GameSalesCategories { get; set; }
        public virtual DbSet<InstanceDailySale> InstanceDailySales { get; set; }
        public virtual DbSet<InstanceGameBook> InstanceGameBooks { get; set; }
        public virtual DbSet<InstanceGameMaster> InstanceGameMasters { get; set; }

        public virtual DbSet<Store> Stores { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<CategorisedSale>(entity =>
            {
                entity.ToTable("CategorisedSales", "lotto");

                //entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.HasOne(d => d.GameSalesCategory)
                    .WithMany(p => p.CategorisedSales)
                    .HasForeignKey(d => d.GameSalesCategoryId)
                    .HasConstraintName("FK_CategorisedSales_GameSalesCategory1");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.CategorisedSales)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_CategorisedSales_GameSalesCategory");
            });


            modelBuilder.Entity<DailySummary>(entity =>
            {
                entity.ToTable("DailySummary", "lotto");

                entity.Property(e => e.Cancel).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Difference).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InHand).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Paid).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Sale).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.DailySummaries)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_DailySummary_Store");
            });

            modelBuilder.Entity<GameSalesCategory>(entity =>
            {
                entity.ToTable("GameSalesCategory", "lotto");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.GameSalesCategories)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_GameSalesCategory_Store");
            });

            modelBuilder.Entity<InstanceDailySale>(entity =>
            {
                entity.ToTable("InstanceDailySales", "lotto");

                entity.Property(e => e.ClosedUserId).HasColumnName("ClosedUserID");

                entity.Property(e => e.OpenUserId).HasColumnName("OpenUserID");

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.HasOne(d => d.ClosedUser)
                    .WithMany(p => p.InstanceDailySaleClosedUsers)
                    .HasForeignKey(d => d.ClosedUserId)
                    .HasConstraintName("FK_InstanceDailySales_AspNetUsers1");

                entity.HasOne(d => d.OpenUser)
                    .WithMany(p => p.InstanceDailySaleOpenUsers)
                    .HasForeignKey(d => d.OpenUserId)
                    .HasConstraintName("FK_InstanceDailySales_AspNetUsers");

                entity.HasOne(d => d.InstanceGameBook)
                    .WithMany(p => p.InstanceDailySales)
                    .HasForeignKey(d => d.InstanceGameBookId)
                    .HasConstraintName("FK_InstanceDailySales_InstanceGameBook");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.InstanceDailySales)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_InstanceDailySales_Store");
            });


            modelBuilder.Entity<InstanceGameBook>(entity =>
            {
                entity.ToTable("InstanceGameBook", "lotto");

                //entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ActivateDate).HasColumnType("datetime");

                entity.Property(e => e.BookNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);


                entity.Property(e => e.DisplayNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SettleDate).HasColumnType("datetime");

                entity.HasOne(d => d.InstanceGame)
                    .WithMany(p => p.InstanceGameBooks)
                    .HasForeignKey(d => d.InstanceGameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InstanceGameId_InstanceGameMasterId");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.InstanceGameBooks)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_InstanceGameBook_Store");
            });

            modelBuilder.Entity<InstanceGameMaster>(entity =>
            {
                entity.ToTable("InstanceGameMaster", "lotto");

                entity.Property(e => e.GameNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.InstanceGameMasters)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_InstanceGameMaster_Store");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store", "client");

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
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
