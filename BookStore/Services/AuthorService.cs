using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class AuthorService :IAuthorService
    {
        private BookStoreDbContext dbContext;

        public AuthorService(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int EditAuthor(Author author)
        {
            dbContext.Entry(author).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }

        public IList<Author> GetAuthors()
        {
            return dbContext.Authors.AsNoTracking().ToList();
        }

    
    }
}
