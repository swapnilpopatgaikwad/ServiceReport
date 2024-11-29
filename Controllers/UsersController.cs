using ServiceReport.Data;
using ServiceReport.Helper;
using ServiceReport.Interface;
using ServiceReport.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NuGet.Common;
using ServiceReport.Global;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceReport.Enum;

namespace ServiceReport.Controllers
{
    public class UsersController : Controller
    {
        private readonly ServiceReportContext _context;
        private readonly IJwtAuthenticationService _jwtAuthenticationService;

      

        public UsersController(ServiceReportContext context, IJwtAuthenticationService jwtAuthenticationService)
        {
            _context = context;
            _jwtAuthenticationService = jwtAuthenticationService;
        }


        public IActionResult Register()
        {
            return View(new UserRegisterModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (await _context.User.AnyAsync(u => u.Username == model.Username))
            {
                ModelState.AddModelError(string.Empty, "Username already exists.");
                return View(model);
            }

            var user = new User
            {
                EmplyoeeID = model.EmplyoeeID,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                ContactNumber = model.ContactNumber,
                Password = model.Password,
                Username = model.Username,
                Department = model.Department,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "User registered successfully.";
            return RedirectToAction("Register");
        }



        //User login
        public IActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(UserLoginModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _context.User
        //                                  .Include(x => x.UserRoles)
        //                                  .ThenInclude(x => x.Role)
        //                                  .SingleOrDefaultAsync(u => u.Username == model.Username);

        //        if (user == null || !(model.Password == user.Password))
        //        {
        //            TempData["ErrorMessage"] = "Invalid username or password. Please try again.";
        //            return RedirectToAction("Login");
        //        }

        //        // Generate token and store it in the session
        //        var token = _jwtAuthenticationService.GenerateToken(user);
        //        HttpContext.Session.SetString("Token", token);

        //        // Store success message with the user's name
        //        TempData["SuccessMessage"] = $"Login successful! Welcome back, {user.FirstName} {user.LastName}.";

        //        // Redirect based on user role
        //        if (user.UserRoles.Any(x => x.Role.RoleName == GlobalSetting.Engineer))
        //        {
        //            return RedirectToAction("EngineerDashboard", "Dashboard");
        //        }  

        //        if (user.UserRoles.Any(x => x.Role.RoleName == GlobalSetting.Admin || x.Role.RoleName == GlobalSetting.SuperAdmin))
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }

        //        return RedirectToAction(nameof(Index), "Home");
        //    }

        //    // If model state is invalid, return with an error
        //    TempData["ErrorMessage"] = "Please fill in all required fields.";
        //    return View(model);
        //}

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Login(UserLoginModel model)
{
    if (ModelState.IsValid)
    {
        var user = await _context.User
                                  .Include(x => x.UserRoles)
                                  .ThenInclude(x => x.Role)
                                  .SingleOrDefaultAsync(u => u.Username == model.Username);

        if (user == null || !(model.Password == user.Password))
        {
            TempData["ErrorMessage"] = "Invalid username or password. Please try again.";
            return RedirectToAction("Login");
        }

        // Generate token and store it in the session
        var token = _jwtAuthenticationService.GenerateToken(user);
        HttpContext.Session.SetString("Token", token);

        // Store success message with the user's name
        TempData["SuccessMessage"] = $"Login successful! Welcome back, {user.FirstName} {user.LastName}.";

        // Redirect based on user role and department
        if (user.UserRoles.Any(x => x.Role.RoleName == GlobalSetting.Engineer))
        {
            // Check department and redirect accordingly
            switch (user.Department)
            {
                case "PDD":
                case "SERVICE":
                    return RedirectToAction("ServiceDashboard", "Dashboard");
                case "MARKING":
                    return RedirectToAction("MarkingDashboard", "Dashboard");
                case "Robotics":
                    return RedirectToAction("RoboticsDashboard", "Dashboard");
                case "Welding":
                    return RedirectToAction("WeldingDashboard", "Dashboard");
                default:
                    return RedirectToAction("Index", "Home");
            }
        }

        if (user.UserRoles.Any(x => x.Role.RoleName == GlobalSetting.Admin || x.Role.RoleName == GlobalSetting.SuperAdmin))
        {
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction(nameof(Index), "Home");
    }

    // If model state is invalid, return with an error
    TempData["ErrorMessage"] = "Please fill in all required fields.";
    return View(model);
}


        //// fetch username from employeeid 
        [HttpGet]
        public async Task<IActionResult> GetUsernameByEmployeeId(string employeeId)
        {
            if (string.IsNullOrEmpty(employeeId))
            {
                return Json(new { success = false });
            }

            // Assuming your user entity has EmployeeId and Username fields
            var user = await _context.User
                                     .SingleOrDefaultAsync(u => u.EmplyoeeID == employeeId);

            if (user == null)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true, username = user.Username });
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "Logout successful! See you again.";
            return RedirectToAction("Index", "Home");
        }

        [CustomAuthorize(GlobalSetting.Admin, GlobalSetting.SuperAdmin)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }


        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,ContactNumber,Password,Username,EmployeeID,Department,Id")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,Email,ContactNumber,Password,Username,EmployeeID,Department,Id")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.ModifiedDate = DateTime.Now;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }

            await _context.SaveChangesAsync();  
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}

