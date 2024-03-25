using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShaTask.DbModels;

namespace ShaTask.Controllers {
    [Authorize]
    public class CashierController : Controller {
        private readonly ShaTaskContext _context;

        public CashierController(ShaTaskContext context) {
            _context = context;
        }

        // GET: Cashier
        public async Task<IActionResult> Index() {
            var shaTaskContext = _context.Cashiers.Include(c => c.Branch);
            return View(await shaTaskContext.ToListAsync());
        }

        // GET: Cashier/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var cashier = await _context.Cashiers
                .Include(c => c.Branch)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cashier == null) {
                return NotFound();
            }

            return View(cashier);
        }

        // GET: Cashier/Create
        public IActionResult Create() {
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "BranchName");
            return View();
        }

        // POST: Cashier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CashierName,BranchId")] Cashier cashier) {
            _context.Add(cashier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Cashier/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var cashier = await _context.Cashiers.FindAsync(id);
            if (cashier == null) {
                return NotFound();
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "BranchName", cashier.BranchId);
            return View(cashier);
        }

        // POST: Cashier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CashierName,BranchId")] Cashier cashier) {
            if (id != cashier.Id) {
                return NotFound();
            }

            try {
                _context.Update(cashier);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!CashierExists(cashier.Id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: Cashier/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var cashier = await _context.Cashiers
                .Include(c => c.Branch)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cashier == null) {
                return NotFound();
            }

            return View(cashier);
        }

        // POST: Cashier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var cashier = await _context.Cashiers.FindAsync(id);
            if (cashier != null) {
                _context.Cashiers.Remove(cashier);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CashierExists(int id) {
            return _context.Cashiers.Any(e => e.Id == id);
        }
    }
}