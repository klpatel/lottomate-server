using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LotoMate.Identity.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace LotoMate.Identity.Infrastructure
{
    public partial class IdentityContext : IdentityDbContext<User, Role, int>
    {
        public IdentityContext()
        {
        }

        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaim> Aspnetroleclaims { get; set; }
        public virtual DbSet<AspNetRole> Aspnetroles { get; set; }
        public virtual DbSet<AspNetUserClaim> Aspnetuserclaims { get; set; }
        public virtual DbSet<AspNetUserLogin> Aspnetuserlogins { get; set; }
        public virtual DbSet<AspNetUserRole> Aspnetuserroles { get; set; }
        public virtual DbSet<AspNetUser> Aspnetusers { get; set; }
        public virtual DbSet<AspNetUserToken> Aspnetusertokens { get; set; }

        #region Client
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<RBAClient> Clients { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<UserClientRole> UserClientRoles { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                //entity.ToTable("AspNetRoles");

                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                //entity.ToTable("AspNetRoleClaims");

                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                //entity.ToTable("AspNetUsers");

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

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                //entity.ToTable("AspNetUserClaims");

                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                //entity.ToTable("AspNetUserLogins");

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                //entity.ToTable("AspNetUserRoles");

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                //entity.ToTable("AspNetUserTokens");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            #region
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
                                
                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_Store_Client");

                
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
            modelBuilder.Entity<UserClientRole>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("UserClientRole", "client");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            #endregion

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
