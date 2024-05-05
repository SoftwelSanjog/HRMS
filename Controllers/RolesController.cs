using HRMS.Data;
using HRMS.Models;
using HRMS.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Controllers
{
    public class RolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public RolesController(ApplicationDbContext context,
         UserManager<ApplicationUser> userManager,
         RoleManager<IdentityRole> roleManager,
         SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _context.Roles.ToListAsync();
            return View(roles);
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(RolesViewModel model)
        {
            IdentityRole role = new IdentityRole();
            role.Name = model.RoleName;

            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");

            }
            else
            {
                return View(model);
            }
        }
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var role = new RolesViewModel();
            var result = await _roleManager.FindByIdAsync(id);
            role.RoleName = result.Name;
            role.Id = result.Id;
            return View(role);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(string id, RolesViewModel model)
        {
            var checkIfExist = await _roleManager.RoleExistsAsync(model.RoleName);
            if (!checkIfExist)
            {
                var result = await _roleManager.FindByIdAsync(id);
                result.Name = model.RoleName;
                var finalresult = await _roleManager.UpdateAsync(result);
                if (finalresult.Succeeded)
                {
                    return RedirectToAction("Index");

                }
                else
                {
                    return View(model);
                }
            }
            return View(model);

        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var rolemodel = new RolesViewModel();
            var roles = await _roleManager.FindByIdAsync(id);
            if (roles == null)
            {
                return NotFound();
            }
            rolemodel.RoleName = roles.Name;
            rolemodel.Id = roles.Id;

            return View(rolemodel);
        }

        // POST: Designations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var roles = await _roleManager.FindByIdAsync(id);
            if (roles != null)
            {
                await _roleManager.DeleteAsync(roles);
            }
            //await _roleManager.CreateAsync(roles);
            return RedirectToAction(nameof(Index));
        }
    }
}
