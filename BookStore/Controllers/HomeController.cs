using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Services;
using BookStore.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IBookService bookService;
        BookStoreDbContext dbContext;
        public HomeController(BookStoreDbContext dbContext,ILogger<HomeController> logger,IBookService bookService)
        {

            _logger = logger;
            this.bookService = bookService;
            this.dbContext = dbContext;
        }

        public IActionResult Index(int page=1, int genreid=0)
        {
            var pageSize = 3;
            

            var books = genreid == 0 ? bookService.GetBooks() : bookService.GetBookByGenreId(genreid);
           

            var pagingBooks = books.OrderBy(p => p.Id)
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize);
                                    
            
            ViewBag.GenreId = genreid;
            var totalBook = books.Count;
            
            var totalPages = Math.Ceiling((decimal)totalBook / pageSize);
            ViewBag.totalPages = totalPages;

            

            return View(pagingBooks);
        }

        public IActionResult Search( string searchItem, int page = 1 )
        
        {
            var pageSize = 3;
            var books = from m in dbContext.Books.Include(x=>x.Author).
                        Include(x=>x.Genre)
                        .Include(x=>x.Publisher)
                        select m;
            if (!String.IsNullOrEmpty(searchItem))
            {
                books =  books.Where(s => s.Name.Contains(searchItem));
            }

            var totalBook = books.Count();
            var totalPages = Math.Ceiling((decimal)totalBook / pageSize);
            ViewBag.totalPages = totalPages;
            books= books.OrderBy(p => p.Id)
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize);

            return View(books.ToList());
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
