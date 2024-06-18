using System;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace SQLcon.Models;

public partial class ApplicationDbContext : DbContext , IApplicationDbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BooksRent> BooksRents { get; set; }

    public virtual DbSet<BooksView> BooksViews { get; set; }

    public virtual DbSet<RentalView> RentalViews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersView> UsersViews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=rc1b-b3in3cdba7cdnmev.mdb.yandexcloud.net;database=dbTest;user=user1;password=reunion12", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.35-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.IdBooks).HasName("PRIMARY");

            entity.ToTable(tb => tb.HasComment("Books list"));

            entity.Property(e => e.IdBooks).HasColumnName("idBooks");
            entity.Property(e => e.Author).HasMaxLength(25);
            entity.Property(e => e.InventoryNumber).HasMaxLength(15);
            entity.Property(e => e.ReleaseYear).HasColumnType("year");
            entity.Property(e => e.Title).HasMaxLength(25);
        });

        modelBuilder.Entity<BooksRent>(entity =>
        {
            entity.HasKey(e => e.IdBooksRent).HasName("PRIMARY");

            entity.ToTable("BooksRent", tb => tb.HasComment("List of rented books"));

            entity.HasIndex(e => e.BookId, "Book_idx");

            entity.HasIndex(e => e.UserId, "User_idx");

            entity.Property(e => e.IdBooksRent).HasColumnName("idBooksRent");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Book).WithMany(p => p.BooksRents)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("Book");

            entity.HasOne(d => d.User).WithMany(p => p.BooksRents)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("User");
        });

        modelBuilder.Entity<BooksView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Books_view");

            entity.Property(e => e.Author).HasMaxLength(25);
            entity.Property(e => e.IdBooks).HasColumnName("idBooks");
            entity.Property(e => e.InventoryNumber).HasMaxLength(15);
            entity.Property(e => e.ReleaseYear).HasColumnType("year");
            entity.Property(e => e.Title).HasMaxLength(25);
        });

        modelBuilder.Entity<RentalView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Rental_view");

            entity.Property(e => e.Author).HasMaxLength(25);
            entity.Property(e => e.BookTitle)
                .HasMaxLength(25)
                .HasColumnName("book_title");
            entity.Property(e => e.MidName).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Surname).HasMaxLength(20);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUsers).HasName("PRIMARY");

            entity.ToTable(tb => tb.HasComment("Library users"));

            entity.Property(e => e.IdUsers).HasColumnName("idUsers");
            entity.Property(e => e.Address).HasMaxLength(45);
            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.MidName).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Surname).HasMaxLength(20);
            entity.Property(e => e.YearOfBirth).HasColumnType("year");
        });

        modelBuilder.Entity<UsersView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Users_view");

            entity.Property(e => e.Address).HasMaxLength(45);
            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.IdUsers).HasColumnName("idUsers");
            entity.Property(e => e.MidName).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Surname).HasMaxLength(20);
            entity.Property(e => e.YearOfBirth).HasColumnType("year");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
