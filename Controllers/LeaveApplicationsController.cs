using HRMS.Data;
using HRMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HRMS.Controllers
{
    public class LeaveApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaveApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeaveApplications
        public async Task<IActionResult> Index()
        {

            //var awaitingStatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "AwaitingApproval").FirstOrDefault();

            var applicationDbContext = _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status);
            //.Where(l => l.StatusId == awaitingStatus!.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LeaveApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Duration)
                 .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }
        public async Task<IActionResult> ApprovedLeaveApplication()
        {
            var approvalStatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "Approved").FirstOrDefault();

            var applicationDbContext = _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .Where(l => l.StatusId == approvalStatus!.Id);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> RejectedLeaveApplication()
        {
            var approvalStatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "Approved").FirstOrDefault();

            var applicationDbContext = _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .Where(l => l.StatusId == approvalStatus!.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> ApproveLeave(int? id)
        {
            var leaveApplication = await _context.LeaveApplications
               .Include(l => l.Duration)
                .Include(l => l.Employee)
               .Include(l => l.LeaveType)
               .Include(l => l.Status)
               .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name");
            return View(leaveApplication);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveLeave(LeaveApplication leave)
        {
            var approvalStatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "Approved").FirstOrDefault();
            var adjustmentType = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveAdjustment" && y.Code == "Negative").FirstOrDefault();

            var leaveApplication = await _context.LeaveApplications
               .Include(l => l.Duration)
                .Include(l => l.Employee)
               .Include(l => l.LeaveType)
               .Include(l => l.Status)
               .FirstOrDefaultAsync(m => m.Id == leave.Id);
            if (leaveApplication == null)
            {
                return NotFound();
            }
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            leaveApplication.ApprovedById = UserId;
            leaveApplication.ApprovedOn = DateTime.Now;
            leaveApplication.StatusId = approvalStatus!.Id;
            leaveApplication.ApprovalNotes = leave.ApprovalNotes;
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name");

            _context.Update(leaveApplication);
            await _context.SaveChangesAsync(UserId);

            var adjustment = new LeaveAdjustmentEntry
            {
                EmployeeId = leaveApplication.EmployeeId,
                NoOfDays = leaveApplication.NoOfDays,
                LeaveStartDate = leaveApplication.StartDate,
                LeaveEndDate = leaveApplication.EndDate,
                AdjustmentDescription = "Leave Taken - Negative Adjustment",
                LeaveAdjustmentDate = DateTime.Now,
                AdjustmentTypeId = adjustmentType.Id
            };
            _context.Add(adjustment);
            await _context.SaveChangesAsync(UserId);

            var employee = await _context.Employees.FindAsync(leaveApplication.EmployeeId);
            employee.LeaveOutStandingBalance = employee.AllocatedLeaveDays - leaveApplication.NoOfDays;
            _context.Update(employee);
            await _context.SaveChangesAsync(UserId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AwaitingApproveLeave(int? id)
        {
            var leaveApplication = await _context.LeaveApplications
               .Include(l => l.Duration)
                .Include(l => l.Employee)
               .Include(l => l.LeaveType)
               .Include(l => l.Status)
               .FirstOrDefaultAsync(m => m.Id == id);

            if (leaveApplication == null)
            {
                return NotFound();
            }
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name");
            return View(leaveApplication);
        }

        [HttpPost]
        public async Task<IActionResult> AwaitingApproveLeave(LeaveApplication leave)
        {
            var approvalStatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "AwaitingApproval").FirstOrDefault();

            var leaveApplication = await _context.LeaveApplications
               .Include(l => l.Duration)
                .Include(l => l.Employee)
               .Include(l => l.LeaveType)
               .Include(l => l.Status)
               .FirstOrDefaultAsync(m => m.Id == leave.Id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            leaveApplication.ApprovedById = "Sanjog";
            leaveApplication.ApprovedOn = DateTime.Now;
            leaveApplication.StatusId = approvalStatus!.Id;

            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name");

            _context.Update(leaveApplication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> RejectLeave(int? id)
        {
            var leaveApplication = await _context.LeaveApplications
               .Include(l => l.Duration)
                .Include(l => l.Employee)
               .Include(l => l.LeaveType)
               .Include(l => l.Status)
               .FirstOrDefaultAsync(m => m.Id == id);

            if (leaveApplication == null)
            {
                return NotFound();
            }
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name");
            return View(leaveApplication);
        }

        [HttpPost]
        public async Task<IActionResult> RejectLeave(LeaveApplication leave)
        {
            var rejectStatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "Rejected").FirstOrDefault();

            var leaveApplication = await _context.LeaveApplications
               .Include(l => l.Duration)
                .Include(l => l.Employee)
               .Include(l => l.LeaveType)
               .Include(l => l.Status)
               .FirstOrDefaultAsync(m => m.Id == leave.Id);
            if (leaveApplication == null)
            {
                return NotFound();
            }
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            leaveApplication.ApprovedById = UserId;
            leaveApplication.ApprovedOn = DateTime.Now;
            leaveApplication.StatusId = rejectStatus!.Id;
            leaveApplication.ApprovalNotes = leave.ApprovalNotes;
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name");

            _context.Update(leaveApplication);
            await _context.SaveChangesAsync(UserId);
            return RedirectToAction(nameof(Index));
        }


        // GET: LeaveApplications/Create
        public IActionResult Create()
        {
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name");
            return View();
        }

        // POST: LeaveApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveApplication leaveApplication)
        {
            var pendingStatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.Code == "AwaitingApproval" && y.SystemCode.Code == "LeaveApprovalStatus").FirstOrDefault();
            //    if (ModelState.IsValid)
            //    {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            leaveApplication.CreatedById = UserId;
            leaveApplication.CreatedOn = DateTime.Now;

            leaveApplication.StatusId = pendingStatus.Id;
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            _context.Add(leaveApplication);
            await _context.SaveChangesAsync(UserId);
            return RedirectToAction(nameof(Index));
            // }


        }

        // GET: LeaveApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications.FindAsync(id);
            if (leaveApplication == null)
            {
                return NotFound();
            }
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }

        // POST: LeaveApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveApplication leaveApplication)
        {
            if (id != leaveApplication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var pendingStatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.Code == "AwaitingApproval" && y.SystemCode.Code == "LeaveApprovalStatus").FirstOrDefault();

                    var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    leaveApplication.ModifiedById = UserId;
                    leaveApplication.ModifiedOn = DateTime.Now;
                    leaveApplication.StatusId = pendingStatus!.Id;

                    _context.Update(leaveApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveApplicationExists(leaveApplication.Id))
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
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }

        // GET: LeaveApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        // POST: LeaveApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveApplication = await _context.LeaveApplications.FindAsync(id);
            if (leaveApplication != null)
            {
                _context.LeaveApplications.Remove(leaveApplication);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveApplicationExists(int id)
        {
            return _context.LeaveApplications.Any(e => e.Id == id);
        }
    }
}
