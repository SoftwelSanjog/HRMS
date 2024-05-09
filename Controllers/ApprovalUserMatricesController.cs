using HRMS.Data;
using HRMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HRMS.Controllers
{
    public class ApprovalUserMatricesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApprovalUserMatricesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApprovalUserMatrices
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ApprovalUserMatrixs.Include(a => a.DocumentType).Include(a => a.User).Include(a => a.WorkFlowUserGroup);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ApprovalUserMatrices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var approvalUserMatrix = await _context.ApprovalUserMatrixs
                .Include(a => a.DocumentType)
                .Include(a => a.User)
                .Include(a => a.WorkFlowUserGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (approvalUserMatrix == null)
            {
                return NotFound();
            }

            return View(approvalUserMatrix);
        }

        // GET: ApprovalUserMatrices/Create
        public IActionResult Create()
        {
            //_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "EmploymentTerms"
            ViewData["DocumentTypeId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "DocumentTypes"), "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName");
            ViewData["WorkFlowUserGroupId"] = new SelectList(_context.WorkFlowUserGroups, "Id", "Description");
            return View();
        }

        // POST: ApprovalUserMatrices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApprovalUserMatrix approvalUserMatrix)
        {
            //if (ModelState.IsValid)
            //{
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            approvalUserMatrix.CreatedOn = DateTime.Now;
            approvalUserMatrix.CreatedById = UserId;
            _context.Add(approvalUserMatrix);
            await _context.SaveChangesAsync(UserId);
            return RedirectToAction(nameof(Index));
            //}
            ViewData["DocumentTypeId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "DocumentTypes"), "Id", "Description", approvalUserMatrix.DocumentTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", approvalUserMatrix.UserId);
            ViewData["WorkFlowUserGroupId"] = new SelectList(_context.WorkFlowUserGroups, "Id", "Description", approvalUserMatrix.WorkFlowUserGroupId);
            return View(approvalUserMatrix);
        }

        // GET: ApprovalUserMatrices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var approvalUserMatrix = await _context.ApprovalUserMatrixs.FindAsync(id);
            if (approvalUserMatrix == null)
            {
                return NotFound();
            }
            ViewData["DocumentTypeId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "DocumentTypes"), "Id", "Description", approvalUserMatrix.DocumentTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", approvalUserMatrix.UserId);
            ViewData["WorkFlowUserGroupId"] = new SelectList(_context.WorkFlowUserGroups, "Id", "Description", approvalUserMatrix.WorkFlowUserGroupId);
            return View(approvalUserMatrix);
        }

        // POST: ApprovalUserMatrices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ApprovalUserMatrix approvalUserMatrix)
        {
            if (id != approvalUserMatrix.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    approvalUserMatrix.ModifiedById = UserId;
                    approvalUserMatrix.ModifiedOn = DateTime.Now;
                    _context.Update(approvalUserMatrix);
                    await _context.SaveChangesAsync(UserId);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApprovalUserMatrixExists(approvalUserMatrix.Id))
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
            ViewData["DocumentTypeId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "DocumentTypes"), "Id", "Description", approvalUserMatrix.DocumentTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", approvalUserMatrix.UserId);
            ViewData["WorkFlowUserGroupId"] = new SelectList(_context.WorkFlowUserGroups, "Id", "Description", approvalUserMatrix.WorkFlowUserGroupId);
            return View(approvalUserMatrix);
        }

        // GET: ApprovalUserMatrices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var approvalUserMatrix = await _context.ApprovalUserMatrixs
                .Include(a => a.DocumentType)
                .Include(a => a.User)
                .Include(a => a.WorkFlowUserGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (approvalUserMatrix == null)
            {
                return NotFound();
            }

            return View(approvalUserMatrix);
        }

        // POST: ApprovalUserMatrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var approvalUserMatrix = await _context.ApprovalUserMatrixs.FindAsync(id);
            if (approvalUserMatrix != null)
            {
                _context.ApprovalUserMatrixs.Remove(approvalUserMatrix);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApprovalUserMatrixExists(int id)
        {
            return _context.ApprovalUserMatrixs.Any(e => e.Id == id);
        }
    }
}
