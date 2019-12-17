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
using Microsoft.Extensions.Configuration;
using _7Element.Email;
using Microsoft.Extensions.Options;

namespace _7Element.Controllers
{
    public class PredsGamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        private string emailSecret = null;
        private string password = null;

        public PredsGamesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // GET: PredsGames
        public async Task<IActionResult> Index()
        {
            var date = DateTime.Now;
            return View(await _context.PredsGame.Where(d => d.DateTime > date && d.Open == true).ToListAsync());
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
                return RedirectToAction(nameof(ConfirmationApply));
            }
            return View(pgdvm);
        }
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(PredsGameDetailViewModel pgdvm)
        {
            var userPredsGame = await _context.UserPredsGame.FirstOrDefaultAsync(m => m.UserId == pgdvm.userId);
            if (ModelState.IsValid)
            {
                _context.Remove(userPredsGame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Index));
        }
        public async Task<IActionResult> ConfirmationApply()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> End(PredsGameDetailViewModel pgdvm)
        {
            var donatedTickets = await _context.DonatedTickets.Where(d => d.PredsGameId == pgdvm.predsGameId && d.TransactionComplete == true).ToListAsync();
            var userPredsGames = await _context.UserPredsGame.Where(d => d.PredsGameId == pgdvm.predsGameId).ToListAsync();
            var predsGame = await _context.PredsGame.FirstOrDefaultAsync(g => g.PredsGameId == pgdvm.predsGameId);
            predsGame.Open = false;
            List<UserPredsGame> winners = new List<UserPredsGame>();
            if (userPredsGames.Count < donatedTickets.Count)
            {
                for (int i = 0; i < userPredsGames.Count; i++)
                {
                    userPredsGames[i].DonatedTicketsId = donatedTickets[i].DonatedTicketsId;
                };
                foreach (var upg in userPredsGames)
                {
                    _context.UserPredsGame.Update(upg);
                    var user = await _userManager.FindByIdAsync(upg.UserId);
                    var tickets = await _context.Ticket.Where(t => t.DonatedTicketsId == upg.DonatedTicketsId).ToListAsync();
                    await SendEmail(user, tickets, predsGame);
                };
                _context.PredsGame.Update(predsGame);
                await _context.SaveChangesAsync();
            }
            else
            {
                for (int i = 0; i < donatedTickets.Count; i++)
                {
                    Random rnd = new Random();
                    int winningNumber = rnd.Next(0, (donatedTickets.Count() + 1));
                    userPredsGames[winningNumber].DonatedTicketsId = donatedTickets[i].DonatedTicketsId;
                    userPredsGames[i].DonatedTicketsId = donatedTickets[i].DonatedTicketsId;
                    winners.Add(userPredsGames[winningNumber]);
                    userPredsGames.RemoveAt(winningNumber);
                }
                foreach (var upg in winners)
                {
                    _context.UserPredsGame.Update(upg);
                    var user = await _userManager.FindByIdAsync(upg.UserId);
                    var tickets = await _context.Ticket.Where(t => t.DonatedTicketsId == upg.DonatedTicketsId).ToListAsync();
                    await SendEmail(user, tickets, predsGame);
                };
                _context.PredsGame.Update(predsGame);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
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
        private async Task SendEmail(ApplicationUser user, List<Ticket> tickets, PredsGame game)
        {
            emailSecret = Configuration["EmailSecrets:Email"];
            password = Configuration["EmailSecrets:Password"];
            EmailSettings es = new EmailSettings()
            {
                PrimaryDomain = "smtp.gmail.com",
                PrimaryPort = 587,
                SecondayDomain = "smtp.gmail.com",
                SecondaryPort = 587,
                UsernameEmail = emailSecret,
                UsernamePassword = password,
                FromEmail = "sevenelementtest@gmail.com",
                ToEmail = user.Email
            };
            string email = user.Email;
            string title = "7Element Tickets";
            string body = $"<h2>Dear {user.FirstName} {user.LastName},</h2><br>" +
                $"<p>Congrats you have won {tickets.Count} tickets:</p><br><ol>";
            for (int i = 0; i < tickets.Count; i++)
            {
                body += $"<li>Section {tickets[i].Section} Row {tickets[i].Row} Seat {tickets[i].Seat}</li>";
            }
            body += $"</ol><p>For the Nashville Predators game on the {game.DateTime} vs the {game.Opponent}. </p><br>" +
                "<h3>Thank you,</h3>" +
                "<h2>7Element</h2>";
            AuthMessageSender ams = new AuthMessageSender(Options.Create(es));
            await ams.SendEmailAsync(email, title, body);
        }
    }
}
