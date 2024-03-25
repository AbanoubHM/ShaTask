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
    public class InvoiceController : Controller {
        private readonly ShaTaskContext _context;

        public InvoiceController(ShaTaskContext context) {
            _context = context;
        }

        // GET: Invoice
        public async Task<IActionResult> Index() {
            var shaTaskContext = _context.InvoiceHeaders.Include(i => i.Branch).Include(i => i.Cashier).Include(d => d.InvoiceDetails);
            return View(await shaTaskContext.ToListAsync());
        }

        // GET: Invoice/Details/5
        public async Task<IActionResult> Details(long? id) {
            if (id == null) {
                return NotFound();
            }

            var invoiceHeader = await _context.InvoiceHeaders
                .Include(i => i.Branch)
                .Include(i => i.Cashier)
                .Include(d => d.InvoiceDetails)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceHeader == null) {
                return NotFound();
            }

            return View(invoiceHeader);
        }

        // GET: Invoice/Create
        public IActionResult Create() {
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "BranchName");
            ViewData["CashierId"] = new SelectList(_context.Cashiers, "Id", "CashierName");
            return View();
        }

        // POST: Invoice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerName,Invoicedate,CashierId,BranchId,InvoiceDetails")] InvoiceHeader invoiceHeader) {
            _context.Add(invoiceHeader);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Invoice/Edit/5
        public async Task<IActionResult> Edit(long? id) {
            if (id == null) {
                return NotFound();
            }

            var invoiceHeader = await _context.InvoiceHeaders.Include(o => o.InvoiceDetails).FirstOrDefaultAsync(x => x.Id == id);
            if (invoiceHeader == null) {
                return NotFound();
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "BranchName", invoiceHeader.BranchId);
            ViewData["CashierId"] = new SelectList(_context.Cashiers, "Id", "CashierName", invoiceHeader.CashierId);
            return View(invoiceHeader);
        }

        // POST: Invoice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CustomerName,Invoicedate,CashierId,BranchId,InvoiceDetails")] InvoiceHeader invoiceHeader) {
            if (id != invoiceHeader.Id) {
                return NotFound();
            }
            await _context.InvoiceDetails.Where(w=>(!invoiceHeader.InvoiceDetails.Select(o=>o.Id).Contains(w.Id))&&w.InvoiceHeaderId.Equals(invoiceHeader.Id)).ExecuteDeleteAsync();
            try {

                _context.Update(invoiceHeader);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!InvoiceHeaderExists(invoiceHeader.Id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: Invoice/Delete/5
        public async Task<IActionResult> Delete(long? id) {
            if (id == null) {
                return NotFound();
            }

            var invoiceHeader = await _context.InvoiceHeaders
                .Include(i => i.Branch)
                .Include(i => i.Cashier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceHeader == null) {
                return NotFound();
            }

            return View(invoiceHeader);
        }

        // POST: Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id) {
            var invoiceHeader = await _context.InvoiceHeaders.FindAsync(id);
            if (invoiceHeader != null) {
                _context.InvoiceHeaders.Remove(invoiceHeader);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceHeaderExists(long id) {
            return _context.InvoiceHeaders.Any(e => e.Id == id);
        }
    }
}
