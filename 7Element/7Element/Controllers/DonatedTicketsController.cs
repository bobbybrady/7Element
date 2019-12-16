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
using Microsoft.AspNetCore.Identity;
using _7Element.Models.ViewModels;
using _7Element.Email;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace _7Element.Controllers
{
    [Authorize]
    public class DonatedTicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        private string emailSecret = null;
        private string password = null;
        public DonatedTicketsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // GET: DonatedTickets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DonatedTickets.Include(d => d.PredsGame).Include(d => d.User).OrderBy(d => d.TransactionComplete);
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
        public async Task<IActionResult> Approve(int id)
        {
            var dt = await _context.DonatedTickets.FirstOrDefaultAsync(m => m.DonatedTicketsId == id);
            dt.TransactionComplete = true;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonatedTicketsExists(dt.DonatedTicketsId))
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
            return View(dt);
        }
        public async Task<IActionResult> Deny(int id)
        {
            var dt = await _context.DonatedTickets.FirstOrDefaultAsync(m => m.DonatedTicketsId == id);
            dt.TransactionComplete = false;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonatedTicketsExists(dt.DonatedTicketsId))
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
            return View(dt);
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
            List<SelectListItem> NumberOfTickets = new List<SelectListItem>()
            {
                new SelectListItem() {Text="2", Value="2"},
                new SelectListItem() { Text="3", Value="3"},
                new SelectListItem() {Text="4", Value="4"},
                new SelectListItem() { Text="5", Value="5"},
                new SelectListItem() {Text="6", Value="6"},
                new SelectListItem() { Text="7", Value="7"},
                new SelectListItem() {Text="8", Value="8"},
                new SelectListItem() { Text="9", Value="9"},
                new SelectListItem() {Text="10", Value="10"}
            };

            ViewData["NumberOfTickets"] = NumberOfTickets;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            DonatedTicketsCreateViewModel dtcvm = new DonatedTicketsCreateViewModel()
            {
                user = user
            };
            return View(dtcvm);
        }

        // POST: DonatedTickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DonatedTicketsCreateViewModel dtcvm)
        {
            ModelState.Remove("DonatedTickets.UserId");
            ModelState.Remove("DonatedTickets.User");
            ModelState.Remove("DonatedTickets.PredsGame");
            for (int i = 0; i < dtcvm.Tickets.Count; i++)
            {
                ModelState.Remove($"Tickets[{i}].DonatedTickets");
            }
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                dtcvm.DonatedTickets.User = user;
                dtcvm.DonatedTickets.UserId = user.Id;
                _context.Add(dtcvm.DonatedTickets);
                await _context.SaveChangesAsync();
                foreach (var ticket in dtcvm.Tickets)
                {
                    ticket.DonatedTicketsId = dtcvm.DonatedTickets.DonatedTicketsId;
                    _context.Add(ticket);
                }
                await _context.SaveChangesAsync();
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
                string title = "7Element Donation";
                string body = $"<h2>Dear {user.FirstName} {user.LastName},</h2><br>" +
                    $"<p>This email is to confirm your desired donation to for the following {dtcvm.NumberOfTickets} tickets:</p><br><ol>";
                for (int i = 0; i < dtcvm.Tickets.Count; i++)
                {
                    body += $"<li>Section {dtcvm.Tickets[i].Section} Row {dtcvm.Tickets[i].Row} Seat {dtcvm.Tickets[i].Seat}</li>";
                }
                body += "</ol><p>To complete your donation please dontate the tickets to tickemaster with the username sevenelementtest@gmail.com.</p><br>" +
                    "<h3>Thank you,</h3>" +
                    "<h2>7Element</h2>";
                AuthMessageSender ams = new AuthMessageSender(Options.Create(es));
                await ams.SendEmailAsync(email, title, body);
                return RedirectToAction(nameof(Confirmation));
            }
            ViewData["PredsGameId"] = new SelectList(_context.PredsGame, "PredsGameId", "PredsGameId", dtcvm.DonatedTickets.PredsGameId);
            return View();
        }
        public async Task<IActionResult> Confirmation()
        {
            return View();
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
