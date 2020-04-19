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
    public class CompaniesController : Controller
    {
        private readonly RealEstate_DBContext _context;

        public CompaniesController(RealEstate_DBContext context)
        {
            _context = context;
        }

        // GET: Companies
        public async Task<IActionResult> Index(string searchString)
        {
            var realEstate_DBContext = _context.Users;

            if (!String.IsNullOrEmpty(searchString))
            {
                var rs_DBCtx = realEstate_DBContext.Where(x => x.UserName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1);
                return View(await rs_DBCtx.ToListAsync());
            }
            return View(await _context.Companies.ToListAsync());
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companies = await _context.Companies
                .FirstOrDefaultAsync(m => m.CompaniesId == id);
            if (companies == null)
            {
                return NotFound();
            }

            return PartialView(companies);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompaniesId,CompanyName,LogoUrl,PhoneNumber,AlternateNumber,Address,City,Department")] Companies companies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView(companies);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companies = await _context.Companies.FindAsync(id);
            if (companies == null)
            {
                return NotFound();
            }
            return PartialView(companies);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompaniesId,CompanyName,LogoUrl,PhoneNumber,AlternateNumber,Address,City,Department")] Companies companies)
        {
            if (id != companies.CompaniesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompaniesExists(companies.CompaniesId))
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
            return PartialView(companies);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companies = await _context.Companies
                .FirstOrDefaultAsync(m => m.CompaniesId == id);
            if (companies == null)
            {
                return NotFound();
            }

            return PartialView(companies);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companies = await _context.Companies.FindAsync(id);
            _context.Companies.Remove(companies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompaniesExists(int id)
        {
            return _context.Companies.Any(e => e.CompaniesId == id);
        }
    }
}
