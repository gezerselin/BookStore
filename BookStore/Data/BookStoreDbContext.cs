using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
namespace BookStore.Data
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext()
        {

        }

        public BookStoreDbContext(DbContextOptions<BookStoreDbContext>options) : base(options)
        {

        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(x => x.Books)
                .WithOne(x => x.Author)
                .HasForeignKey(x => x.AuthorId);

            modelBuilder.Entity<Publisher>()
                .HasMany(x => x.Books)
                .WithOne(x => x.Publisher)
                .HasForeignKey(x => x.PublisherId);

            modelBuilder.Entity<BookGenre>()
                .HasKey(x => new { x.BookId, x.GenreId });

            modelBuilder.Entity<BookGenre>()
                 .HasOne(x => x.Book)
                 .WithMany(x => x.Genres)
                 .HasForeignKey(x => x.BookId);

            modelBuilder.Entity<BookGenre>()
                .HasOne(x => x.Genre)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.GenreId);




            base.OnModelCreating(modelBuilder);
        }

       

    }
}
