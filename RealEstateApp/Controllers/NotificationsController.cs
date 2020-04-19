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
    public class NotificationsController : Controller
    {
        private readonly RealEstate_DBContext _context;

        public NotificationsController(RealEstate_DBContext context)
        {
            _context = context;
        }

        // GET: Notifications
        public async Task<IActionResult> Index(string searchString)
        {
            var realEstate_DBContext = _context.Notifications.Include(n => n.FkUsers);

            if (!String.IsNullOrEmpty(searchString))
            {
                var rs_DBCtx = realEstate_DBContext.Where(x => x.Notification.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1);
                return View(await rs_DBCtx.ToListAsync());
            }

            return View(await realEstate_DBContext.ToListAsync());
        }

        // GET: Notifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notifications = await _context.Notifications
                .Include(n => n.FkUsers)
                .FirstOrDefaultAsync(m => m.NotificationsId == id);
            if (notifications == null)
            {
                return NotFound();
            }

            return PartialView(notifications);
        }

        // GET: Notifications/Create
        public IActionResult Create()
        {
            ViewData["FkUsersId"] = new SelectList(_context.Users, "UsersId", "LastNames");
            return PartialView();
        }

        // POST: Notifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NotificationsId,FkUsersId,Notification,NotificationDatetime")] Notifications notifications)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notifications);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkUsersId"] = new SelectList(_context.Users, "UsersId", "LastNames", notifications.FkUsersId);
            return PartialView(notifications);
        }

        // GET: Notifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notifications = await _context.Notifications.FindAsync(id);
            if (notifications == null)
            {
                return NotFound();
            }
            ViewData["FkUsersId"] = new SelectList(_context.Users, "UsersId", "LastNames", notifications.FkUsersId);
            return PartialView(notifications);
        }

        // POST: Notifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NotificationsId,FkUsersId,Notification,NotificationDatetime")] Notifications notifications)
        {
            if (id != notifications.NotificationsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notifications);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotificationsExists(notifications.NotificationsId))
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
            ViewData["FkUsersId"] = new SelectList(_context.Users, "UsersId", "LastNames", notifications.FkUsersId);
            return PartialView(notifications);
        }

        // GET: Notifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notifications = await _context.Notifications
                .Include(n => n.FkUsers)
                .FirstOrDefaultAsync(m => m.NotificationsId == id);
            if (notifications == null)
            {
                return NotFound();
            }

            return PartialView(notifications);
        }

        // POST: Notifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notifications = await _context.Notifications.FindAsync(id);
            _context.Notifications.Remove(notifications);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotificationsExists(int id)
        {
            return _context.Notifications.Any(e => e.NotificationsId == id);
        }
    }
}
