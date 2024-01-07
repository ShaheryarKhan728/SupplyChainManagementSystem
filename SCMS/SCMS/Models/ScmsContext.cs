using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SCMS.Models;

public partial class ScmsContext : DbContext
{
    public ScmsContext()
    {
    }

    public ScmsContext(DbContextOptions<ScmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Accoun> Accouns { get; set; }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountType> AccountTypes { get; set; }

    public virtual DbSet<AccountUser> AccountUsers { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CategoryType> CategoryTypes { get; set; }

    public virtual DbSet<ColorAttribute> ColorAttributes { get; set; }

    public virtual DbSet<ImageAttribute> ImageAttributes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }

    public virtual DbSet<ProductBulk> ProductBulks { get; set; }

    public virtual DbSet<SizeAttribute> SizeAttributes { get; set; }

    public virtual DbSet<SizeUnit> SizeUnits { get; set; }

    public virtual DbSet<WeightUnit> WeightUnits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=SCMS;Data Source=DESKTOP-1NVIISU\\MSSQLSERVER01;Encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accoun>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.AccountTypeNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.AccountType)
                .HasConstraintName("FK_Accounts_AccountType");
        });

        modelBuilder.Entity<AccountType>(entity =>
        {
            entity.ToTable("AccountType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AccountTypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AccountUser>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UserAddress).HasMaxLength(50);
            entity.Property(e => e.UserArea).HasMaxLength(50);
            entity.Property(e => e.UserCity).HasMaxLength(50);
            entity.Property(e => e.UserCnic).HasColumnName("UserCNIC");
            entity.Property(e => e.UserEmail).HasMaxLength(50);
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword).HasMaxLength(50);
            entity.Property(e => e.UserRating).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UserReturnRate).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CategoryMargin).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<CategoryType>(entity =>
        {
            entity.ToTable("CategoryType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CategoryTypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<ColorAttribute>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ColorAttribute");

            entity.Property(e => e.ColorName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ImageAttribute>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.ProductId, e.LineItemId });

            entity.ToTable("ImageAttribute");

            entity.Property(e => e.FileName).HasMaxLength(1000);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Order");

            entity.Property(e => e.ApprovedOn).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.OrderAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.ProductDescription).HasMaxLength(500);
            entity.Property(e => e.ProductName).HasMaxLength(50);
            entity.Property(e => e.Warranty)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProductAttribute>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.ProductId, e.LineItemId }).HasName("PK_ProductAttributes_1");

            entity.Property(e => e.ColorName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.SizeName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.WeightName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductAttributes)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductAttributes_Products");
        });

        modelBuilder.Entity<ProductBulk>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.ProductId, e.LineItemId }).HasName("PK_ProductBulk_1");

            entity.ToTable("ProductBulk");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ModifedBy).HasColumnType("datetime");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductBulks)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductBulk_Products");
        });

        modelBuilder.Entity<SizeAttribute>(entity =>
        {
            entity.ToTable("SizeAttribute");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.SizeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SizeUnit>(entity =>
        {
            entity.HasKey(e => e.SizeUnitId).HasName("PK_Colors");

            entity.ToTable("SizeUnit");

            entity.Property(e => e.SizeUnitId).ValueGeneratedNever();
            entity.Property(e => e.SizeUnitName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<WeightUnit>(entity =>
        {
            entity.ToTable("WeightUnit");

            entity.Property(e => e.WeightUnitId).ValueGeneratedNever();
            entity.Property(e => e.WeightUnitName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
