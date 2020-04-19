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
    public class TasksController : Controller
    {
        private readonly RealEstate_DBContext _context;

        public TasksController(RealEstate_DBContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index(string searchString)
        {
            var realEstate_DBContext = _context.Tasks.Include(t => t.FkUsers);

            if (!String.IsNullOrEmpty(searchString))
            {
                var rs_DBCtx = realEstate_DBContext.Where(x => x.TaskName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1);
                return View(await rs_DBCtx.ToListAsync());
            }


            return View(await realEstate_DBContext.ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .Include(t => t.FkUsers)
                .FirstOrDefaultAsync(m => m.TasksId == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return PartialView(tasks);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            ViewData["FkUsersId"] = new SelectList(_context.Users, "UsersId", "LastNames");
            return PartialView();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TasksId,TaskName,Description,FkUsersId,TasKDate,TaskReminderDate")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tasks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkUsersId"] = new SelectList(_context.Users, "UsersId", "LastNames", tasks.FkUsersId);
            return PartialView(tasks);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks == null)
            {
                return NotFound();
            }
            ViewData["FkUsersId"] = new SelectList(_context.Users, "UsersId", "LastNames", tasks.FkUsersId);
            return PartialView(tasks);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TasksId,TaskName,Description,FkUsersId,TaskDate,TaskReminderDate")] Tasks tasks)
        {
            if (id != tasks.TasksId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tasks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TasksExists(tasks.TasksId))
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
            ViewData["FkUsersId"] = new SelectList(_context.Users, "UsersId", "LastNames", tasks.FkUsersId);
            return PartialView(tasks);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .Include(t => t.FkUsers)
                .FirstOrDefaultAsync(m => m.TasksId == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return PartialView(tasks);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tasks = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(tasks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TasksExists(int id)
        {
            return _context.Tasks.Any(e => e.TasksId == id);
        }
    }
}
