using AutoMapper;
using HRMS.Data;
using HRMS.Models;
using HRMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HRMS.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public EmployeesController(IMapper mapper,IConfiguration configuration, ApplicationDbContext context)
        {
            _mapper = mapper;
            _configuration = configuration;
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index(EmployeeViewModel employees)
        {
            //employees.Employees = await _context.Employees
            //    .Include(c => c.Cluster)
            //    .Include(c => c.Bank)
            //    .Include(c => c.Designation)
            //    .Include(c => c.EmployeeStatus)
            //    .ToListAsync();

            var rawdata = _context.Employees
               .Include(c => c.Cluster)
               .Include(c => c.Bank)
               .Include(c => c.Designation)
               .Include(c => c.EmployeeStatus)
               .AsQueryable();
            if ((!string.IsNullOrEmpty(employees.EmpId)))
            {
                rawdata = rawdata.Where(x => x.EmpId == employees.EmpId);
            }
            if ((!string.IsNullOrEmpty(employees.FirstName)))
            {
                rawdata = rawdata.Where(x => x.FirstName.Contains(employees.FirstName));
            }
            if ((!string.IsNullOrEmpty(employees.PhoneNumber)))
            {
                rawdata = rawdata.Where(x => x.PhoneNumber == employees.PhoneNumber);
            }
            employees.Employees = await rawdata.ToListAsync();
            return View(employees);

            //var model = await _context.Employees
            //    .Include(c => c.Cluster)
            //    .Include(c => c.Bank)
            //    .Include(c => c.Designation)
            //    .Include(c => c.EmployeeStatus)
            //    .ToListAsync();

            //return View(model);
        }

        public async Task<IActionResult> EmployeeGrid()
        {
            return View(await _context.Employees
                .Include(c => c.Cluster)
                .Include(c => c.Bank)
                .Include(c => c.Designation)
                .ToListAsync());
        }
        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            ViewData["ClusterId"] = new SelectList(_context.Clusters, "Id", "ClusterName");
            ViewData["DesignationId"] = new SelectList(_context.Designations, "Id", "Name");
            ViewData["BankId"] = new SelectList(_context.Banks, "Id", "BankName");
            ViewData["GenderId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "Gender"), "Id", "Description");
            ViewData["ReasonForTerminationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "ReasonForTermination"), "Id", "Description");
            ViewData["EmploymentTermsId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "EmploymentTerms"), "Id", "Description");
            ViewData["EmployeeStatusId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "EmployeeStatus"), "Id", "Description");

            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Employee employee, IFormFile employeephoto)
        public async Task<IActionResult> Create(EmployeeViewModel newemployee, IFormFile employeephoto)
        {
            var employee = new Employee();
            _mapper.Map(newemployee, employee);

            if (employeephoto != null)
            {
                var filename = $"{employee.EmpId}_{DateTime.Now.ToString("yyyyMMddHHmmss")}{GetFileExtension(employeephoto.ContentType)}";
                var path = _configuration["FileSettings:UploadFolder"]!;
                var filepath = Path.Combine(path, filename);
                var stream = new FileStream(filepath, FileMode.Create);
                await employeephoto.CopyToAsync(stream);
                employee.ProfilePictureURL = filepath;
            }

            var statusId = await _context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "EmployeeStatus" && x.Code == "Active").FirstOrDefaultAsync();

            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            employee.CreatedById = UserId;
            employee.CreatedOn = DateTime.Now;
            employee.EmployeeStatusId = statusId.Id;

            //if (ModelState.IsValid)
            //{

            _context.Add(employee);
            await _context.SaveChangesAsync(UserId);
            return RedirectToAction(nameof(Index));
            //}

            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", employee.CountryId);
            ViewData["ClusterId"] = new SelectList(_context.Clusters, "Id", "ClusterName", employee.ClusterId);
            ViewData["DesignationId"] = new SelectList(_context.Designations, "Id", "Name", employee.DesignationId);
            ViewData["BankId"] = new SelectList(_context.Banks, "Id", "BankName", employee.BankId);
            ViewData["GenderId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "Gender"), "Id", "Description", employee.GenderId);
            ViewData["ReasonForTerminationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "ReasonForTermination"), "Id", "Description", employee.ReasonForTerminationId);
            ViewData["EmploymentTermsId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "EmploymentTerms"), "Id", "Description", employee.EmploymentTermsId);
            ViewData["EmployeeStatusId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "EmployeeStatus"), "Id", "Description", employee.EmployeeStatusId);

            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var employee = await _context.Employees.FindAsync(id);
            var employee = _context.Employees.Where(x => x.Id == id).FirstOrDefault();
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", employee.CountryId);
            ViewData["ClusterId"] = new SelectList(_context.Clusters, "Id", "ClusterName", employee.ClusterId);
            ViewData["DesignationId"] = new SelectList(_context.Designations, "Id", "Name", employee.DesignationId);
            ViewData["BankId"] = new SelectList(_context.Banks, "Id", "BankName", employee.BankId);
            ViewData["GenderId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "Gender"), "Id", "Description", employee.GenderId);
            ViewData["ReasonForTerminationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "ReasonForTermination"), "Id", "Description", employee.ReasonForTerminationId);
            ViewData["EmploymentTermsId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "EmploymentTerms"), "Id", "Description", employee.EmploymentTermsId);
            ViewData["EmployeeStatusId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "EmployeeStatus"), "Id", "Description", employee.EmployeeStatusId);


            return View(employee);

        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            try
            {
                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                employee.ModifiedById = UserId;
                employee.ModifiedOn = DateTime.Now;
                _context.Update(employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employee.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            //}
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", employee.CountryId);
            ViewData["ClusterId"] = new SelectList(_context.Clusters, "Id", "ClusterName", employee.ClusterId);
            ViewData["DesignationId"] = new SelectList(_context.Designations, "Id", "Name", employee.DesignationId);
            ViewData["BankId"] = new SelectList(_context.Banks, "Id", "BankName", employee.BankId);
            ViewData["GenderId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "Gender"), "Id", "Description", employee.GenderId);
            ViewData["ReasonForTerminationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "ReasonForTermination"), "Id", "Description", employee.ReasonForTerminationId);
            ViewData["EmploymentTermsId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "EmploymentTerms"), "Id", "Description", employee.EmploymentTermsId);
            ViewData["EmployeeStatusId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "EmployeeStatus"), "Id", "Description", employee.EmployeeStatusId);

            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
        private string GetFileExtension(string contentType)
        {
            // Define a mapping between content types and file extensions
            Dictionary<string, string> contentTypeMappings = new Dictionary<string, string>
        {
            {"image/jpeg", ".jpg"},
            {"image/png", ".png"},
            {"application/pdf", ".pdf"},
            // Add more mappings as needed
        };

            // Check if the content type exists in the mapping
            if (contentTypeMappings.ContainsKey(contentType))
            {
                // Return the associated file extension
                return contentTypeMappings[contentType];
            }
            else
            {
                // If content type not found, return empty string or handle it accordingly
                return string.Empty;
            }
        }
    }
}
