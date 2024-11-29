using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceReport.Data;
using ServiceReport.Enum;
using ServiceReport.Models;
using ServiceReport.Services;

namespace ServiceReport.Controllers
{
    public class ReportServicesController : Controller
    {
        private readonly ServiceReportContext _context;
        private readonly TokenService _tokenService;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReportServicesController(ServiceReportContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: ReportServices
        public async Task<IActionResult> Index()
        {
            //return View(await _context.ReportService.ToListAsync());
            return RedirectToAction(nameof(Create));
        }

        // GET: ReportServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportService = await _context.ReportService.FirstOrDefaultAsync(m => m.Id == id);
            if (reportService == null)
            {
                return NotFound();
            }

            return View(reportService);
        }

        // GET: ReportServices/Create
        public IActionResult Create()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userFullName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.GivenName)?.Value;

            var model = new ReportServiceViewModel
            {
                EmployeeID = userId,
                EngineerName = userFullName,
                AreaOptions = System.Enum.GetValues(typeof(AreaOptions))
                                  .Cast<AreaOptions>()
                                  .Select(area => new SelectListItem
                                  {
                                      Value = ((int)area).ToString(),
                                      Text = area.ToString().Replace("_", " ") // Optional formatting
                                  }).ToList()
            };
           
            // Populate SparePartStatus dropdown
            ViewBag.SparePartStatusOptions = System.Enum.GetValues(typeof(SparePartStatus))
                .Cast<SparePartStatus>()
                .Select(status => new SelectListItem
                {
                    Value = status.ToString(),
                    Text = status.GetDisplayName()
                }).ToList();

            // Populate Status dropdown
            ViewBag.StatusOptions = System.Enum.GetValues(typeof(Status))
                .Cast<Status>()
                .Select(status => new SelectListItem
                {
                    Value = status.ToString(),
                    Text = status.GetDisplayName()
                }).ToList();

            // Populate MachineType dropdown
            ViewBag.MachineTypeOptions = System.Enum.GetValues(typeof(MachineType))
                .Cast<MachineType>()
                .Select(mt => new SelectListItem
                {
                    Value = mt.ToString(),
                    Text = mt.GetDisplayName()
                }).ToList();

            // Populate ServiceType dropdown
            ViewBag.ServiceTypeOptions = System.Enum.GetValues(typeof(ServiceType))
                .Cast<ServiceType>()
                .Select(st => new SelectListItem
                {
                    Value = st.ToString(),
                    Text = st.GetDisplayName()
                }).ToList();

            return View(model);
        }

        // POST: ReportServices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Id,EmployeeID,EngineerName,CompanyName,OtherCompanyName,MachineSerialNumber,LaserPower,CompanyAddress,MachineType,ServiceType,StartDate,EndDate,WorkingDays,SelectedAreas,SelectedProblems,Description,Diagnosis,CustomerFeedback,Status,SpareParts,SparePartStatus,CustomerSealSign,ImageBase64,CreatedDate,ModifiedDate")] ReportServiceViewModel reportService)
        {
            if (ModelState.IsValid)
            {
                var reportEntity = new ReportService
                {
                    EmployeeID = reportService.EmployeeID,
                    EngineerName = reportService.EngineerName,
                    CompanyName = reportService.CompanyName,
                    //OtherCompanyName = reportService.OtherCompanyName,
                    MachineSerialNumber = reportService.MachineSerialNumber,
                    LaserPower = reportService.LaserPower,
                    CompanyAddress = reportService.CompanyAddress,
                    MachineType = reportService.MachineType,
                    ServiceType = reportService.ServiceType,
                    StartDate = reportService.StartDate,
                    EndDate = reportService.EndDate,
                    WorkingDays = reportService.WorkingDays,
                    //SelectedAreas = string.Join(",", reportService.SelectedAreas), // Convert list to comma-separated string
                         // Existing property mappings
                    //SelectedAreas = string.Join(",", reportService.SelectedAreas),
                    //SelectedProblems = reportService.SelectedProblems
                //.SelectMany(kvp => kvp.Value.Select(problem => $"{kvp.Key}:{problem}"))
                //.Aggregate((current, next) => $"{current},{next}"),
            // Other mappings
                    Description = reportService.Description,
                    Diagnosis = reportService.Diagnosis,
                    CustomerFeedback = reportService.CustomerFeedback,
                    Status = reportService.Status,
                    SpareParts = reportService.SpareParts,
                    SparePartStatus = reportService.SparePartStatus,
                    CustomerSealSign = reportService.CustomerSealSign,
                    ImageBase64 = reportService.ImageBase64,
                    CreatedDate = DateTime.UtcNow, // Set to current time
                    ModifiedDate = DateTime.UtcNow, // Set to current time
                     //TokenNumber = reportService.TokenNumber // Set the token number
                };
                reportEntity.TokenNumber =await _tokenService.GenerateTokenAsync();
                _context.Add(reportEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }

            // Repopulate AreaOptions in case of validation failure
            reportService.AreaOptions = System.Enum.GetValues(typeof(AreaOptions))
                .Cast<AreaOptions>()
                .Select(area => new SelectListItem
                {
                    Value = ((int)area).ToString(),
                    Text = area.ToString().Replace("_", " ") // Optional formatting
                }).ToList();
            return View(reportService); // Return the original model to the view for re-rendering
        }

        // GET: ReportServices/Customer Data from Machine Serial Number
                [HttpGet]
        public async Task<IActionResult> GetMachineDetails(string machineSerialNumber)
        {
            if (string.IsNullOrWhiteSpace(machineSerialNumber))
            {
                return BadRequest("Machine serial number is required.");
            }

            // Fetch machine data from the master data table based on the serial number
            var machine = await _context.MasterData
                                         .Where(m => m.MachineSerialNumber == machineSerialNumber)
                                         .Select(m => new
                                         {
                                             CompanyName = m.CompanyName,
                                             CompanyAddress = m.CompanyAddress,
                                             //LaserPower = m.LaserPower
                                         })
                                         .FirstOrDefaultAsync();

            if (machine == null)
            {
                return NotFound("Machine data not found.");
            }

            return Json(machine); // Return the machine data as JSON
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var reportService = await _context.ReportService.FindAsync(id);
            if (reportService == null) return NotFound();

            // Populate SparePartStatus dropdown
            ViewBag.SparePartStatusOptions = System.Enum.GetValues(typeof(SparePartStatus))
                .Cast<SparePartStatus>()
                .Select(status => new SelectListItem
                {
                    Value = status.ToString(),
                    Text = status.GetDisplayName()
                }).ToList();

            // Populate Status dropdown
            ViewBag.StatusOptions = System.Enum.GetValues(typeof(Status))
                .Cast<Status>()
                .Select(status => new SelectListItem
                {
                    Value = status.ToString(),
                    Text = status.GetDisplayName()
                }).ToList();

            // Populate MachineType dropdown
            ViewBag.MachineTypeOptions = System.Enum.GetValues(typeof(MachineType))
                .Cast<MachineType>()
                .Select(mt => new SelectListItem
                {
                    Value = mt.ToString(),
                    Text = mt.GetDisplayName()
                }).ToList();

            // Populate ServiceType dropdown
            ViewBag.ServiceTypeOptions = System.Enum.GetValues(typeof(ServiceType))
                .Cast<ServiceType>()
                .Select(st => new SelectListItem
                {
                    Value = st.ToString(),
                    Text = st.GetDisplayName()
                }).ToList();

            return View(reportService);
        }

        // POST: ReportServices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeID,EngineerName,CompanyName,OtherCompanyName,MachineSerialNumber,LaserPower,CompanyAddress,MachineType,ServiceType,StartDate,EndDate,WorkingDays,SelectedAreas,Description,Diagnosis,CustomerFeedback,Status,SpareParts,SparePartStatus,CustomerSealSign,ImageBase64,CreatedDate,ModifiedDate")] ReportService reportService)
        {
            if (id != reportService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reportService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportServiceExists(reportService.Id))
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
            return View(reportService);
        }

        // GET: ReportServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportService = await _context.ReportService.FirstOrDefaultAsync(m => m.Id == id);
            if (reportService == null)
            {
                return NotFound();
            }

            return View(reportService);
        }

        // POST: ReportServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reportService = await _context.ReportService.FindAsync(id);
            if (reportService != null)
            {
                _context.ReportService.Remove(reportService);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }   

        private bool ReportServiceExists(int id)
        {
            return _context.ReportService.Any(e => e.Id == id);
        }
    }
}
