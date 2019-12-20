using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _7Element.Data;
using _7Element.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using _7Element.Models.ViewModels;

namespace _7Element.Controllers
{
    [Authorize]
    public class PickupGamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PickupGamesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: PickupGames
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var pickupGamesList = await _context.PickupGame.ToListAsync();
            var pgvm = new PickupGamesViewModel()
            {
                User = user,
                PickupGames = pickupGamesList.OrderBy(pg => pg.DateTime).ToList()
            };
            pgvm.PickupGameManager();
            return View(pgvm);
        }

        // GET: PickupGames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pickupGame = await _context.PickupGame
                .FirstOrDefaultAsync(m => m.PickupGameId == id);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var UserPickupGamesList = await _context.UserPickupGame.Where(m => m.PickupGameId == pickupGame.PickupGameId).ToListAsync();
            var userList = new List<ApplicationUser>();
            var userPickupGame = await _context.UserPickupGame.FirstOrDefaultAsync(u => u.UserId == user.Id && u.PickupGameId == pickupGame.PickupGameId);
            foreach (var upg in UserPickupGamesList)
            {
                var u = await _userManager.FindByIdAsync(upg.UserId);
                userList.Add(u);
            }
            if (pickupGame == null)
            {
                return NotFound();
            }
            PickupGameDetailViewModel pgdvm = new PickupGameDetailViewModel()
            {
                User = user,
                Users = userList,
                PickupGame = pickupGame,
                userPickupGames = UserPickupGamesList,
                UserPickupGame = userPickupGame
            };

            return View(pgdvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apply(int PickupGameId, int MaxSkaters, int MaxGoalies)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var numberOfApplicants = await _context.PickupGame.Where(m => m.PickupGameId == PickupGameId).ToListAsync();
            if (user.Position == "Skater")
            {
                UserPickupGame upg = new UserPickupGame()
                {
                    PickupGameId = PickupGameId,
                    UserId = user.Id,
                    IsStandby = (numberOfApplicants.Count < MaxSkaters) ? false : true,
                    DateTime = DateTime.Now
                };

                if (ModelState.IsValid)
                {
                    _context.Add(upg);
                    await _context.SaveChangesAsync();
                    return Redirect($"/PickupGames/Details/{PickupGameId}");
                }

            }
            else
            {
                UserPickupGame upg = new UserPickupGame()
                {
                    PickupGameId = PickupGameId,
                    UserId = user.Id,
                    IsStandby = (numberOfApplicants.Count < MaxGoalies) ? false : true,
                    DateTime = DateTime.Now
                };

                if (ModelState.IsValid)
                {
                    _context.Add(upg);
                    await _context.SaveChangesAsync();
                    return Redirect($"/PickupGames/Details/{PickupGameId}");
                }
            }
            return Redirect("/home");
        }

        public async Task<IActionResult> Deny(int PickupGameId)
        {
            var toDelete = await _context.UserPickupGame.FirstOrDefaultAsync(m => m.PickupGameId == PickupGameId);
            _context.UserPickupGame.Remove(toDelete);
            await _context.SaveChangesAsync();
            return Redirect($"/PickupGames/Details/{toDelete.PickupGameId}");
        }
        // GET: PickupGames/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user.IsAdmin == true)
            {
                return View();
            }
            return Redirect("/home");
        }

        // POST: PickupGames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PickupGameId,MaxSkaters,MaxGoalies,DateTime,Location,Title")] PickupGame pickupGame)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user.IsAdmin == true)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(pickupGame);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(pickupGame);
            }
            return Redirect("/home");
        }

        // GET: PickupGames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user.IsAdmin == true)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var pickupGame = await _context.PickupGame.FindAsync(id);
                if (pickupGame == null)
                {
                    return NotFound();
                }
                return View(pickupGame);
            }
            return Redirect("/home");
        }

        // POST: PickupGames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PickupGameId,MaxSkaters,MaxGoalies,DateTime,Location,Title")] PickupGame pickupGame)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user.IsAdmin == true)
            {
                if (id != pickupGame.PickupGameId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(pickupGame);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PickupGameExists(pickupGame.PickupGameId))
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
                return View(pickupGame);
            }
            return Redirect("/home");
        }

        // GET: PickupGames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user.IsAdmin == true)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var pickupGame = await _context.PickupGame
                    .FirstOrDefaultAsync(m => m.PickupGameId == id);
                if (pickupGame == null)
                {
                    return NotFound();
                }

                return View(pickupGame);
            }
            return Redirect("/home");
        }

        // POST: PickupGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user.IsAdmin == true)
            {
                var pickupGame = await _context.PickupGame.FindAsync(id);
                _context.PickupGame.Remove(pickupGame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Redirect("/home");
        }

        private bool PickupGameExists(int id)
        {
            return _context.PickupGame.Any(e => e.PickupGameId == id);
        }
    }
}
