using BookStore.Models;
using BookStore.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class GenreService : IGenreService
    {
        private BookStoreDbContext dbContext;

        public GenreService(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int EditGenre(Genre genre)
        {
            dbContext.Entry(genre).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }

        public IList<Genre> GetGenres()
        {
            return dbContext.Genres.AsNoTracking().ToList();
        }
    }
}
