using HRMS.Data;
using HRMS.Models;
using HRMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
    }
}
