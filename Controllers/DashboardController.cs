using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceReport.Controllers
{
    public class DashboardController : Controller
    {
        //// GET: DashboardController
        //public ActionResult EngineerDashboard()
        //{
        //    return View();
        //}
        // Example for PDD Dashboard
            [Authorize(Roles = "Engineer")]
            public IActionResult PddDashboard()
            {
                // Check if user is logged in and has the correct department
                if (HttpContext.Session.GetString("Token") == null)
                {
                    return RedirectToAction("Login", "Users");
                }

                return View(); // Return the view for PDD Dashboard
            }

            // Example for Service Dashboard
            public IActionResult ServiceDashboard()
            {
                if (HttpContext.Session.GetString("Token") == null)
                {
                    return RedirectToAction("Login", "Users");
                }

                return View(); // Return the view for Service Dashboard
            }

            // Similarly for Marking, Robotics, Welding, etc.
            public IActionResult MarkingDashboard()
            {
                if (HttpContext.Session.GetString("Token") == null)
                {
                    return RedirectToAction("Login", "Users");
                }

                return View(); // Return the view for Marking Dashboard
            }

            // Implement for Robotics, Welding...
        


        // GET: DashboardController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DashboardController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DashboardController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DashboardController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DashboardController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DashboardController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DashboardController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
