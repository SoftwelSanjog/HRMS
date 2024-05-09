using HRMS.Data;
using HRMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HRMS.Controllers
{
    public class WorkFlowUserGroupMembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkFlowUserGroupMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WorkFlowUserGroupMembers
        public async Task<IActionResult> Index(int? id)
        {
            var members = await _context.WorkFlowUserGroupMembers.Where(x => x.WorkFlowUserGroup.Id == id).Include(w => w.Approver).Include(w => w.Sender).Include(w => w.WorkFlowUserGroup).ToListAsync();
            return View(members);
        }

        // GET: WorkFlowUserGroupMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workFlowUserGroupMember = await _context.WorkFlowUserGroupMembers
                .Include(w => w.Approver)
                .Include(w => w.Sender)
                .Include(w => w.WorkFlowUserGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workFlowUserGroupMember == null)
            {
                return NotFound();
            }

            return View(workFlowUserGroupMember);
        }

        // GET: WorkFlowUserGroupMembers/Create
        public IActionResult Create()
        {
            ViewData["ApproverId"] = new SelectList(_context.Users, "Id", "FullName");
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "FullName");
            ViewData["WorkFlowUserGroupId"] = new SelectList(_context.WorkFlowUserGroups, "Id", "Description");
            return View();
        }

        // POST: WorkFlowUserGroupMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkFlowUserGroupMember workFlowUserGroupMember)
        {

            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Add(workFlowUserGroupMember);
            await _context.SaveChangesAsync(UserId);
            return RedirectToAction(nameof(Index));

            ViewData["ApproverId"] = new SelectList(_context.Users, "Id", "FullName", workFlowUserGroupMember.ApproverId);
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "FullName", workFlowUserGroupMember.SenderId);
            ViewData["WorkFlowUserGroupId"] = new SelectList(_context.WorkFlowUserGroups, "Id", "Description", workFlowUserGroupMember.WorkFlowUserGroupId);
            return View(workFlowUserGroupMember);
        }

        // GET: WorkFlowUserGroupMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workFlowUserGroupMember = await _context.WorkFlowUserGroupMembers.FindAsync(id);
            if (workFlowUserGroupMember == null)
            {
                return NotFound();
            }
            ViewData["ApproverId"] = new SelectList(_context.Users, "Id", "FullName", workFlowUserGroupMember.ApproverId);
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "FullName", workFlowUserGroupMember.SenderId);
            ViewData["WorkFlowUserGroupId"] = new SelectList(_context.WorkFlowUserGroups, "Id", "Description", workFlowUserGroupMember.WorkFlowUserGroupId);
            return View(workFlowUserGroupMember);
        }

        // POST: WorkFlowUserGroupMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WorkFlowUserGroupMember workFlowUserGroupMember)
        {
            if (id != workFlowUserGroupMember.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    _context.Update(workFlowUserGroupMember);
                    await _context.SaveChangesAsync(UserId);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkFlowUserGroupMemberExists(workFlowUserGroupMember.Id))
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
            ViewData["ApproverId"] = new SelectList(_context.Users, "Id", "FullName", workFlowUserGroupMember.ApproverId);
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "FullName", workFlowUserGroupMember.SenderId);
            ViewData["WorkFlowUserGroupId"] = new SelectList(_context.WorkFlowUserGroups, "Id", "Description", workFlowUserGroupMember.WorkFlowUserGroupId);
            return View(workFlowUserGroupMember);
        }

        // GET: WorkFlowUserGroupMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workFlowUserGroupMember = await _context.WorkFlowUserGroupMembers
                .Include(w => w.Approver)
                .Include(w => w.Sender)
                .Include(w => w.WorkFlowUserGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workFlowUserGroupMember == null)
            {
                return NotFound();
            }

            return View(workFlowUserGroupMember);
        }

        // POST: WorkFlowUserGroupMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workFlowUserGroupMember = await _context.WorkFlowUserGroupMembers.FindAsync(id);
            if (workFlowUserGroupMember != null)
            {
                _context.WorkFlowUserGroupMembers.Remove(workFlowUserGroupMember);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkFlowUserGroupMemberExists(int id)
        {
            return _context.WorkFlowUserGroupMembers.Any(e => e.Id == id);
        }
    }
}
