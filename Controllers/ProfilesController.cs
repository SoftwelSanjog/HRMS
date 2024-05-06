using HRMS.Data;
using HRMS.Models;
using HRMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace HRMS.Controllers
{
    public class ProfilesController : Controller
    {
        public ApplicationDbContext _context;
        public ProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var tasks = new ProfileViewModel();
            var roles = await _context.Roles.OrderBy(x => x.Name).ToListAsync();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
            var systemtasks = await _context.SystemProfiles
                .Include("Children.Children.Children")
                .OrderBy(x => x.Order)
                .ToListAsync();
            ViewBag.Tasks = new SelectList(systemtasks, "Id", "Name");
            return View(tasks);
        }

        public async Task<ActionResult> AssignRights(ProfileViewModel vm)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = new RoleProfile
            {
                TaskId = vm.TaskId,
                RoleId = vm.RoleId
            };
            _context.RoleProfiles.Add(role);
            await _context.SaveChangesAsync(UserId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UserRights(string Id)
        {
            var tasks = new ProfileViewModel();
            tasks.RoleId = Id;
            var systemtasks = await _context.SystemProfiles
                .Include(s=>s.Profile)
                .Include("Children.Children.Children")
                .OrderBy(x => x.Order)
                .ToListAsync();
            tasks.Profiles = systemtasks;
            tasks.RolesRightsIds = await _context.RoleProfiles.Where(x => x.RoleId == Id).Select(r => r.TaskId).ToListAsync();
            return View(tasks);
        }
        [HttpPost]
        public async Task<IActionResult> UserGroupRights(string Id, ProfileViewModel pv)
        {

            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var allrights = await _context.RoleProfiles.Where(x=>x.RoleId==Id).ToListAsync();
            _context.RoleProfiles.RemoveRange(allrights);
            await _context.SaveChangesAsync(UserId);

            foreach (var taskId in pv.Ids.Distinct())
            {
                var role = new RoleProfile
                {
                    TaskId = taskId,
                    RoleId = Id,
                };
                _context.RoleProfiles.Add(role);
                await _context.SaveChangesAsync(UserId);
            }
            return RedirectToAction("Index");
        }
    }
}
