﻿using HRMS.Data;
using HRMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HRMS.Controllers
{
    public class SystemProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SystemProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SystemProfiles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SystemProfiles.Include(s => s.Profile);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SystemProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemProfile = await _context.SystemProfiles
                .Include(s => s.Profile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemProfile == null)
            {
                return NotFound();
            }

            return View(systemProfile);
        }

        // GET: SystemProfiles/Create
        public IActionResult Create()
        {
            ViewData["ProfileId"] = new SelectList(_context.SystemProfiles, "Id", "Name");
            return View();
        }

        // POST: SystemProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SystemProfile systemProfile)
        {
            //if (ModelState.IsValid)
            //{
            ViewData["ProfileId"] = new SelectList(_context.SystemProfiles, "Id", "Name", systemProfile.ProfileId);
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            systemProfile.CreatedById = UserId;
            systemProfile.CreatedOn = DateTime.Now;
            _context.Add(systemProfile);
            await _context.SaveChangesAsync(UserId);
            return RedirectToAction(nameof(Index));
            // }
            //return View(systemProfile);
        }

        // GET: SystemProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemProfile = await _context.SystemProfiles.FindAsync(id);
            if (systemProfile == null)
            {
                return NotFound();
            }
            ViewData["ProfileId"] = new SelectList(_context.SystemProfiles, "Id", "Name", systemProfile.ProfileId);
            return View(systemProfile);
        }

        // POST: SystemProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SystemProfile systemProfile)
        {
            if (id != systemProfile.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    systemProfile.ModifiedById = UserId;
                    systemProfile.ModifiedOn = DateTime.Now;
                    _context.Update(systemProfile);
                    await _context.SaveChangesAsync(UserId);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemProfileExists(systemProfile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
           // }
            ViewData["ProfileId"] = new SelectList(_context.SystemProfiles, "Id", "Name", systemProfile.ProfileId);
            return View(systemProfile);
        }

        // GET: SystemProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemProfile = await _context.SystemProfiles
                .Include(s => s.Profile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemProfile == null)
            {
                return NotFound();
            }

            return View(systemProfile);
        }

        // POST: SystemProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var systemProfile = await _context.SystemProfiles.FindAsync(id);
            if (systemProfile != null)
            {
                _context.SystemProfiles.Remove(systemProfile);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemProfileExists(int id)
        {
            return _context.SystemProfiles.Any(e => e.Id == id);
        }
    }
}
