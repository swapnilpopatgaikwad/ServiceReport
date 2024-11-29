using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceReport.Data;
using ServiceReport.Models;

namespace ServiceReport.Controllers
{
    public class UserRolesController : Controller
    {
        private readonly ServiceReportContext _context;

        public UserRolesController(ServiceReportContext context)
        {
            _context = context;
        }

        // GET: UserRole/Index
        public async Task<IActionResult> Index()
        {
            var userRoles = await _context.UserRole
                .Include(u => u.Role)
                .Include(u => u.User)
                .Select(ur => new UserRoleModel
                {
                    UserId = ur.UserId,
                    RoleId = ur.RoleId,
                    UserName = ur.User.Username,
                    RoleName = ur.Role.RoleName,
                    CreatedDate = ur.CreatedDate,
                    ModifiedDate = ur.ModifiedDate,
                })
                .ToListAsync();

            return View(userRoles); // Ensure userRoles is IEnumerable<UserRole>
        }


        // GET: UserRole/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRole = await _context.UserRole
                .Include(u => u.Role)
                .Include(u => u.User)
                .Select(ur => new UserRoleModel
                {
                    UserId = ur.UserId,
                    RoleId = ur.RoleId,
                    UserName = ur.User.Username,
                    RoleName = ur.Role.RoleName
                })
                .FirstOrDefaultAsync(m => m.UserId == id);

            if (userRole == null)
            {
                return NotFound();
            }

            return View(userRole);
        }

        // GET: UserRole/Create
        public IActionResult Create()
        {
            ViewBag.Users = new SelectList(_context.User, "Id", "Username");
            ViewBag.Roles = new SelectList(_context.Role, "Id", "RoleName");

            return View(new AssignUserRoleModel() ); 
        }

        //POST: UserRole/Create
       [HttpPost]
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,RoleId")] AssignUserRoleModel userRole)
        {
            if (ModelState.IsValid)
            {
                if (!_context.UserRole.Any(ur => ur.UserId == userRole.UserId && ur.RoleId == userRole.RoleId))
                {
                    _context.Add(new UserRole
                    {
                        UserId = userRole.UserId,
                        RoleId = userRole.RoleId,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    });
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "This user already has the selected role.");
            }
            ViewBag.Users = new SelectList(_context.User, "Id", "Username");
            ViewBag.Roles = new SelectList(_context.Role, "Id", "RoleName");
            //ViewBag.Users = new SelectList(_context.User.Select(u => new { u.Id, u.Username }), "Id", "Username", userRole.UserId);
            //ViewBag.Roles = new SelectList(_context.Role.Select(r => new { r.Id, r.RoleName }), "Id", "RoleName", userRole.RoleId);
            return View(userRole);
        }
        // GET: UserRole/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRole = await _context.UserRole
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (userRole == null)
            {
                return NotFound();
            }

            ViewData["RoleId"] = new SelectList(_context.Role, "Id", "RoleName", userRole.RoleId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Username", userRole.UserId);

            return View(userRole);
        }

        // POST: UserRole/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,RoleId,CreatedDate")] UserRole userRole)
        {
            if (id != userRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    userRole.ModifiedDate = DateTime.Now;
                    _context.Update(userRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRoleExists(userRole.Id))
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
            ViewData["RoleId"] = new SelectList(_context.Role, "Id", "RoleName", userRole.RoleId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Username", userRole.UserId);
            return View(userRole);
        }

        private bool UserRoleExists(int id) => throw new NotImplementedException();

        // GET: UserRole/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRole = await _context.UserRole
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (userRole == null)
            {
                return NotFound();
            }

            return View(userRole);
        }

        // POST: UserRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userRole = await _context.UserRole.FindAsync(id);
            if (userRole != null)
            {
                _context.UserRole.Remove(userRole);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
