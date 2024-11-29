using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using ServiceReport.Data;
using ServiceReport.Enum;
using ServiceReport.Models;
using System.Security.Claims;

namespace ServiceReport.Controllers
{
    public class DailyWorkReportsController : Controller
    {
        private readonly ServiceReportContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DailyWorkReportsController(ServiceReportContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: DailyWorkReports
        public async Task<IActionResult> Index()
        {
            //return View(await _context.DailyWorkReport.ToListAsync());
            return RedirectToAction(nameof(Create));
        }

        // GET: DailyWorkReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyWorkReport = await _context.DailyWorkReport
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dailyWorkReport == null)
            {
                return NotFound();
            }

            return View(dailyWorkReport);
        }

        // GET: DailyWorkReports/Create
        public IActionResult Create()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userFullName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.GivenName)?.Value;
            
            var model = new DailyWorkReportViewModel
            {
                EmplyoeeID = userId,  // Default to the current user's ID
                EngineerName = userFullName // Default to the user's full name
            };

           // Populate MachineType dropdown
            ViewBag.MachineTypeOptions = System.Enum.GetValues(typeof(MachineType))
                .Cast<MachineType>()
                .Select(mt => new SelectListItem
                {
                    Value = mt.ToString(),
                    Text = mt.GetDisplayName()
                }).ToList();


            return View(model);
        }

        // POST: DailyWorkReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmplyoeeID,EngineerName,CompanyName,MachineType,ReportDate,InTime,OutTime,WorkingHours,WorkActivity,CustomerRemark,CustomerSealAndSign")] DailyWorkReportViewModel dailyWorkReport)
        {
            if (ModelState.IsValid)
            {
                var reportEntity = new DailyWorkReport
                {
                    EmplyoeeID = dailyWorkReport.EmplyoeeID,
                    EngineerName = dailyWorkReport.EngineerName,
                    CompanyName = dailyWorkReport.CompanyName,
                    MachineType = dailyWorkReport.MachineType,
                    ReportDate = dailyWorkReport.ReportDate,
                    InTime = dailyWorkReport.InTime,
                    OutTime = dailyWorkReport.OutTime,
                    WorkingHours = dailyWorkReport.WorkingHours,
                    WorkActivity = dailyWorkReport.WorkActivity,
                    CustomerRemark = dailyWorkReport.CustomerRemark,
                    CustomerSealAndSign = dailyWorkReport.CustomerSealAndSign,
                    CreatedDate = DateTime.UtcNow, // Set to current time
                    ModifiedDate = DateTime.UtcNow // Set to current time
                };
                _context.Add(reportEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var machineTypes = System.Enum.GetValues(typeof(MachineType))
                .Cast<MachineType>()
                .Select(mt => new
                {
                    Value = (int)mt,
                    Text = mt.ToString().Replace("SheetCutting", "Sheet Cutting").Replace("Machine", " Machine").Replace("Welding", " Welding") // Optional formatting
                })
                .ToList();

            // Add an empty option at the top
            var machineTypeList = new List<dynamic> { new { Value = "", Text = "Select Machine Type" } }
                .Concat(machineTypes);

            ViewBag.MachineTypes = new SelectList(machineTypeList, "Value", "Text");
            return View(dailyWorkReport);
        }

        // GET: DailyWorkReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyWorkReport = await _context.DailyWorkReport.FindAsync(id);
            if (dailyWorkReport == null)
            {
                return NotFound();
            }
            return View(dailyWorkReport);
        }

        // POST: DailyWorkReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmplyoeeID,EngineerName,CompanyName,MachineType,ReportDate,InTime,OutTime,WorkingHours,WorkActivity,CustomerRemark,CustomerSealAndSign,CreatedDate,ModifiedDate")] DailyWorkReport dailyWorkReport)
        {
            if (id != dailyWorkReport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dailyWorkReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailyWorkReportExists(dailyWorkReport.Id))
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
            return View(dailyWorkReport);
        }

        // GET: DailyWorkReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyWorkReport = await _context.DailyWorkReport
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dailyWorkReport == null)
            {
                return NotFound();
            }

            return View(dailyWorkReport);
        }

        // POST: DailyWorkReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dailyWorkReport = await _context.DailyWorkReport.FindAsync(id);
            if (dailyWorkReport != null)
            {
                _context.DailyWorkReport.Remove(dailyWorkReport);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DailyWorkReportExists(int id)
        {
            return _context.DailyWorkReport.Any(e => e.Id == id);
        }
    }
}
