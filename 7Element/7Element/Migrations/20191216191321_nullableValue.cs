using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _7Element.Migrations
{
    public partial class nullableValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPredsGame_DonatedTickets_DonatedTicketsId",
                table: "UserPredsGame");

            migrationBuilder.AlterColumn<int>(
                name: "DonatedTicketsId",
                table: "UserPredsGame",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-fffffffff123",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7e795f27-f7bc-4a68-8440-8f742a31ad15", "AQAAAAEAACcQAAAAEDa/38B4g5mb30izYmuUsGsokS/gebieY015YAscJGstvQijSrDqqUfweGjRS0Orpg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9e818902-d9c1-4e65-981c-003c87479cc4", "AQAAAAEAACcQAAAAEBjMxOl1x0QfN4v0JBnFM5Ji6k2tvuLphbT6YIIyvGoJiuv6H06XS2DlW9s8R2MeUA==" });

            migrationBuilder.UpdateData(
                table: "PickupGame",
                keyColumn: "PickupGameId",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2019, 12, 16, 13, 13, 20, 895, DateTimeKind.Local).AddTicks(1778));

            migrationBuilder.UpdateData(
                table: "PickupGame",
                keyColumn: "PickupGameId",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2019, 12, 16, 13, 13, 20, 895, DateTimeKind.Local).AddTicks(9221));

            migrationBuilder.UpdateData(
                table: "PredsGame",
                keyColumn: "PredsGameId",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2019, 12, 16, 13, 13, 20, 896, DateTimeKind.Local).AddTicks(7520));

            migrationBuilder.UpdateData(
                table: "UserPickupGame",
                keyColumn: "UserPickupGameId",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2019, 12, 16, 13, 13, 20, 896, DateTimeKind.Local).AddTicks(1155));

            migrationBuilder.UpdateData(
                table: "UserPickupGame",
                keyColumn: "UserPickupGameId",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2019, 12, 16, 13, 13, 20, 896, DateTimeKind.Local).AddTicks(1967));

            migrationBuilder.AddForeignKey(
                name: "FK_UserPredsGame_DonatedTickets_DonatedTicketsId",
                table: "UserPredsGame",
                column: "DonatedTicketsId",
                principalTable: "DonatedTickets",
                principalColumn: "DonatedTicketsId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPredsGame_DonatedTickets_DonatedTicketsId",
                table: "UserPredsGame");

            migrationBuilder.AlterColumn<int>(
                name: "DonatedTicketsId",
                table: "UserPredsGame",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-fffffffff123",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9ae51974-6dd7-42b5-85c1-c0e25adcc156", "AQAAAAEAACcQAAAAEJfIwwIy0pzd8bh4w4kcG0oLBZSOPlH5X0G9PrgCr10AoCml9uIiLvu0IctecPFqZw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5015a647-7a97-47dc-8c4f-34a0d2e6fb57", "AQAAAAEAACcQAAAAELxpVepnhBxaTcUC7ER4ejDr98rgC273fKUX8H1QmT84bKUIrx1PlbzfkoY8Ex5cwA==" });

            migrationBuilder.UpdateData(
                table: "PickupGame",
                keyColumn: "PickupGameId",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2019, 12, 12, 9, 24, 32, 801, DateTimeKind.Local).AddTicks(6759));

            migrationBuilder.UpdateData(
                table: "PickupGame",
                keyColumn: "PickupGameId",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2019, 12, 12, 9, 24, 32, 802, DateTimeKind.Local).AddTicks(4840));

            migrationBuilder.UpdateData(
                table: "PredsGame",
                keyColumn: "PredsGameId",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2019, 12, 12, 9, 24, 32, 803, DateTimeKind.Local).AddTicks(3395));

            migrationBuilder.UpdateData(
                table: "UserPickupGame",
                keyColumn: "UserPickupGameId",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2019, 12, 12, 9, 24, 32, 802, DateTimeKind.Local).AddTicks(6869));

            migrationBuilder.UpdateData(
                table: "UserPickupGame",
                keyColumn: "UserPickupGameId",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2019, 12, 12, 9, 24, 32, 802, DateTimeKind.Local).AddTicks(7674));

            migrationBuilder.AddForeignKey(
                name: "FK_UserPredsGame_DonatedTickets_DonatedTicketsId",
                table: "UserPredsGame",
                column: "DonatedTicketsId",
                principalTable: "DonatedTickets",
                principalColumn: "DonatedTicketsId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
