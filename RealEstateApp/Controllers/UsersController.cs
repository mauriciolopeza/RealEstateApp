using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Models;

namespace RealEstateApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly RealEstate_DBContext _context;

        public UsersController(RealEstate_DBContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index(string searchString)
        {
            var realEstate_DBContext = _context.Users.Include(u => u.FkCompanies);            
           
            if (!String.IsNullOrEmpty(searchString))
            {
                var rs_DBCtx = realEstate_DBContext.Where(x => x.UserName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1);
                return View(await rs_DBCtx.ToListAsync());
            }

            return View(await realEstate_DBContext.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(u => u.FkCompanies)
                .FirstOrDefaultAsync(m => m.UsersId == id);
            if (users == null)
            {
                return NotFound();
            }

            return PartialView(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["FkCompaniesId"] = new SelectList(_context.Companies, "CompaniesId", "CompanyName");
            return PartialView();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsersId,UserName,Password,Names,LastNames,EmailAddress,PhoneNumber,AlternateNumber,FkCompaniesId")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkCompaniesId"] = new SelectList(_context.Companies, "CompaniesId", "CompanyName", users.FkCompaniesId);
            return PartialView(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            ViewData["FkCompaniesId"] = new SelectList(_context.Companies, "CompaniesId", "CompanyName", users.FkCompaniesId);
            return PartialView(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsersId,UserName,Password,Names,LastNames,EmailAddress,PhoneNumber,AlternateNumber,FkCompaniesId")] Users users)
        {
            if (id != users.UsersId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.UsersId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkCompaniesId"] = new SelectList(_context.Companies, "CompaniesId", "CompanyName", users.FkCompaniesId);
            return PartialView(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(u => u.FkCompanies)
                .FirstOrDefaultAsync(m => m.UsersId == id);
            if (users == null)
            {
                return NotFound();
            }

            return PartialView(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UsersId == id);
        }
    }
}
