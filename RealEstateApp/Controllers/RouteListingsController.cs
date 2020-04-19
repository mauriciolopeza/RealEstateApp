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
    public class RouteListingsController : Controller
    {
        private readonly RealEstate_DBContext _context;

        public RouteListingsController(RealEstate_DBContext context)
        {
            _context = context;
        }

        // GET: RouteListings
        public async Task<IActionResult> Index(string searchString)
        {
            var realEstate_DBContext = _context.RouteListings.Include(r => r.FkListings).Include(r => r.FkRoutes);

            if (!String.IsNullOrEmpty(searchString))
            {
                var rs_DBCtx = realEstate_DBContext.Where(x => x.ScheduledTime.ToString().IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1);
                return View(await rs_DBCtx.ToListAsync());
            }

            return View(await realEstate_DBContext.ToListAsync());
        }

        // GET: RouteListings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeListings = await _context.RouteListings
                .Include(r => r.FkListings)
                .Include(r => r.FkRoutes)
                .FirstOrDefaultAsync(m => m.RouteListingsId == id);
            if (routeListings == null)
            {
                return NotFound();
            }

            return PartialView(routeListings);
        }

        // GET: RouteListings/Create
        public IActionResult Create()
        {
            ViewData["FkListingsId"] = new SelectList(_context.Listings, "ListingsId", "ListingNumber");
            ViewData["FkRoutesId"] = new SelectList(_context.Routes, "RoutesId", "RouteName");
            return PartialView();
        }

        // POST: RouteListings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RouteListingsId,FkListingsId,FkRoutesId,ScheduledTime")] RouteListings routeListings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(routeListings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkListingsId"] = new SelectList(_context.Listings, "ListingsId", "ListingNumber", routeListings.FkListingsId);
            ViewData["FkRoutesId"] = new SelectList(_context.Routes, "RoutesId", "RouteName", routeListings.FkRoutesId);
            return PartialView(routeListings);
        }

        // GET: RouteListings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeListings = await _context.RouteListings.FindAsync(id);
            if (routeListings == null)
            {
                return NotFound();
            }
            ViewData["FkListingsId"] = new SelectList(_context.Listings, "ListingsId", "ListingNumber", routeListings.FkListingsId);
            ViewData["FkRoutesId"] = new SelectList(_context.Routes, "RoutesId", "RouteName", routeListings.FkRoutesId);
            return PartialView(routeListings);
        }

        // POST: RouteListings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RouteListingsId,FkListingsId,FkRoutesId,ScheduledTime")] RouteListings routeListings)
        {
            if (id != routeListings.RouteListingsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(routeListings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteListingsExists(routeListings.RouteListingsId))
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
            ViewData["FkListingsId"] = new SelectList(_context.Listings, "ListingsId", "ListingNumber", routeListings.FkListingsId);
            ViewData["FkRoutesId"] = new SelectList(_context.Routes, "RoutesId", "RouteName", routeListings.FkRoutesId);
            return PartialView(routeListings);
        }

        // GET: RouteListings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeListings = await _context.RouteListings
                .Include(r => r.FkListings)
                .Include(r => r.FkRoutes)
                .FirstOrDefaultAsync(m => m.RouteListingsId == id);
            if (routeListings == null)
            {
                return NotFound();
            }

            return PartialView(routeListings);
        }

        // POST: RouteListings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var routeListings = await _context.RouteListings.FindAsync(id);
            _context.RouteListings.Remove(routeListings);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RouteListingsExists(int id)
        {
            return _context.RouteListings.Any(e => e.RouteListingsId == id);
        }
    }
}
