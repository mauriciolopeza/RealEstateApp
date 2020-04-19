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
    public class CommunicationLogsController : Controller
    {
        private readonly RealEstate_DBContext _context;

        public CommunicationLogsController(RealEstate_DBContext context)
        {
            _context = context;
        }

        // GET: CommunicationLogs
        public async Task<IActionResult> Index(string searchString)
        {
            var realEstate_DBContext = _context.CommunicationLog;

            if (!String.IsNullOrEmpty(searchString))
            {
                var rs_DBCtx = realEstate_DBContext.Where(x => x.CommunicationType.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1);
                return View(await rs_DBCtx.ToListAsync());
            }

            return View(await _context.CommunicationLog.ToListAsync());
        }

        // GET: CommunicationLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communicationLog = await _context.CommunicationLog
                .FirstOrDefaultAsync(m => m.CommunicationLogId == id);
            if (communicationLog == null)
            {
                return NotFound();
            }

            return PartialView(communicationLog);
        }

        // GET: CommunicationLogs/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: CommunicationLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommunicationLogId,CommunicationDate,CommunicationType,Notes")] CommunicationLog communicationLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(communicationLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView(communicationLog);
        }

        // GET: CommunicationLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communicationLog = await _context.CommunicationLog.FindAsync(id);
            if (communicationLog == null)
            {
                return NotFound();
            }
            return PartialView(communicationLog);
        }

        // POST: CommunicationLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommunicationLogId,CommunicationDate,CommunicationType,Notes")] CommunicationLog communicationLog)
        {
            if (id != communicationLog.CommunicationLogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(communicationLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommunicationLogExists(communicationLog.CommunicationLogId))
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
            return PartialView(communicationLog);
        }

        // GET: CommunicationLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communicationLog = await _context.CommunicationLog
                .FirstOrDefaultAsync(m => m.CommunicationLogId == id);
            if (communicationLog == null)
            {
                return NotFound();
            }

            return PartialView(communicationLog);
        }

        // POST: CommunicationLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var communicationLog = await _context.CommunicationLog.FindAsync(id);
            _context.CommunicationLog.Remove(communicationLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommunicationLogExists(int id)
        {
            return _context.CommunicationLog.Any(e => e.CommunicationLogId == id);
        }
    }
}
