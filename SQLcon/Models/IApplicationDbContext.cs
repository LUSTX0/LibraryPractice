using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace SQLcon.Models;

public interface IApplicationDbContext 
{    
    //void OnConfiguring(DbContextOptionsBuilder optionsBuilder);

    //void OnModelCreating(ModelBuilder modelBuilder);

    //void OnModelCreatingPartial(ModelBuilder modelBuilder);

     DbSet<Book> Books { get; set; }

     DbSet<BooksRent> BooksRents { get; set; }

    DbSet<BooksView> BooksViews { get; set; }

    DbSet<RentalView> RentalViews { get; set; }

    DbSet<User> Users { get; set; }

    DbSet<UsersView> UsersViews { get; set; }
}
