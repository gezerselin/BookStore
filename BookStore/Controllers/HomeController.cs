using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Services;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IBookService bookService;
        public HomeController(ILogger<HomeController> logger,IBookService bookService)
        {

            _logger = logger;
            this.bookService = bookService;
        }

        public IActionResult Index(int page=1, int genreid=0)
        {
            var pageSize = 4;

            var books = genreid == 0 ? bookService.GetBooks() : bookService.GetBookByGenreId(genreid);

            var pagingBooks = books.OrderBy(p => p.Id)
                                    .Skip((page-1)*pageSize)
                                    .Take(pageSize);
            ViewBag.GenreId = genreid;
            var totalBook = books.Count;
            
            var totalPages = Math.Ceiling((decimal)totalBook / pageSize);
            ViewBag.totalPages = totalPages;
            return View(pagingBooks);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
