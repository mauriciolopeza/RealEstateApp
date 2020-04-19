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
    public class InterestsController : Controller
    {
        private readonly RealEstate_DBContext _context;

        public InterestsController(RealEstate_DBContext context)
        {
            _context = context;
        }

        // GET: Interests
        public async Task<IActionResult> Index(string searchString)
        {
            var realEstate_DBContext = _context.Interests.Include(i => i.FkCompanies);

            if (!String.IsNullOrEmpty(searchString))
            {
                var rs_DBCtx = realEstate_DBContext.Where(x => x.InterestName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1);
                return View(await rs_DBCtx.ToListAsync());
            }

            return View(await realEstate_DBContext.ToListAsync());
        }

        // GET: Interests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interests = await _context.Interests
                .Include(i => i.FkCompanies)
                .FirstOrDefaultAsync(m => m.InterestsId == id);
            if (interests == null)
            {
                return NotFound();
            }

            return PartialView(interests);
        }

        // GET: Interests/Create
        public IActionResult Create()
        {
            ViewData["FkCompaniesId"] = new SelectList(_context.Companies, "CompaniesId", "CompanyName");
            return PartialView();
        }

        // POST: Interests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InterestsId,InterestName,Description,FkCompaniesId")] Interests interests)
        {
            if (ModelState.IsValid)
            {
                _context.Add(interests);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkCompaniesId"] = new SelectList(_context.Companies, "CompaniesId", "CompanyName", interests.FkCompaniesId);
            return PartialView(interests);
        }

        // GET: Interests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interests = await _context.Interests.FindAsync(id);
            if (interests == null)
            {
                return NotFound();
            }
            ViewData["FkCompaniesId"] = new SelectList(_context.Companies, "CompaniesId", "CompanyName", interests.FkCompaniesId);
            return PartialView(interests);
        }

        // POST: Interests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InterestsId,InterestName,Description,FkCompaniesId")] Interests interests)
        {
            if (id != interests.InterestsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(interests);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterestsExists(interests.InterestsId))
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
            ViewData["FkCompaniesId"] = new SelectList(_context.Companies, "CompaniesId", "CompanyName", interests.FkCompaniesId);
            return PartialView(interests);
        }

        // GET: Interests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interests = await _context.Interests
                .Include(i => i.FkCompanies)
                .FirstOrDefaultAsync(m => m.InterestsId == id);
            if (interests == null)
            {
                return NotFound();
            }

            return PartialView(interests);
        }

        // POST: Interests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var interests = await _context.Interests.FindAsync(id);
            _context.Interests.Remove(interests);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InterestsExists(int id)
        {
            return _context.Interests.Any(e => e.InterestsId == id);
        }
    }
}
