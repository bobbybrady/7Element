using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _7Element.Data;
using _7Element.Models;
using Microsoft.AspNetCore.Identity;
using _7Element.Models.ViewModels;

namespace _7Element.Controllers
{
    public class PredsGamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PredsGamesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: PredsGames
        public async Task<IActionResult> Index()
        {
            var date = DateTime.Now;
            return View(await _context.PredsGame.Where(d => d.DateTime > date).ToListAsync());
        }

        // GET: PredsGames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var predsGame = await _context.PredsGame
                .FirstOrDefaultAsync(m => m.PredsGameId == id);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userPredsGame = await _context.UserPredsGame.FirstOrDefaultAsync(d => d.UserId == user.Id && d.PredsGameId == predsGame.PredsGameId);
            if (predsGame == null)
            {
                return NotFound();
            }
            PredsGameDetailViewModel pgdvm = new PredsGameDetailViewModel()
            {
                User = user,
                PredsGame = predsGame,
                UserPredsGame = userPredsGame,
                userId = user.Id,
                predsGameId = predsGame.PredsGameId
            };

            return View(pgdvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apply(PredsGameDetailViewModel pgdvm)
        {
            UserPredsGame UserPredsGame = new UserPredsGame()
            {
                UserId = pgdvm.userId,
                PredsGameId = pgdvm.predsGameId,
                DonatedTicketsId = null
            };
            if (ModelState.IsValid)
            {
                _context.Add(UserPredsGame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details));
            }
            return View(pgdvm);
        }

        // GET: PredsGames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PredsGames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PredsGameId,DateTime,Opponent")] PredsGame predsGame)
        {
            if (ModelState.IsValid)
            {
                _context.Add(predsGame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(predsGame);
        }

        // GET: PredsGames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var predsGame = await _context.PredsGame.FindAsync(id);
            if (predsGame == null)
            {
                return NotFound();
            }
            return View(predsGame);
        }

        // POST: PredsGames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PredsGameId,DateTime,Opponent")] PredsGame predsGame)
        {
            if (id != predsGame.PredsGameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(predsGame);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PredsGameExists(predsGame.PredsGameId))
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
            return View(predsGame);
        }

        // GET: PredsGames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var predsGame = await _context.PredsGame
                .FirstOrDefaultAsync(m => m.PredsGameId == id);
            if (predsGame == null)
            {
                return NotFound();
            }

            return View(predsGame);
        }

        // POST: PredsGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var predsGame = await _context.PredsGame.FindAsync(id);
            _context.PredsGame.Remove(predsGame);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PredsGameExists(int id)
        {
            return _context.PredsGame.Any(e => e.PredsGameId == id);
        }
    }
}
