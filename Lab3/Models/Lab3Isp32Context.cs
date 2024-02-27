using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Models;

public partial class Lab3Isp32Context : DbContext
{
    public Lab3Isp32Context()
    {
    }

    public Lab3Isp32Context(DbContextOptions<Lab3Isp32Context> options)
        : base(options)
    {
    }

    public virtual DbSet<PriceList> PriceLists { get; set; }

    public virtual DbSet<Product> products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localDB)\\MSSQLLocalDB;Initial Catalog=Lab3ISP32;Integrated Security=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PriceList>(entity =>
        {
            //entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(450);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products");

            entity.Property(e => e.IdTovar).HasColumnName("id_tovar");
            entity.Property(e => e.ProductCoast).HasColumnType("money");
            entity.Property(e => e.ProductSales).HasColumnType("money");

            entity.HasOne(d => d.IdTovarNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdTovar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_products_PriceLists");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMail");
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
