using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pigmania.Data;
using Pigmania.Models;

namespace Pigmania.Controllers
{
    public class PigController : Controller
    {
        private readonly ILogger<PigController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _um;
        private readonly RoleManager<IdentityRole> _rm;

        public PigController(ILogger<PigController> logger, ApplicationDbContext context,UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
        {
            _logger = logger;
            _context = context;
            _um = um;
            _rm = rm;

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pigs.ToListAsync());
        }

        // GET
        // GET: pig/Add
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([Bind("Id,Name,Speed")] Pig pig)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pig);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(pig);
        }

        // GET: Pig/Edit/5

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            var pig = await _context.Pigs.FindAsync(id);

            if (id == null) return NotFound();
            if (pig == null) return NotFound();


            return View(pig);
        }

        // POST: Pig/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Speed")] Pig pig)
        {
            if (id != pig.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pig);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PigExists(pig.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(pig);
        }

        [Authorize(Roles = "Admin")]
        private bool PigExists(int id)
        {
            return _context.Pigs.Any(e => e.Id == id);
        }

        // GET: Pig/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pig = await _context.Pigs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pig == null)
            {
                return NotFound();
            }

            return View(pig);
        }

        // POST: Pig/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pig = await _context.Pigs.FindAsync(id);
            if (pig != null) _context.Pigs.Remove(pig);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
}