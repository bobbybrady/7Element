using System;
using System.Collections.Generic;
using System.Text;
using _7Element.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _7Element.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<DonatedTickets> DonatedTickets { get; set; }
        public DbSet<PickupGame> PickupGame { get; set; }
        public DbSet<PlayerStats> PlayerStats { get; set; }
        public DbSet<PredsGame> PredsGame { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<UserPickupGame> UserPickupGame { get; set; }
        public DbSet<UserPredsGame> UserPredsGame { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserPickupGame>()
                .Property(b => b.DateTime)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<PickupGame>()
                .HasMany(o => o.UserPickupGames)
                .WithOne(l => l.PickupGame)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PickupGame>()
                .HasMany(o => o.PlayerStats)
                .WithOne(l => l.PickupGame)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PredsGame>()
                .HasMany(o => o.UserPredsGames)
                .WithOne(l => l.PredsGame)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PredsGame>()
                .HasMany(o => o.DonatedTickets)
                .WithOne(l => l.PredsGame)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserPredsGame>()
                .HasOne(s => s.User)
                .WithMany(s => s.UserPredsGames)
                .OnDelete(DeleteBehavior.Restrict);


            ApplicationUser user = new ApplicationUser
            {
                FirstName = "Admina",
                LastName = "Straytor",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff",
                Position = "Goalie",
                IsAdmin = true,
                IsVeteran = true
            };
            ApplicationUser anotherUser = new ApplicationUser
            {
                FirstName = "Bobby",
                LastName = "Brady",
                UserName = "bobby@bobby.com",
                NormalizedUserName = "BOBBY@BOBBY.COM",
                Email = "bobby@bobby.com",
                NormalizedEmail = "BOBBY@BOBBY.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794123",
                Id = "00000000-ffff-ffff-ffff-fffffffff123",
                Position = "Forward",
                IsVeteran = true
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            var anotherPasswordHash = new PasswordHasher<ApplicationUser>();
            anotherUser.PasswordHash = passwordHash.HashPassword(anotherUser, "Bobby8*");
            modelBuilder.Entity<ApplicationUser>().HasData(anotherUser);

            modelBuilder.Entity<PickupGame>().HasData(
                new PickupGame()
                {
                    PickupGameId = 1,
                    MaxGoalies = 2,
                    MaxSkaters = 20,
                    Location = "Ford Ice Bellvue",
                    DateTime = DateTime.Now,
                    Title = "Hockey"
                },
                new PickupGame()
                {
                    PickupGameId = 2,
                    MaxGoalies = 2,
                    MaxSkaters = 20,
                    Location = "Ford Ice Antioch",
                    DateTime = DateTime.Now,
                    Title = "Hockey again"
                }
            );

            modelBuilder.Entity<UserPickupGame>().HasData(
                new UserPickupGame()
                {
                    UserPickupGameId = 1,
                    UserId = user.Id,
                    PickupGameId = 1,
                    DateTime = DateTime.Now,
                    IsStandby = false

                },
                new UserPickupGame()
                {
                    UserPickupGameId = 2,
                    UserId = anotherUser.Id,
                    PickupGameId = 1,
                    DateTime = DateTime.Now,
                    IsStandby = false

                }
            );

            modelBuilder.Entity<PlayerStats>().HasData(
                new PlayerStats()
                {
                    PlayerStatsId = 1,
                    PickupGameId = 1,
                    UserId = anotherUser.Id,
                    Shots = 3,
                    Goals = 1,
                    TOI = 14.5,
                    PIM = 0,
                    Assists = 2
                },
                new PlayerStats()
                {
                    PlayerStatsId = 2,
                    PickupGameId = 1,
                    UserId = user.Id,
                    ShotsFaced = 40,
                    GoalsAllowed = 6,
                    TOI = 60,
                    PIM = 0
                },
                new PlayerStats()
                {
                    PlayerStatsId = 3,
                    PickupGameId = 1,
                    UserId = anotherUser.Id,
                    Shots = 11,
                    Goals = 4,
                    TOI = 14.5,
                    PIM = 0,
                    Assists = 2
                },
                new PlayerStats()
                {
                    PlayerStatsId = 4,
                    PickupGameId = 1,
                    UserId = user.Id,
                    ShotsFaced = 55,
                    GoalsAllowed = 3,
                    TOI = 60,
                    PIM = 0
                }
            );

            modelBuilder.Entity<PredsGame>().HasData(
                new PredsGame()
                {
                    PredsGameId = 1,
                    Opponent = "Dallas Stars",
                    DateTime = DateTime.Now
                }
            );

            modelBuilder.Entity<DonatedTickets>().HasData(
                new DonatedTickets()
                {
                    DonatedTicketsId = 1,
                    PredsGameId = 1,
                    UserId = user.Id,
                    EmailAddress = "test@test.com",
                    EmailBody = "test",
                    EmailTitle = "test",
                    TransactionComplete = true
                },
                new DonatedTickets()
                {
                    DonatedTicketsId = 2,
                    PredsGameId = 1,
                    UserId = user.Id,
                    EmailAddress = "test@test.com",
                    EmailBody = "test",
                    EmailTitle = "test",
                    TransactionComplete = true
                }
            );

            modelBuilder.Entity<Ticket>().HasData(
                new Ticket()
                {
                    TicketId = 1,
                    DonatedTicketsId = 1,
                    Section = "301",
                    Row = "M",
                    Seat = "7"
                },
                new Ticket()
                {
                    TicketId = 2,
                    DonatedTicketsId = 1,
                    Section = "301",
                    Row = "M",
                    Seat = "8"
                },
                new Ticket()
                {
                    TicketId = 3,
                    DonatedTicketsId = 2,
                    Section = "101",
                    Row = "B",
                    Seat = "11"
                },
                new Ticket()
                {
                    TicketId = 4,
                    DonatedTicketsId = 2,
                    Section = "101",
                    Row = "B",
                    Seat = "12"
                }
            );
            modelBuilder.Entity<UserPredsGame>().HasData(
               new UserPredsGame()
               {
                   UserPredsGameId = 1,
                   UserId = user.Id,
                   DonatedTicketsId = 1,
                   PredsGameId = 1
               },
               new UserPredsGame()
               {
                   UserPredsGameId = 2,
                   UserId = anotherUser.Id,
                   DonatedTicketsId = 2,
                   PredsGameId = 1
               }
            );
        }
    }
}
