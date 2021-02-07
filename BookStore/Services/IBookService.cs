using BookStore.Models;
using System.Collections.Generic;

namespace BookStore.Services
{
    public interface IBookService
    {
        List<Book> GetBooks();
        List<Book> GetBookByGenreId(int genreId);
    }
}