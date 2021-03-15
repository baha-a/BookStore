using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Helpers;

namespace Store.Controllers
{
    public class CheckoutsController : Controller
    {
        private readonly BookDbContext _context;

        public CheckoutsController(BookDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bookDbContext = _context.Checkouts.Include(c => c.Book).Include(c => c.User);
            return View(await bookDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var checkout = await _context.Checkouts
                .Include(c => c.Book)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (checkout == null)
                return NotFound();

            return View(checkout);
        }

        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,BookId")] Checkout checkout)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    checkout.CheckedOutDate = DateTime.Now;
                    checkout.DuoDate = DateTime.Now.AddWeeks(3);
                    _context.Add(checkout);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch { }
                ViewData["DuplicateKey"] = true;
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", checkout.BookId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", checkout.UserId);
            return View(checkout);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var checkout = await _context.Checkouts
                .Include(c => c.Book)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (checkout == null)
                return NotFound();

            return View(checkout);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, bool softdelete = true)
        {
            var checkout = await _context.Checkouts.FindAsync(id);
            if (softdelete)
            {
                checkout.Deleted = true;
                _context.Checkouts.Update(checkout);
            }
            else
                _context.Checkouts.Remove(checkout);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckoutExists(int id)
        {
            return _context.Checkouts.Any(e => e.Id == id);
        }
    }
}
