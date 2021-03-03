using BookStore.Models;
using System.Collections.Generic;

namespace BookStore.Services
{
    public interface IBookService
    {
        List<Book> GetBooks();
        List<Book> GetBookByGenreId(int genreId);
        void AddBook(Book book);
        Book GetBookById(int id);
        int EditBook(Book book);
        void DeleteBook(Book deletingBook);
        void addComment(Comment comment);
    }
}