﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRMS.Data;
using HRMS.Models;

namespace HRMS.Controllers
{
    public class ClustersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClustersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clusters
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clusters.ToListAsync());
        }

        // GET: Clusters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cluster = await _context.Clusters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cluster == null)
            {
                return NotFound();
            }

            return View(cluster);
        }

        // GET: Clusters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clusters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClusterCode,ClusterName,CreatedById,CreatedOn,ModifiedById,ModifiedOn")] Cluster cluster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cluster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cluster);
        }

        // GET: Clusters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cluster = await _context.Clusters.FindAsync(id);
            if (cluster == null)
            {
                return NotFound();
            }
            return View(cluster);
        }

        // POST: Clusters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClusterCode,ClusterName,CreatedById,CreatedOn,ModifiedById,ModifiedOn")] Cluster cluster)
        {
            if (id != cluster.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cluster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClusterExists(cluster.Id))
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
            return View(cluster);
        }

        // GET: Clusters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cluster = await _context.Clusters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cluster == null)
            {
                return NotFound();
            }

            return View(cluster);
        }

        // POST: Clusters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cluster = await _context.Clusters.FindAsync(id);
            if (cluster != null)
            {
                _context.Clusters.Remove(cluster);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClusterExists(int id)
        {
            return _context.Clusters.Any(e => e.Id == id);
        }
    }
}
