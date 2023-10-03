using System;
using System.Collections.Generic;
using InventoryManagementSystem.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Database;

public partial class FiberspaceContext : IdentityDbContext<IdentityUser>
{
    public FiberspaceContext()
    {
    }

    public FiberspaceContext(DbContextOptions<FiberspaceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<InventoryItem> InventoryItems { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Location> Locations { get; set; }
    public virtual DbSet<CycleHistory> CycleHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=tcp:fiberspace.database.windows.net;Initial Catalog=fiberspace;Persist Security Info=False;User ID=fiber;Password=projectgroup6!;MultipleActiveResultSets=False;Encrypt=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<CycleHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CycleHis__4162D133E26074B3");
            entity.ToTable("CycleHistory");
            entity.Property(e => e.Id)
                .HasColumnName("CycleHistoryId")
                .ValueGeneratedOnAdd();




        });
        modelBuilder.Entity<InventoryItem>(entity =>
        {
            entity.HasKey(e => e.InventoryItemId).HasName("PK__Inventor__3BB2AC80BFD08667");

            entity.ToTable("InventoryItem");

            entity.Property(e => e.InventoryItemId).ValueGeneratedNever();
            entity.Property(e => e.ItemMasterDescription)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ItemSerialNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ItemStatusCode)
                .HasMaxLength(2)
                .IsFixedLength();
            entity.Property(e => e.ItemType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LocationName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PrimaryQuantity).HasColumnType("decimal(10, 4)");
            entity.Property(e => e.PrimaryUom)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PrimaryUOM");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__Location__E7FEA497518AB304");
            entity.ToTable("Location");
            entity.Property(e => e.LocationId)
                .HasColumnName("LocationId")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.LocationName)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.Active);

            entity.Property(e => e.ItemType)
                .IsRequired()
                .HasMaxLength(20);
              
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
