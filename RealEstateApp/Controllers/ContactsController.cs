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
    public class ContactsController : Controller
    {
        private readonly RealEstate_DBContext _context;

        public ContactsController(RealEstate_DBContext context)
        {
            _context = context;
        }

        // GET: Contacts
        public async Task<IActionResult> Index(string searchString)
        {
            var realEstate_DBContext = _context.Contacts.Include(c => c.FkCompanies);

            if (!String.IsNullOrEmpty(searchString))
            {
                var rs_DBCtx = realEstate_DBContext.Where(x => x.ContactNames.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1);
                return View(await rs_DBCtx.ToListAsync());
            }

            return View(await realEstate_DBContext.ToListAsync());
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacts = await _context.Contacts
                .Include(c => c.FkCompanies)
                .FirstOrDefaultAsync(m => m.ContactsId == id);
            if (contacts == null)
            {
                return NotFound();
            }

            return PartialView(contacts);
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            ViewData["FkCompaniesId"] = new SelectList(_context.Companies, "CompaniesId", "CompanyName");
            return PartialView();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactsId,ContactNames,ContactLastNames,IdNumber,PhoneNumber,AlternateNumber,EmailAddress,ContactType,FkCompaniesId")] Contacts contacts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contacts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkCompaniesId"] = new SelectList(_context.Companies, "CompaniesId", "CompanyName", contacts.FkCompaniesId);
            return PartialView(contacts);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacts = await _context.Contacts.FindAsync(id);
            if (contacts == null)
            {
                return NotFound();
            }
            ViewData["FkCompaniesId"] = new SelectList(_context.Companies, "CompaniesId", "CompanyName", contacts.FkCompaniesId);
            return PartialView(contacts);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactsId,ContactNames,ContactLastNames,IdNumber,PhoneNumber,AlternateNumber,EmailAddress,ContactType,FkCompaniesId")] Contacts contacts)
        {
            if (id != contacts.ContactsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contacts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactsExists(contacts.ContactsId))
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
            ViewData["FkCompaniesId"] = new SelectList(_context.Companies, "CompaniesId", "CompanyName", contacts.FkCompaniesId);
            return PartialView(contacts);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacts = await _context.Contacts
                .Include(c => c.FkCompanies)
                .FirstOrDefaultAsync(m => m.ContactsId == id);
            if (contacts == null)
            {
                return NotFound();
            }

            return PartialView(contacts);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contacts = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(contacts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactsExists(int id)
        {
            return _context.Contacts.Any(e => e.ContactsId == id);
        }
    }
}
