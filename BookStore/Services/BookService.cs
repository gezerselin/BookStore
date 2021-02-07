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

        public List<Book> GetBookByGenreId(int genreId)
        {
            return  dbContext.Books.AsNoTracking().Include(b => b.Genres)
                .ThenInclude(bg => bg.Genre).Where(b => b.Genres.Any(bg => bg.GenreId == genreId)).ToList(); 
        }

        public List<Book> GetBooks()
        {
            var books = dbContext.Books.AsNoTracking().ToList();
            return books;
            
        }
    }
}
