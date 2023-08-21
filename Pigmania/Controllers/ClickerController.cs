using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pigmania.Data;
using Pigmania.Models;

namespace Pigmania.Controllers
{
    public class ClickerController : Controller
    {
        private readonly ILogger<ClickerController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _um;
        private readonly RoleManager<IdentityRole> _rm;

        public ClickerController(ILogger<ClickerController> logger, ApplicationDbContext context,UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
        {
            _logger = logger;
            _context = context;
            _um = um;
            _rm = rm;

        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }
        


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Add([Bind("TruffleCoins")] ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                user = _um.GetUserAsync(User).Result;
                user.TruffleCoins = user.TruffleCoins;
                if (user.TruffleClickPower == 0)
                {
                    user.TruffleClickPower = user.TruffleClickPower + 1;
                }
                user.TruffleCoins = Convert.ToInt32(user.TruffleCoins + user.TruffleClickPower);
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Add));
            }
            return View(user);
        }

        [Authorize]
        public IActionResult EditClickPower()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> EditClickPower([Bind("TruffleClickPower")] ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                user = _um.GetUserAsync(User).Result;
                user.TruffleCoins = user.TruffleCoins;
                double priceTemp = 2500 * user.TruffleClickPower / 1.8;
                int price = Convert.ToInt32(priceTemp);
                if (user.TruffleCoins >= price)
                {
                    user.TruffleCoins = user.TruffleCoins - price;
                    user.TruffleClickPower = user.TruffleClickPower;
                    user.TruffleClickPower = user.TruffleClickPower + 1;
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Add));
                }
            }

            return View(user);
        }
        
        
    }
}
