using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceReport.Data;
using ServiceReport.Models;

namespace ServiceReport.Controllers
{
    public class MasterDatasController : Controller
    {
        private readonly ServiceReportContext _context;

        public MasterDatasController(ServiceReportContext context)
        {
            _context = context;
        }

        // GET: MasterDatas
        public async Task<IActionResult> Index()
        {
            return View(await _context.MasterData.ToListAsync());
        }

        // GET: MasterDatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masterData = await _context.MasterData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (masterData == null)
            {
                return NotFound();
            }

            return View(masterData);
        }

        // GET: MasterDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MasterDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MachineSerialNumber,CompanyName,CompanyAddress,LaserPower,CreatedDate,ModifiedDate")] MasterData masterData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(masterData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(masterData);
        }
        [HttpGet]
        public async Task<IActionResult> GetMachineDetailsBySerialNumber(string machineSerialNumber)
        {
            if (string.IsNullOrEmpty(machineSerialNumber))
            {
                return Json(new { success = false });
            }

            // Assuming the master data contains MachineSerialNumber, CompanyName, CompanyAddress, and LaserPower
            var machine = await _context.MasterData
                                        .SingleOrDefaultAsync(m => m.MachineSerialNumber == machineSerialNumber);

            if (machine == null)
            {
                return Json(new { success = false });
            }

            return Json(new
            {
                success = true,
                companyName = machine.CompanyName,
                companyAddress = machine.CompanyAddress,
                laserPower = machine.LaserPower
            });
        }

        // GET: MasterDatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masterData = await _context.MasterData.FindAsync(id);
            if (masterData == null)
            {
                return NotFound();
            }
            return View(masterData);
        }

        // POST: MasterDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MachineSerialNumber,CompanyName,CompanyAddress,LaserPower,CreatedDate,ModifiedDate")] MasterData masterData)
        {
            if (id != masterData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(masterData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MasterDataExists(masterData.Id))
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
            return View(masterData);
        }

        // GET: MasterDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masterData = await _context.MasterData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (masterData == null)
            {
                return NotFound();
            }

            return View(masterData);
        }

        // POST: MasterDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var masterData = await _context.MasterData.FindAsync(id);
            if (masterData != null)
            {
                _context.MasterData.Remove(masterData);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MasterDataExists(int id)
        {
            return _context.MasterData.Any(e => e.Id == id);
        }
    }
}
