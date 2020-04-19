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
    public class RoutesController : Controller
    {
        private readonly RealEstate_DBContext _context;

        public RoutesController(RealEstate_DBContext context)
        {
            _context = context;
        }

        // GET: Routes
        public async Task<IActionResult> Index(string searchString)
        {
            var realEstate_DBContext = _context.Routes.Include(r => r.FkContacts).Include(r => r.FkUsers);

            if (!String.IsNullOrEmpty(searchString))
            {
                var rs_DBCtx = realEstate_DBContext.Where(x => x.RouteName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1);
                return View(await rs_DBCtx.ToListAsync());
            }

            return View(await realEstate_DBContext.ToListAsync());
        }

        // GET: Routes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routes = await _context.Routes
                .Include(r => r.FkContacts)
                .Include(r => r.FkUsers)
                .FirstOrDefaultAsync(m => m.RoutesId == id);
            if (routes == null)
            {
                return NotFound();
            }

            return PartialView(routes);
        }

        // GET: Routes/Create
        public IActionResult Create()
        {
            ViewData["FkContactsId"] = new SelectList(_context.Contacts, "ContactsId", "ContactLastNames");
            ViewData["FkUsersId"] = new SelectList(_context.Users, "UsersId", "LastNames");
            return PartialView();
        }

        // POST: Routes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoutesId,RouteName,RouteDate,FkContactsId,FkUsersId")] Routes routes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(routes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkContactsId"] = new SelectList(_context.Contacts, "ContactsId", "ContactLastNames", routes.FkContactsId);
            ViewData["FkUsersId"] = new SelectList(_context.Users, "UsersId", "LastNames", routes.FkUsersId);
            return PartialView(routes);
        }

        // GET: Routes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routes = await _context.Routes.FindAsync(id);
            if (routes == null)
            {
                return NotFound();
            }
            ViewData["FkContactsId"] = new SelectList(_context.Contacts, "ContactsId", "ContactLastNames", routes.FkContactsId);
            ViewData["FkUsersId"] = new SelectList(_context.Users, "UsersId", "LastNames", routes.FkUsersId);
            return PartialView(routes);
        }

        // POST: Routes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoutesId,RouteName,RouteDate,FkContactsId,FkUsersId")] Routes routes)
        {
            if (id != routes.RoutesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(routes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoutesExists(routes.RoutesId))
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
            ViewData["FkContactsId"] = new SelectList(_context.Contacts, "ContactsId", "ContactLastNames", routes.FkContactsId);
            ViewData["FkUsersId"] = new SelectList(_context.Users, "UsersId", "LastNames", routes.FkUsersId);
            return PartialView(routes);
        }

        // GET: Routes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routes = await _context.Routes
                .Include(r => r.FkContacts)
                .Include(r => r.FkUsers)
                .FirstOrDefaultAsync(m => m.RoutesId == id);
            if (routes == null)
            {
                return NotFound();
            }

            return PartialView(routes);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var routes = await _context.Routes.FindAsync(id);
            _context.Routes.Remove(routes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoutesExists(int id)
        {
            return _context.Routes.Any(e => e.RoutesId == id);
        }
    }
}
