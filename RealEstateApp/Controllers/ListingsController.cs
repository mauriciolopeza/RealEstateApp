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
    public class ListingsController : Controller
    {
        private readonly RealEstate_DBContext _context;

        public ListingsController(RealEstate_DBContext context)
        {
            _context = context;
        }

        // GET: Listings
        public async Task<IActionResult> Index(string searchString)
        {
            var realEstate_DBContext = _context.Listings.Include(l => l.FkCompanies).Include(l => l.FkContacts);

            if (!String.IsNullOrEmpty(searchString))
            {
                var rs_DBCtx = realEstate_DBContext.Where(x => x.ListingNumber.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1);
                return View(await rs_DBCtx.ToListAsync());
            }

            return View(await realEstate_DBContext.ToListAsync());
        }

        // GET: Listings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listings = await _context.Listings
                .Include(l => l.FkCompanies)
                .Include(l => l.FkContacts)
                .FirstOrDefaultAsync(m => m.ListingsId == id);
            if (listings == null)
            {
                return NotFound();
            }

            return PartialView(listings);
        }

        // GET: Listings/Create
        public IActionResult Create()
        {
            ViewData["FkCompaniesId"] = new SelectList(_context.Companies, "CompaniesId", "CompanyName");
            ViewData["FkContactsId"] = new SelectList(_context.Contacts, "ContactsId", "ContactLastNames");
            return PartialView();
        }

        // POST: Listings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ListingsId,ListingNumber,SmlsNumber,TuNuevoEspacioNumber,IsSmlsPromoted,IsTuNuevoEspacioPromoted,ListingType,Address,City,Department,BloqueNumber,UnitNumber,UrbanizationName,Neighborhood,Estrato,SalesPrice,AdministrationFee,ParkingSpaces,HasOpenKitchen,HasElevator,IsClosedUrbanization,FkCompaniesId,HasBalcony,HasMaidRoom,IsStudio,HasPool,YearBuilt,FkContactsId,AreaSize,NumberOfRooms,NumberOfBathrooms")] Listings listings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkCompaniesId"] = new SelectList(_context.Companies, "CompaniesId", "CompanyName", listings.FkCompaniesId);
            ViewData["FkContactsId"] = new SelectList(_context.Contacts, "ContactsId", "ContactLastNames", listings.FkContactsId);
            return PartialView(listings);
        }

        // GET: Listings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listings = await _context.Listings.FindAsync(id);
            if (listings == null)
            {
                return NotFound();
            }
            ViewData["FkCompaniesId"] = new SelectList(_context.Companies, "CompaniesId", "CompanyName", listings.FkCompaniesId);
            ViewData["FkContactsId"] = new SelectList(_context.Contacts, "ContactsId", "ContactLastNames", listings.FkContactsId);
            return PartialView(listings);
        }

        // POST: Listings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ListingsId,ListingNumber,SmlsNumber,TuNuevoEspacioNumber,IsSmlsPromoted,IsTuNuevoEspacioPromoted,ListingType,Address,City,Department,BloqueNumber,UnitNumber,UrbanizationName,Neighborhood,Estrato,SalesPrice,AdministrationFee,ParkingSpaces,HasOpenKitchen,HasElevator,IsClosedUrbanization,FkCompaniesId,HasBalcony,HasMaidRoom,IsStudio,HasPool,YearBuilt,FkContactsId,AreaSize,NumberOfRooms,NumberOfBathrooms")] Listings listings)
        {
            if (id != listings.ListingsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListingsExists(listings.ListingsId))
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
            ViewData["FkCompaniesId"] = new SelectList(_context.Companies, "CompaniesId", "CompanyName", listings.FkCompaniesId);
            ViewData["FkContactsId"] = new SelectList(_context.Contacts, "ContactsId", "ContactLastNames", listings.FkContactsId);
            return PartialView(listings);
        }

        // GET: Listings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listings = await _context.Listings
                .Include(l => l.FkCompanies)
                .Include(l => l.FkContacts)
                .FirstOrDefaultAsync(m => m.ListingsId == id);
            if (listings == null)
            {
                return NotFound();
            }

            return PartialView(listings);
        }

        // POST: Listings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listings = await _context.Listings.FindAsync(id);
            _context.Listings.Remove(listings);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListingsExists(int id)
        {
            return _context.Listings.Any(e => e.ListingsId == id);
        }
    }
}
