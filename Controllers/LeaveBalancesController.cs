using HRMS.Data;
using HRMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HRMS.Controllers
{
    public class LeaveBalancesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LeaveBalancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var model = await _context.Employees
               .Include(c => c.Cluster)
               .Include(c => c.Bank)
               .Include(c => c.Designation)
               .Include(c => c.EmployeeStatus)
               .ToListAsync();

            return View(model);
        }
        [HttpGet]
        public IActionResult AdjustLeaveBalance(int id)
        {
            LeaveAdjustmentEntry leaveAdjustment = new LeaveAdjustmentEntry();
            leaveAdjustment.EmployeeId = id;
            ViewData["AdjustmentTypeId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "LeaveAdjustment"), "Id", "Description");
            ViewData["LeavePeriodId"] = new SelectList(_context.LeavePeriods.Where(x => x.Closed == false), "Id", "Name");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName",id);
            return View(leaveAdjustment);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdjustLeaveBalance(LeaveAdjustmentEntry leaveAdjustmentEntry)
        {
            ViewData["AdjustmentTypeId"] = new SelectList(_context.SystemCodeDetails, "Id", "Description", leaveAdjustmentEntry.AdjustmentTypeId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveAdjustmentEntry.EmployeeId);
            ViewData["LeavePeriodId"] = new SelectList(_context.LeavePeriods.Where(x=>x.Closed==false), "Id", "Name",leaveAdjustmentEntry.LeavePeriodId);

            var adjustmentType = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveAdjustment" && y.Id== leaveAdjustmentEntry.AdjustmentTypeId).FirstOrDefault();
        
            leaveAdjustmentEntry.AdjustmentDescription = leaveAdjustmentEntry.AdjustmentDescription +"-" + adjustmentType.Description;
            //leaveAdjustmentEntry.Id = 0;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            leaveAdjustmentEntry.CreatedById = userId;
            leaveAdjustmentEntry.CreatedOn = DateTime.Now;

            if (LeaveAdjustmentEntryExists(leaveAdjustmentEntry.Id))
            {
                _context.Update(leaveAdjustmentEntry);
                await _context.SaveChangesAsync(userId);
            }
            else
            {
                _context.Add(leaveAdjustmentEntry);
                await _context.SaveChangesAsync(userId);
            }
          

            var employee = await _context.Employees.FindAsync(leaveAdjustmentEntry.EmployeeId);
            if (adjustmentType.Code == "Positive")
            {
                employee.LeaveOutStandingBalance = employee.LeaveOutStandingBalance + leaveAdjustmentEntry.NoOfDays;
            }
            else
            {
                employee.LeaveOutStandingBalance = employee.LeaveOutStandingBalance - leaveAdjustmentEntry.NoOfDays;

            }
            _context.Update(employee);
            await _context.SaveChangesAsync(userId);

            return RedirectToAction(nameof(Index));

            //return View(leaveAdjustmentEntry);
        }
        private bool LeaveAdjustmentEntryExists(int id)
        {
            return _context.LeaveAdjustmentEntries.Any(e => e.Id == id);
        }
    }
}
