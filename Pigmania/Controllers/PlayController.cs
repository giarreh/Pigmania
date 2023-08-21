using Pigmania.Data;
using Pigmania.Models;



using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Pigmania.Controllers
{
    public class PlayController : Controller
    {
        private readonly ILogger<PlayController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _um;
        private readonly RoleManager<IdentityRole> _rm;

        public PlayController(ILogger<PlayController> logger, ApplicationDbContext context,UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
        {
            _logger = logger;
            _context = context;
            _um = um;
            _rm = rm;

        }

       
        // GET
        // GET: pig/Add
        [Authorize]
        public async Task<IActionResult> Play()
        {
            return View(await _context.Pigs.ToListAsync());
        }
        [Authorize]
        public async Task<IActionResult> MoneyTransaction()
        {
            return View();
        }  
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        
        public async Task<IActionResult> MoneyTransaction([Bind("TruffleCoins")] ApplicationUser user, int amount)
        {
            if (ModelState.IsValid)
            {
                user = _um.GetUserAsync(User).Result;
                user.TruffleCoins = user.TruffleCoins;
                if (amount > 0)
                {
                    user.TruffleCoins = user.TruffleCoins + amount;
                }
                if (amount < 0)
                {
                    user.TruffleCoins = user.TruffleCoins - amount;
                }
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                // return RedirectToAction(nameof(Play));
            }
            return View(user);
        }
        
    }
}