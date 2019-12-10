using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _7Element.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Position = table.Column<string>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false),
                    IsVeteran = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PickupGame",
                columns: table => new
                {
                    PickupGameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaxSkaters = table.Column<int>(nullable: false),
                    MaxGoalies = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickupGame", x => x.PickupGameId);
                });

            migrationBuilder.CreateTable(
                name: "PredsGame",
                columns: table => new
                {
                    PredsGameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Opponent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredsGame", x => x.PredsGameId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerStats",
                columns: table => new
                {
                    PlayerStatsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    PickupGameId = table.Column<int>(nullable: false),
                    Shots = table.Column<int>(nullable: false),
                    PIM = table.Column<int>(nullable: false),
                    Goals = table.Column<int>(nullable: false),
                    Assists = table.Column<int>(nullable: false),
                    TOI = table.Column<double>(nullable: false),
                    ShotsFaced = table.Column<int>(nullable: false),
                    GoalsAllowed = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStats", x => x.PlayerStatsId);
                    table.ForeignKey(
                        name: "FK_PlayerStats_PickupGame_PickupGameId",
                        column: x => x.PickupGameId,
                        principalTable: "PickupGame",
                        principalColumn: "PickupGameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerStats_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPickupGame",
                columns: table => new
                {
                    UserPickupGameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    PickupGameId = table.Column<int>(nullable: false),
                    IsStandby = table.Column<bool>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPickupGame", x => x.UserPickupGameId);
                    table.ForeignKey(
                        name: "FK_UserPickupGame_PickupGame_PickupGameId",
                        column: x => x.PickupGameId,
                        principalTable: "PickupGame",
                        principalColumn: "PickupGameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserPickupGame_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonatedTickets",
                columns: table => new
                {
                    DonatedTicketsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    PredsGameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonatedTickets", x => x.DonatedTicketsId);
                    table.ForeignKey(
                        name: "FK_DonatedTickets_PredsGame_PredsGameId",
                        column: x => x.PredsGameId,
                        principalTable: "PredsGame",
                        principalColumn: "PredsGameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonatedTickets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    TicketId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonatedTicketsId = table.Column<int>(nullable: false),
                    Section = table.Column<string>(nullable: false),
                    Row = table.Column<string>(nullable: false),
                    Seat = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Ticket_DonatedTickets_DonatedTicketsId",
                        column: x => x.DonatedTicketsId,
                        principalTable: "DonatedTickets",
                        principalColumn: "DonatedTicketsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPredsGame",
                columns: table => new
                {
                    UserPredsGameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    PredsGameId = table.Column<int>(nullable: false),
                    DonatedTicketsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPredsGame", x => x.UserPredsGameId);
                    table.ForeignKey(
                        name: "FK_UserPredsGame_DonatedTickets_DonatedTicketsId",
                        column: x => x.DonatedTicketsId,
                        principalTable: "DonatedTickets",
                        principalColumn: "DonatedTicketsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPredsGame_PredsGame_PredsGameId",
                        column: x => x.PredsGameId,
                        principalTable: "PredsGame",
                        principalColumn: "PredsGameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserPredsGame_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsAdmin", "IsVeteran", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Position", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "00000000-ffff-ffff-ffff-ffffffffffff", 0, "8a8a8a09-e619-4d5e-a761-d7eb1a4e48cb", "admin@admin.com", true, "Admina", true, true, "Straytor", false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEEnTyBPdOHugRlGCs1vDzIoeFHbzv8Qx66ELaKrC3K4Bq7PwTlvE4Zy/Mrx6ifmXXQ==", null, false, "Goalie", "7f434309-a4d9-48e9-9ebb-8803db794577", false, "admin@admin.com" },
                    { "00000000-ffff-ffff-ffff-fffffffff123", 0, "041a86ff-0222-45c8-b241-bba32654cef5", "bobby@bobby.com", true, "Bobby", false, true, "Brady", false, null, "BOBBY@BOBBY.COM", "BOBBY@BOBBY.COM", "AQAAAAEAACcQAAAAEF7riStwGMw/w+DjXL8ELbHxNrG1odfa9K5lOgsSA2TOekKP8hXtNgXHz1x5RXW5uw==", null, false, "Forward", "7f434309-a4d9-48e9-9ebb-8803db794123", false, "bobby@bobby.com" }
                });

            migrationBuilder.InsertData(
                table: "PickupGame",
                columns: new[] { "PickupGameId", "DateTime", "Location", "MaxGoalies", "MaxSkaters", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 12, 10, 10, 14, 49, 424, DateTimeKind.Local).AddTicks(7897), "Ford Ice Bellvue", 2, 20, "Hockey" },
                    { 2, new DateTime(2019, 12, 10, 10, 14, 49, 425, DateTimeKind.Local).AddTicks(5674), "Ford Ice Antioch", 2, 20, "Hockey again" }
                });

            migrationBuilder.InsertData(
                table: "PredsGame",
                columns: new[] { "PredsGameId", "DateTime", "Opponent" },
                values: new object[] { 1, new DateTime(2019, 12, 10, 10, 14, 49, 426, DateTimeKind.Local).AddTicks(4063), "Dallas Stars" });

            migrationBuilder.InsertData(
                table: "DonatedTickets",
                columns: new[] { "DonatedTicketsId", "PredsGameId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 2, 1, "00000000-ffff-ffff-ffff-ffffffffffff" }
                });

            migrationBuilder.InsertData(
                table: "PlayerStats",
                columns: new[] { "PlayerStatsId", "Assists", "Goals", "GoalsAllowed", "PIM", "PickupGameId", "Shots", "ShotsFaced", "TOI", "UserId" },
                values: new object[,]
                {
                    { 1, 2, 1, 0, 0, 1, 3, 0, 14.5, "00000000-ffff-ffff-ffff-fffffffff123" },
                    { 2, 0, 0, 6, 0, 1, 0, 40, 60.0, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 3, 2, 4, 0, 0, 1, 11, 0, 14.5, "00000000-ffff-ffff-ffff-fffffffff123" },
                    { 4, 0, 0, 3, 0, 1, 0, 55, 60.0, "00000000-ffff-ffff-ffff-ffffffffffff" }
                });

            migrationBuilder.InsertData(
                table: "UserPickupGame",
                columns: new[] { "UserPickupGameId", "DateTime", "IsStandby", "PickupGameId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 12, 10, 10, 14, 49, 425, DateTimeKind.Local).AddTicks(7617), false, 1, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 2, new DateTime(2019, 12, 10, 10, 14, 49, 425, DateTimeKind.Local).AddTicks(8434), false, 1, "00000000-ffff-ffff-ffff-fffffffff123" }
                });

            migrationBuilder.InsertData(
                table: "Ticket",
                columns: new[] { "TicketId", "DonatedTicketsId", "Row", "Seat", "Section" },
                values: new object[,]
                {
                    { 1, 1, "M", "7", "301" },
                    { 2, 1, "M", "8", "301" },
                    { 3, 2, "B", "11", "101" },
                    { 4, 2, "B", "12", "101" }
                });

            migrationBuilder.InsertData(
                table: "UserPredsGame",
                columns: new[] { "UserPredsGameId", "DonatedTicketsId", "PredsGameId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 2, 2, 1, "00000000-ffff-ffff-ffff-fffffffff123" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DonatedTickets_PredsGameId",
                table: "DonatedTickets",
                column: "PredsGameId");

            migrationBuilder.CreateIndex(
                name: "IX_DonatedTickets_UserId",
                table: "DonatedTickets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStats_PickupGameId",
                table: "PlayerStats",
                column: "PickupGameId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStats_UserId",
                table: "PlayerStats",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_DonatedTicketsId",
                table: "Ticket",
                column: "DonatedTicketsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPickupGame_PickupGameId",
                table: "UserPickupGame",
                column: "PickupGameId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPickupGame_UserId",
                table: "UserPickupGame",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPredsGame_DonatedTicketsId",
                table: "UserPredsGame",
                column: "DonatedTicketsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPredsGame_PredsGameId",
                table: "UserPredsGame",
                column: "PredsGameId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPredsGame_UserId",
                table: "UserPredsGame",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "PlayerStats");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "UserPickupGame");

            migrationBuilder.DropTable(
                name: "UserPredsGame");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "PickupGame");

            migrationBuilder.DropTable(
                name: "DonatedTickets");

            migrationBuilder.DropTable(
                name: "PredsGame");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
