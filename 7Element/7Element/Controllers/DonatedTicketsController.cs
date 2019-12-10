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
using System.Net.Http;

namespace _7Element.Controllers
{
    [Authorize]
    public class DonatedTicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonatedTicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DonatedTickets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DonatedTickets.Include(d => d.PredsGame).Include(d => d.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DonatedTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donatedTickets = await _context.DonatedTickets
                .Include(d => d.PredsGame)
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.DonatedTicketsId == id);
            if (donatedTickets == null)
            {
                return NotFound();
            }

            return View(donatedTickets);
        }

        // GET: DonatedTickets/Create
        public async Task<IActionResult> Create()
        {
            DateTime date = DateTime.Now.AddDays(7);
            var currentDateTime = DateTime.Now;
            var predsGames = await _context.PredsGame.Where(pg => pg.DateTime > currentDateTime && pg.DateTime < date).ToListAsync();
            List<SelectListItem> Positions = new List<SelectListItem>();
            foreach (var pg in predsGames)
            {
                Positions.Add(new SelectListItem() { Text = ($"{pg.DateTime} {pg.Opponent}"), Value = pg.PredsGameId.ToString() });
            }
            ViewData["PredsGameId"] = Positions;

            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: DonatedTickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonatedTicketsId,UserId,PredsGameId,EmailAddress,EmailTitle,EmailBody,TransactionComplete")] DonatedTickets donatedTickets)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donatedTickets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PredsGameId"] = new SelectList(_context.PredsGame, "PredsGameId", "PredsGameId", donatedTickets.PredsGameId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", donatedTickets.UserId);
            return View(donatedTickets);
        }

        // GET: DonatedTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donatedTickets = await _context.DonatedTickets.FindAsync(id);
            if (donatedTickets == null)
            {
                return NotFound();
            }
            ViewData["PredsGameId"] = new SelectList(_context.PredsGame, "PredsGameId", "PredsGameId", donatedTickets.PredsGameId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", donatedTickets.UserId);
            return View(donatedTickets);
        }

        // POST: DonatedTickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonatedTicketsId,UserId,PredsGameId,EmailAddress,EmailTitle,EmailBody,TransactionComplete")] DonatedTickets donatedTickets)
        {
            if (id != donatedTickets.DonatedTicketsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donatedTickets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonatedTicketsExists(donatedTickets.DonatedTicketsId))
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
            ViewData["PredsGameId"] = new SelectList(_context.PredsGame, "PredsGameId", "PredsGameId", donatedTickets.PredsGameId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", donatedTickets.UserId);
            return View(donatedTickets);
        }

        // GET: DonatedTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donatedTickets = await _context.DonatedTickets
                .Include(d => d.PredsGame)
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.DonatedTicketsId == id);
            if (donatedTickets == null)
            {
                return NotFound();
            }

            return View(donatedTickets);
        }

        // POST: DonatedTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donatedTickets = await _context.DonatedTickets.FindAsync(id);
            _context.DonatedTickets.Remove(donatedTickets);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonatedTicketsExists(int id)
        {
            return _context.DonatedTickets.Any(e => e.DonatedTicketsId == id);
        }

        private async Task<string> GetSchedule()
        {
            string date = DateTime.Now.AddDays(7).ToString("yy.MM.dd");
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://statsapi.web.nhl.com/api/v1/schedule?teamId=18&endDate={date}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
