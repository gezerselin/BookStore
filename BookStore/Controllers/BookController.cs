using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private IBookService bookService;

        public BookController(IBookService bookService )
        {
            this.bookService = bookService;

        }
        public IActionResult Index()
        {
            var books = bookService.GetBooks();
            return View(books);
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
