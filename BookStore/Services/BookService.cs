using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class BookService : IBookService
    {
        private BookStoreDbContext dbContext;

        public BookService(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddBook(Book book)
        {
            dbContext.Books.Add(book);
            dbContext.SaveChanges();
        }

        public void addComment(Comment comment)
        {
            dbContext.Comments.Add(comment);
            dbContext.SaveChanges();

        }

        public void DeleteBook(Book deletingBook)
        {
            dbContext.Books.Remove(deletingBook);
            dbContext.SaveChanges();

        }

        public int EditBook(Book book)
        {
            dbContext.Entry(book).State = EntityState.Modified;
            return dbContext.SaveChanges();
     }

        public List<Book> GetBookByGenreId(int genreId)
        {
            return dbContext.Books.AsNoTracking().Include(x => x.Author).Include(x => x.Publisher).Include(x => x.Genre).Where(b => b.GenreId == genreId).ToList();

        }

        public Book GetBookById(int id)
        {
            return dbContext.Books.Find(id);

        }

        public List<Book> GetBooks()
        {
            var books = dbContext.Books.AsNoTracking().Include(x=>x.Author).Include(x=>x.Publisher).Include(x=>x.Genre).ToList();
            return books;
            
        }
    }
}
