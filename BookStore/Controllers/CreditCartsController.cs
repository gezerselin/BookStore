using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.Controllers
{
    public class CreditCartsController : Controller
    {
        private readonly BookStoreDbContext _context;

        public CreditCartsController(BookStoreDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles="Boss")]
        // GET: CreditCarts
        public async Task<IActionResult> Index()
        {
            var bookStoreDbContext = _context.CreditCarts.Include(c => c.User);
            return View(await bookStoreDbContext.ToListAsync());
        }

        // GET: CreditCarts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditCart = await _context.CreditCarts
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (creditCart == null)
            {
                return NotFound();
            }

            return View(creditCart);
        }

        // GET: CreditCarts/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: CreditCarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,CartNo,Month,Year,CVV,UserId")] CreditCart creditCart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(creditCart);
                await _context.SaveChangesAsync();
                return Redirect("/Cart/PaymentDetail");
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", creditCart.UserId);
            return View(creditCart);
        }

        // GET: CreditCarts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditCart = await _context.CreditCarts.FindAsync(id);
            if (creditCart == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", creditCart.UserId);
            return View(creditCart);
        }

        // POST: CreditCarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,CartNo,Month,Year,CVV,UserId")] CreditCart creditCart)
        {
            if (id != creditCart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(creditCart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreditCartExists(creditCart.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("/Carts/PaymentDetail"); ;
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", creditCart.UserId);
            return View(creditCart);
        }

        // GET: CreditCarts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditCart = await _context.CreditCarts
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (creditCart == null)
            {
                return NotFound();
            }

            return Json(creditCart.CartName);
        }

        // POST: CreditCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var creditCart = await _context.CreditCarts.FindAsync(id);
            _context.CreditCarts.Remove(creditCart);
            await _context.SaveChangesAsync();
            return Json("OK");
        }

        private bool CreditCartExists(int id)
        {
            return _context.CreditCarts.Any(e => e.Id == id);
        }
    }
}
